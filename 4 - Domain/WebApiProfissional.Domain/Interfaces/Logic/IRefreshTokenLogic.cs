using System.Threading.Tasks;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.InputModels.Authentication;

namespace WebApiProfissional.Domain.Interfaces.Logic
{
    public interface IRefreshTokenLogic
    {
        Task<RefreshTokens> GetAssociarRefreshToken(RefreshTokenInput model);
    }
}
