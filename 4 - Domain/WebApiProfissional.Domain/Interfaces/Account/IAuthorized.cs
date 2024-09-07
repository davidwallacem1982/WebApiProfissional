using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.ViewModels;

namespace WebApiProfissional.Domain.Interfaces.Account
{
    public interface IAuthorized
    {
        /// <summary>
        /// Obtém o ID do usuário autenticado a partir dos claims armazenados no contexto HTTP.
        /// </summary>
        /// <returns>O ID do usuário autenticado como um inteiro.</returns>
        /// <exception cref="InvalidOperationException">Lança uma exceção se o ID do usuário não puder ser recuperado ou convertido.</exception>
        int GetId();

        /// <summary>
        /// Recupera as informações do usuário autenticado do banco de dados e as encapsula em um modelo de visualização.
        /// </summary>
        /// <param name="_usuario">Interface de lógica do usuário para buscar as informações no banco de dados.</param>
        /// <returns>Um objeto UserAuthorizedViewModel contendo o ID, login e status de administrador do usuário autenticado.</returns>
        /// <exception cref="InvalidOperationException">Lança uma exceção se o usuário não for encontrado no banco de dados.</exception>
        UserAuthorizedViewModel User(IUsuarioLogic _usuario);

    }
}
