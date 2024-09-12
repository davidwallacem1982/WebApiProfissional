using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Repository;

namespace WebApiProfissional.Infra.Services.Identity
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly ILogger<AuthenticateService> _logger;
        private readonly IUsuarioRepository _usuario;
        private readonly IRefreshTokenRepository _refreshToken;
        private readonly IRevokedTokenRepository _revokedToken;
        private readonly IConfiguration _configuration;

        public AuthenticateService(ILogger<AuthenticateService> logger,
                                   IUsuarioRepository usuario,
                                   IRefreshTokenRepository refreshToken,
                                   IRevokedTokenRepository revokedToken,
                                   IConfiguration configuration)
        {
            _logger = logger;
            _usuario = usuario;
            _refreshToken = refreshToken;
            _revokedToken = revokedToken;
            _configuration = configuration;
        }

        public async Task<Usuarios> AuthenticateUserAsync(string login, string senha)
        {
            return await _usuario.GetSingleOrDefaultAsyncBy(u => u.Login == login);
        }

        public SigningCredentials Credentials()
        {
            var secretKey = _configuration["jwt:secretKey"];

            // Aumentando o tamanho da chave HMAC conforme necessário
            if (secretKey.Length < 256 / 8)
            {
                // Considere seguir as orientações no link fornecido para relaxar a validação do tamanho da chave HMAC, se necessário.
                throw new InvalidOperationException("A chave HMAC deve ter pelo menos 256 bits.");
            }

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            return credentials;
        }

        public async Task<string> GenerateAccesToken(int id, string login)
        {
            var identityClaims = new ClaimsIdentity();

            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, id.ToString()));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, login));
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var issuer = _configuration["jwt:issuer"];
            var audience = _configuration["jwt:audience"];
            var credentials = Credentials();
            DateTime expiration = DateTime.UtcNow.AddMinutes(15); // Ajuste conforme necessário

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials,
                Subject = identityClaims,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(60),
                IssuedAt = DateTime.UtcNow,
                TokenType = "at+jwt"
            });

            var encodedJwt = handler.WriteToken(securityToken);

            return await Task.FromResult(encodedJwt);
        }

        public Task<string> GenerateRefreshToken(int id)
        {
            var jti = Guid.NewGuid().ToString();
            var secretKey = _configuration["jwt:secretKey"];
            var issuer = _configuration["jwt:issuer"];
            var audience = _configuration["jwt:audience"];
            var credentials = Credentials();

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.Jti, jti)
            };

            // Cria as identity claims
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials,
                Subject = identityClaims,
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddDays(1),
                TokenType = "rt+jwt"
            });

            var encodedJwt = handler.WriteToken(securityToken);

            // Armazena o Refresh Token gerado com o Jti na base de dados
            //await StoreRefreshToken(id, encodedJwt, jti, DateTime.Now.AddDays(1));

            return Task.FromResult(encodedJwt);
        }

        public async Task StoreRefreshToken(int id, string refreshToken, string jti, DateTime expiresAt)
        {
            // Obtém o usuário pelo email
            var user = await _usuario.GetSingleOrDefaultAsyncBy(u => u.Id == id) ?? throw new InvalidOperationException("O Usuário não encontrado");
            if (user != null)
            {
                // Insere um novo token na tabela RefreshTokens
                var newToken = new RefreshTokens
                {
                    IdUsuario = user.Id,
                    Token = refreshToken,
                    JwtId = jti,
                    ExpiresAt = expiresAt
                };
                _refreshToken.Add(newToken);
                await _refreshToken.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public async Task WithoutRevokeRefreshToken(string login, string jti)
        {
            // Obtém o usuário pelo email
            var user = await _usuario.GetSingleOrDefaultAsyncBy(u => u.Login == login);
            if (user != null)
            {
                // Revoga o token correspondente ao Jti
                var token = await _refreshToken
                    .GetSingleOrDefaultAsyncBy(rt => rt.IdUsuario == user.Id && rt.JwtId == jti);

                if (token != null)
                {
                    token.IsRevoked = true;
                    token.RevokedAt = DateTime.UtcNow;

                    await _refreshToken.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public async Task WithRevokeRefreshToken(string Login, string jti)
        {
            // Obtém o usuário pelo email
            var user = await _usuario.GetSingleOrDefaultAsyncBy(u => u.Login == Login);
            if (user != null)
            {
                // Revoga o token correspondente ao Jti
                var token = await _refreshToken
                    .GetSingleOrDefaultAsyncBy(rt => rt.IdUsuario == user.Id && rt.JwtId == jti);

                if (token != null)
                {
                    // Grava o token revogado na tabela RevokedTokens
                    var revokedToken = new RevokedTokens
                    {
                        IdUsuario = user.Id,
                        Token = token.Token,
                        RevokedAt = DateTime.UtcNow
                    };

                    _revokedToken.Add(revokedToken);
                    await _revokedToken.SaveChangesAsync();
                }
            }
            else
            {
                throw new Exception("Usuário não encontrado.");
            }
        }

        public async Task<bool> IsTokenRevoked(string token)
        {
            // Verifica se o token está na tabela RevokedTokens
            return await _refreshToken.ExistAsync(rt => rt.Token == token);// Retorna true se o token estiver revogado
        }

        public async Task RevokeRefreshToken(int userId, string refreshToken)
        {
            // Obtém o Refresh Token correspondente ao token fornecido
            var token = await _refreshToken.GetSingleOrDefaultAsyncBy(rt => rt.IdUsuario == userId && rt.Token == refreshToken);

            if (token != null)
            {
                // Marca o Refresh Token como revogado
                token.IsRevoked = true;
                token.RevokedAt = DateTime.UtcNow;

                // Atualiza a tabela de Refresh Tokens
                await _refreshToken.SaveChangesAsync();

                // Grava o token revogado na tabela RevokedTokens
                var revokedToken = new RevokedTokens
                {
                    IdUsuario = userId,
                    Token = refreshToken,
                    RevokedAt = DateTime.UtcNow
                };
                _revokedToken.Add(revokedToken);
                await _revokedToken.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("Refresh Token não encontrado.");
            }
        }
    }
}
