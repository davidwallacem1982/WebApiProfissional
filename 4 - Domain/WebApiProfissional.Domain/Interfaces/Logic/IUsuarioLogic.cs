using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Usuarios;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.Interfaces.Logic
{
    public interface IUsuarioLogic
    {

        /// <summary>
        /// Verifica se um usuário existe no sistema com base no login fornecido de forma assíncrona.
        /// </summary>
        /// <param name="login">O login do usuário a ser verificado.</param>
        /// <returns>True se o usuário existir; caso contrário, false.</returns>
        Task<bool> UserExistByLoginAsync(string login);

        /// <summary>
        /// Verifica se um usuário existe no sistema com base no ID fornecido de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do usuário a ser verificado.</param>
        /// <returns>True se o usuário existir; caso contrário, false.</returns>
        Task<bool> UserExistByIdAsync(int id);

        /// <summary>
        /// Obtém um usuário do sistema com base no ID fornecido de forma assíncrona.
        /// </summary>
        /// <param name="id">O ID do usuário a ser recuperado.</param>
        /// <returns>A entidade <see cref="Usuarios"/> correspondente ao ID fornecido.</returns>
        Task<Usuarios> GetUserByIdAsync(int id);

        /// <summary>
        /// Obtém um usuário do sistema com base no login fornecido de forma assíncrona.
        /// </summary>
        /// <param name="login">O login do usuário a ser recuperado.</param>
        /// <returns>A entidade <see cref="Usuarios"/> correspondente ao login fornecido.</returns>
        Task<Usuarios> GetUserByLoginAsync(string login);

        /// <summary>
        /// Inclui um novo usuário no sistema com base nos dados fornecidos no modelo de entrada.
        /// </summary>
        /// <param name="model">Modelo de entrada contendo os dados do novo usuário.</param>
        /// <returns>A entidade <see cref="Usuarios"/> que foi inserida no banco de dados.</returns>
        Task<Usuarios> IncluirUserAsync(NewUsuarioInput model);

        /// <summary>
        /// Atualiza a senha de um usuário existente no sistema com base nos dados fornecidos no modelo de entrada.
        /// </summary>
        /// <param name="model">Modelo de entrada contendo o ID do usuário e a nova senha.</param>
        /// <returns>A entidade <see cref="Usuarios"/> atualizada no banco de dados.</returns>
        Task<Usuarios> AtualizarSenhaAsync(UpdateUsuarioInput model);
    }
}
