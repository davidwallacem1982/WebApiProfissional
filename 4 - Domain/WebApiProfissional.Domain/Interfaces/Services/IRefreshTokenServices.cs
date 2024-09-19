using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.InputModels.Authentication;

namespace WebApiProfissional.Domain.Interfaces.Services
{
    public interface IRefreshTokenServices
    {
        Task<RefreshTokens> SelectAssociarRefreshToken(RefreshTokenInput model);
    }
}
