﻿using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Infra.Services.Identity;
using WebApiProfissional.Utils;

namespace WebApiProfissional.Services.Logic
{
    public class AuthenticateLogic : IAuthenticateLogic
    {
        private readonly ILogger<AuthenticateService> _logger;
        private readonly IUsuarioRepository _usuario;
        private readonly IAuthenticateService _authenticate;

        public AuthenticateLogic(ILogger<AuthenticateService> logger, IUsuarioRepository usuario, IAuthenticateService authenticate)
        {
            _logger = logger;
            _usuario = usuario;
            _authenticate = authenticate;
        }

        public async Task<bool> AuthenticateUserAsync(string login, string senha)
        {
            var usuario = await _authenticate.AuthenticateUserAsync(login, senha) ?? throw new InvalidOperationException("O Usuário não encontrado");

            return PasswordHelper.ValidarSenha(senha, usuario.PasswordHash, usuario.PasswordSalt);
        }

        public Task<string> GenerateAccesToken(int id, string login)
        {
            return _authenticate.GenerateAccesToken(id, login);
        }

        public Task<string> GenerateRefreshToken(int id)
        {
            return _authenticate.GenerateRefreshToken(id);
        }

        public Task<bool> IsTokenRevoked(string token)
        {
            return _authenticate.IsTokenRevoked(token);
        }

        public Task RevokeRefreshToken(int userId, string refreshToken)
        {
            return _authenticate.RevokeRefreshToken(userId, refreshToken);
        }

        public Task StoreRefreshToken(int id, string refreshToken, string jti, DateTime expiresAt)
        {
            return _authenticate.StoreRefreshToken(id, refreshToken, jti, expiresAt);
        }

        public Task WithoutRevokeRefreshToken(string Login, string jti)
        {
            return _authenticate.WithoutRevokeRefreshToken(Login, jti);
        }

        public Task WithRevokeRefreshToken(string Login, string jti)
        {
            return _authenticate.WithRevokeRefreshToken(Login.Trim(), jti);
        }
    }
}
