using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.InputModels.Authentication;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Utils;

namespace WebApiProfissional.Infra.Services
{
    public class RefreshTokenServices : IRefreshTokenServices
    {
        private readonly ILogger<RefreshTokenServices> _logger;
        private readonly IRefreshTokenRepository _refreshToken;

        public RefreshTokenServices(ILogger<RefreshTokenServices> logger, IRefreshTokenRepository refreshToken)
        {
            _logger = logger;
            _refreshToken = refreshToken;
        }

        public async Task<RefreshTokens> SelectAssociarRefreshToken(RefreshTokenInput model)
        {
            try
            {
                _logger.LogInformation("SelectAssociarRefreshToken no Banco - Inicio");
                var refreshToken = await _refreshToken.GetSingleOrDefaultAsyncBy(rt => rt.Token == model.RefreshToken);

                if (refreshToken == null)
                {
                    _logger.LogWarning("Refresh Token inválido.");
                    throw new Exception("Refresh Token inválido.");
                }

                _logger.LogInformation("SelectAssociarRefreshToken no Banco - Fim");
                return refreshToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

    }
}
