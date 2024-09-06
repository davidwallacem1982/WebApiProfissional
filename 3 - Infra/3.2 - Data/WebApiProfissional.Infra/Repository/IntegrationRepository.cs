using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Infra.Configurations.Contexts;
using WebApiProfissional.Infra.Repository.Base;

namespace WebApiProfissional.Infra.Repository
{
    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Funcionarios"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{Funcionarios}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class FuncionariosRepository : RepositoryBase<Funcionarios>, IFuncionariosRepository
    {
        public FuncionariosRepository(IntegrationContext context) : base(context) { }
    }

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Usuarios"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{Usuarios}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class UsuarioRepository : RepositoryBase<Usuarios>, IUsuarioRepository
    {
        public UsuarioRepository(IntegrationContext context) : base(context) { }
    }

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="RefreshTokens"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{RefreshTokens}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class RefreshTokenRepository : RepositoryBase<RefreshTokens>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(IntegrationContext context) : base(context) { }
    }

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="RevokedTokens"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{RevokedTokens}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class RevokedTokenRepository : RepositoryBase<RevokedTokens>, IRevokedTokenRepository
    {
        public RevokedTokenRepository(IntegrationContext context) : base(context) { }
    }
}
