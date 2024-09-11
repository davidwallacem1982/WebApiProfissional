using WebApiProfissional.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.Interfaces.Services
{
    public interface IUsuarioServices
    {
        bool SelectExistByLogin(string login);

        /// <summary>
        /// Verifica assincronamente se existe um usuário no banco de dados com o login fornecido.
        /// </summary>
        /// <param name="login">O login do usuário a ser verificado.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é um booleano indicando se o login existe ou não no banco de dados.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco de dados para verificar se há algum usuário com o login fornecido.
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da verificação.
        /// </exception>
        Task<bool> SelectExistByLoginAsync(string login);

        /// <summary>
        /// Verifica assincronamente se existe um usuário no banco de dados com o ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser verificado.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é um booleano indicando se o ID existe ou não no banco de dados.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco de dados para verificar se há algum usuário com o ID fornecido.
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da verificação.
        /// </exception>
        Task<bool> SelectUserExistByIdAsync(int id);

        /// <summary>
        /// Recupera assincronamente um usuário do banco de dados com base no login fornecido.
        /// </summary>
        /// <param name="login">O login do usuário a ser recuperado.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é um objeto <see cref="Usuarios"/> que corresponde ao login fornecido, ou <c>null</c> se o usuário não for encontrado.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco de dados para buscar um único usuário que corresponda ao login fornecido.
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da consulta.
        /// </exception>
        Task<Usuarios> SelectUserByLoginAsync(string login);

        /// <summary>
        /// Recupera assincronamente um usuário do banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser recuperado.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é um objeto <see cref="Usuarios"/> que corresponde ao ID fornecido, ou <c>null</c> se o usuário não for encontrado.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco de dados para buscar um usuário com o ID fornecido.
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada com detalhes sobre o erro.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da consulta.
        /// </exception>
        Task<Usuarios> SelectUserByIdAsync(int id);

        /// <summary>
        /// Insere assincronamente um novo usuário no banco de dados.
        /// </summary>
        /// <param name="entity">O objeto <see cref="Usuarios"/> que representa o usuário a ser inserido.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é o objeto <see cref="Usuarios"/> que foi inserido no banco de dados.
        /// </returns>
        /// <remarks>
        /// O método adiciona o usuário fornecido ao banco de dados e salva as alterações. 
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada com detalhes sobre o erro.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a inserção do usuário.
        /// </exception>
        Task<Usuarios> InsertUserAsync(Usuarios entity);

        /// <summary>
        /// Atualiza assincronamente as informações de um usuário no banco de dados.
        /// </summary>
        /// <param name="entity">O objeto <see cref="Usuarios"/> contendo as informações do usuário a ser atualizado.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. O resultado da tarefa é o objeto <see cref="Usuarios"/> que foi atualizado no banco de dados.
        /// </returns>
        /// <remarks>
        /// O método atualiza o usuário fornecido no banco de dados e salva as alterações. 
        /// Caso ocorra uma <see cref="Exception"/>, a exceção é registrada no log e uma nova exceção é lançada com detalhes sobre o erro.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a atualização do usuário.
        /// </exception>
        Task<Usuarios> UpdateUserAsync(Usuarios entity);
    }
}
