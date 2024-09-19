using System;
using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.InputModels.Authentication;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Services;

namespace WebApiProfissional.Services.Logic
{
    public class RefreshTokenLogic : IRefreshTokenLogic
    {
        private readonly IRefreshTokenServices _refreshToken;

        public RefreshTokenLogic(IRefreshTokenServices refreshToken)
        {
            _refreshToken = refreshToken;
        }

        public async Task<RefreshTokens> GetAssociarRefreshToken(RefreshTokenInput model)
        {
            var tokenDetails = await _refreshToken.SelectAssociarRefreshToken(model) ?? throw new InvalidOperationException("Refresh Token inválido.");

            return tokenDetails;
        }
    }
}
