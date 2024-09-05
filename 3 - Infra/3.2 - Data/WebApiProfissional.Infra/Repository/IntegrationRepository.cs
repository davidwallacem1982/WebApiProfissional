using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Infra.Configurations.Contexts;
using WebApiProfissional.Infra.Repository.Base;

namespace WebApiProfissional.Infra.Repository
{
    public class FuncionariosRepository : RepositoryBase<Funcionarios>, IFuncionariosRepository { public FuncionariosRepository(IntegrationContext context) : base(context) { } }
    
    public class UsuarioRepository : RepositoryBase<Usuarios>, IUsuarioRepository { public UsuarioRepository(IntegrationContext context) : base(context) { } }

    public class RefreshTokenRepository : RepositoryBase<RefreshTokens>, IRefreshTokenRepository { public RefreshTokenRepository(IntegrationContext context) : base(context) { } }
    
    public class RevokedTokenRepository : RepositoryBase<RevokedTokens>, IRevokedTokenRepository { public RevokedTokenRepository(IntegrationContext context) : base(context) { } }
}
