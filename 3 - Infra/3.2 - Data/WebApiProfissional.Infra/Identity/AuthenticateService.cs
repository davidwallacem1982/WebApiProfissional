using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Utils;

namespace WebApiProfissional.Infra.Identity
{
    public class AuthenticateService(ILogger<AuthenticateService> logger,
                               IUsuarioRepository usuario,
                               IRefreshTokenRepository refreshToken,
                               IRevokedTokenRepository revokedToken,
                               IConfiguration configuration) : IAuthenticate
    {
        private readonly ILogger<AuthenticateService> _logger = logger;
        private readonly IUsuarioRepository _usuario = usuario;
        private readonly IRefreshTokenRepository _refreshToken = refreshToken;
        private readonly IRevokedTokenRepository _revokedToken = revokedToken;
        private readonly IConfiguration _configuration = configuration;

        public async Task<bool> AuthenticateUserAsync(string login, string senha)
        {
            var usuario = await _usuario.GetSingleOrDefaultAsyncBy(u => u.Login == login) ?? throw new InvalidOperationException("O Usuário não encontrado");

            return PasswordHelper.ValidarSenha(senha, usuario.PasswordHash, usuario.PasswordSalt);
        }

        /// <summary>
        /// Cria e retorna as credenciais de assinatura para JWT.
        /// Este método configura uma chave secreta HMAC para a assinatura de tokens JWT. 
        /// Verifica se a chave é suficientemente longa (pelo menos 256 bits) e cria um objeto 
        /// <see cref="SigningCredentials"/> usando a chave e o algoritmo HMAC SHA-256.
        /// </summary>
        /// <returns>Um objeto <see cref="SigningCredentials"/> configurado para assinatura de tokens JWT.</returns>
        /// <exception cref="InvalidOperationException">Se a chave HMAC for menor que 256 bits.</exception>
        private SigningCredentials Credentials()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            return new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }


        //public async Task<string> GenerateAccesToken(int id, string login)
        //{
        //    var identityClaims = new ClaimsIdentity();

        //    identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, id.ToString()));
        //    identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, login));
        //    identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //    identityClaims.AddClaim(new Claim("scope", "read write")); // Define os escopos

        //    var issuer = _configuration["jwt:issuer"];
        //    var audience = _configuration["jwt:audience"];
        //    var credentials = Credentials();

        //    var securityToken = new JwtSecurityToken(
        //        issuer: issuer,
        //        audience: audience,
        //        claims: identityClaims.Claims,
        //        expires: DateTime.UtcNow.AddMinutes(15), // Expiração configurável
        //        signingCredentials: credentials
        //    );

        //    return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(securityToken));
        //}


        public async Task<string> GenerateToken(int userId, string tokenType, string login = null)
        {
            // Criação de uma identidade de claims para o Refresh Token
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()));
            if (login != null)
            {
                identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, login));
            }
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); // ID único do token
            identityClaims.AddClaim(new Claim("token_type", tokenType)); // Define o tipo como "rt+jwt" (Refresh Token)

            // Configurações do token JWT
            var issuer = _configuration["jwt:issuer"];
            var audience = _configuration["jwt:audience"];
            var credentials = Credentials();

            // Criação do token JWT
            var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: identityClaims.Claims,
                expires: DateTime.UtcNow.AddDays(1), // O Refresh Token geralmente tem um período de validade mais longo
                signingCredentials: credentials
            );

            // Retorna o token em formato string
            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(securityToken));
        }

        public async Task<string> GenerateRefreshToken(int id) => await GenerateToken(id, "rt+jwt");
    }
}
