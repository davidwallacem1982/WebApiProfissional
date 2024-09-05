using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Entities.Token;
using WebApiProfissional.Domain.Interfaces.Repository.Base;

/// <summary>
/// Contém as interfaces de repositório que fornecem operações de acesso a dados para as entidades da aplicação, utilizando um padrão de repositório base.
/// </summary>
/// <remarks>
/// Essas interfaces definem contratos para o acesso e manipulação de dados das entidades <see cref="Funcionarios"/>, <see cref="Usuarios"/>, 
/// <see cref="RefreshTokens"/> e <see cref="RevokedTokens"/>.
/// Elas estendem a interface genérica <see cref="IRepositoryBase{T}"/>, que provê os métodos base de CRUD (Create, Read, Update, Delete).
/// </remarks>
namespace WebApiProfissional.Domain.Interfaces.Repository
{
    /// <summary>
    /// Define um contrato específico para as operações de repositório da entidade <see cref="Funcionarios"/>.
    /// </summary>
    public interface IFuncionariosRepository : IRepositoryBase<Funcionarios> { }

    /// <summary>
    /// Define um contrato específico para as operações de repositório da entidade <see cref="Usuarios"/>.
    /// </summary>
    public interface IUsuarioRepository : IRepositoryBase<Usuarios> { }

    /// <summary>
    /// Define um contrato específico para as operações de repositório da entidade <see cref="RefreshTokens"/>.
    /// </summary>
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshTokens> { }

    /// <summary>
    /// Define um contrato específico para as operações de repositório da entidade <see cref="RevokedTokens"/>.
    /// </summary>
    public interface IRevokedTokenRepository : IRepositoryBase<RevokedTokens> { }
}
