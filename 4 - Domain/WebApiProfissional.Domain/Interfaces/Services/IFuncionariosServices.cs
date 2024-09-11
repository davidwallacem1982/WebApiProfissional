using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.Interfaces.Services
{
    public interface IFuncionariosServices
    {
        /// <summary>
        /// Recupera um funcionário específico do banco de dados com base no seu ID.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser recuperado.</param>
        /// <returns>
        /// Um objeto <see cref="Funcionarios"/> contendo os dados do funcionário correspondente ao ID fornecido.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco para buscar o funcionário com o ID fornecido. 
        /// Se encontrado, o funcionário é retornado com os campos Id, Nome e Sobrenome.
        /// Em caso de erro, como uma <see cref="Exception"/>, o erro é registrado no log e uma exceção detalhada é lançada.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da consulta.
        /// </exception>
        Funcionarios SelectFuncionariosById(int id);

        /// <summary>
        /// Recupera uma lista completa de funcionários do banco de dados.
        /// </summary>
        /// <returns>
        /// Uma lista de objetos <see cref="Funcionarios"/> contendo todos os funcionários armazenados no banco de dados.
        /// </returns>
        /// <remarks>
        /// O método realiza uma consulta no banco para buscar todos os funcionários. 
        /// Em caso de erro, como uma <see cref="Exception"/>, o erro é registrado no log, 
        /// e uma nova exceção com informações detalhadas é lançada.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre uma <see cref="Exception"/> durante a execução da consulta.
        /// </exception>/summary>
        /// <returns></returns>
        IList<Funcionarios> SelectAllFuncionarios();

        /// <summary>
        /// Recupera uma lista de funcionários, ordenada por nome, para ser utilizada em um dropdown.
        /// </summary>
        /// <returns>
        /// Uma lista de objetos <see cref="Funcionarios"/> contendo o Id, Nome e Sobrenome dos funcionários.
        /// </returns>
        /// <remarks>
        /// O método realiza um select no banco de dados para buscar todos os funcionários, 
        /// ordenando-os por nome e projetando-os em uma nova lista com os campos Id, Nome e Sobrenome.
        /// Em caso de erro (ex. uma <see cref="Exception"/>), o erro é registrado no log e a exceção é lançada novamente.
        /// </remarks>
        /// <exception cref="Exception">
        /// Lançada quando ocorre um erro durante a execução da consulta.
        /// </exception>
        IList<Funcionarios> SelectDropDownFuncionarios();

        /// <summary>
        /// Recupera assincronamente uma lista paginada de funcionários.
        /// </summary>
        /// <param name="pageNumber">O número da página a ser recuperada (começando de 1).</param>
        /// <param name="pageSize">A quantidade de itens a serem recuperados por página.</param>
        /// <returns>
        /// Uma tarefa que representa a operação assíncrona. 
        /// O resultado da tarefa contém uma lista paginada de objetos <see cref="Funcionarios"/>.
        /// </returns>
        /// <remarks>
        /// O método busca a lista completa de funcionários, ordena por nome 
        /// e então projeta em um formato paginado usando o número da página e o tamanho da página especificados.
        /// </remarks>
        Task<PagedList<Funcionarios>> PaginationFuncionariosAsync(int pageNumber, int pageSize);

        /// <summary>
        /// Verifica se um funcionário com o CPF fornecido já existe no banco de dados.
        /// </summary>
        /// <param name="cpf">CPF do funcionário a ser verificado.</param>
        /// <returns>Retorna <c>true</c> se o funcionário existir no banco de dados; caso contrário, <c>false</c>.</returns>
        /// <exception cref="Exception">
        /// Lança uma exceção se ocorrer um erro durante a execução da consulta no banco de dados.
        /// A exceção capturada será registrada e o método lançará uma nova exceção personalizada com detalhes do erro.
        /// </exception>
        bool SelectFuncionarioExistByCpf(long cpf);

        /// <summary>
        /// Verifica de forma assíncrona se um funcionário com o CPF fornecido já existe no banco de dados.
        /// </summary>
        /// <param name="cpf">CPF do funcionário a ser verificado.</param>
        /// <returns>
        /// Uma <see cref="Task{TResult}"/> que representa o resultado da operação assíncrona.
        /// O valor da <see cref="Task"/> será <c>true</c> se o funcionário existir no banco de dados, caso contrário, <c>false</c>.
        /// </returns>
        /// <exception cref="Exception">
        /// Lança uma exceção se ocorrer um erro durante a execução da consulta no banco de dados.
        /// A exceção capturada será registrada e o método lançará uma nova exceção personalizada com detalhes do erro.
        /// </exception>
        Task<bool> SelectFuncionarioExistByCpfAsync(long cpf);

        /// <summary>
        /// Insere um novo registro de funcionário no banco de dados de forma assíncrona.
        /// </summary>
        /// <param name="entity">Entidade <see cref="Funcionarios"/> contendo os dados do funcionário a ser inserido.</param>
        /// <returns>
        /// Uma <see cref="Task{Funcionarios}"/> que representa a operação assíncrona.
        /// O valor da <see cref="Task"/> será a entidade <see cref="Funcionarios"/> inserida com sucesso no banco de dados.
        /// </returns>
        /// <exception cref="Exception">
        /// Lança uma exceção se ocorrer um erro durante a inserção do funcionário no banco de dados.
        /// A exceção capturada será registrada e o método lançará uma nova exceção personalizada com detalhes do erro.
        /// </exception>
        Task<Funcionarios> InsertFuncionarioAsync(Funcionarios entity);
    }
}
