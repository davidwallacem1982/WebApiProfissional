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
    public class FuncionariosRepository(IntegrationContext context) : RepositoryBase<Funcionarios>(context), IFuncionariosRepository{}

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="Usuarios"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{Usuarios}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class UsuarioRepository(IntegrationContext context) : RepositoryBase<Usuarios>(context), IUsuarioRepository{}

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="RefreshTokens"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{RefreshTokens}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class RefreshTokenRepository(IntegrationContext context) : RepositoryBase<RefreshTokens>(context), IRefreshTokenRepository{}

    /// <summary>
    /// Repositório para operações relacionadas à entidade <see cref="RevokedTokens"/>.
    /// Herda os métodos padrão de <see cref="RepositoryBase{RevokedTokens}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados utilizado para realizar as operações.</param>
    public class RevokedTokenRepository(IntegrationContext context) : RepositoryBase<RevokedTokens>(context), IRevokedTokenRepository{}
}
