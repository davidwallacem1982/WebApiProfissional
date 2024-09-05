using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApiProfissional.Domain.Interfaces.Repository.Base
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        #region Assinaturas dos Métodos

        /// <summary>
        /// Retorna o contexto atual do banco de dados.
        /// </summary>
        DbContext GetContext();

        /// <summary>
        /// Adiciona uma nova entidade ao contexto.
        /// </summary>
        /// <param name="entity">A entidade a ser adicionada.</param>
        /// <returns>A entidade adicionada.</returns>
        T Add(T entity);

        /// <summary>
        /// Atualiza uma entidade existente no contexto.
        /// </summary>
        /// <param name="entity">A entidade a ser atualizada.</param>
        void Update(T entity);

        /// <summary>
        /// Remove uma entidade do contexto.
        /// </summary>
        /// <param name="entity">A entidade a ser removida.</param>
        /// <returns>A entidade removida.</returns>
        T Remove(T entity);

        /// <summary>
        /// Obtém uma entidade pelo seu identificador.
        /// </summary>
        /// <param name="id">O identificador da entidade.</param>
        /// <returns>A entidade correspondente ao identificador fornecido.</returns>
        T GetById(int id);

        /// <summary>
        /// Retorna o primeiro elemento que corresponde ao predicado fornecido ou o valor padrão se nenhum elemento for encontrado.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A primeira entidade que corresponde ao filtro ou null.</returns>
        T GetFirstOrDefaultBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o único elemento que corresponde ao predicado fornecido ou o valor padrão se nenhum elemento for encontrado.
        /// Lança uma exceção se houver mais de um elemento correspondente.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A única entidade que corresponde ao filtro ou null.</returns>
        T GetSingleOrDefaultBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o primeiro elemento que corresponde ao predicado fornecido sem fazer o rastreamento das mudanças no contexto.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A primeira entidade que corresponde ao filtro ou null, sem rastreamento.</returns>
        T GetAsNoTracking(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna todos os elementos do conjunto de dados da entidade T.
        /// </summary>
        /// <returns>Uma coleção <see cref="IQueryable{T}"/> contendo todos os elementos.</returns>
        IQueryable<T> GetList();

        /// <summary>
        /// Retorna uma lista de elementos que correspondem ao predicado fornecido.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>Uma coleção <see cref="IQueryable{T}"/> contendo os elementos que correspondem ao filtro.</returns>
        IQueryable<T> GetListBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica se existe pelo menos um elemento que satisfaça o predicado fornecido.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>True se pelo menos um elemento satisfizer o predicado; caso contrário, false.</returns>
        bool Exist(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o número total de elementos no conjunto de dados da entidade T.
        /// </summary>
        /// <returns>O número total de elementos.</returns>
        long Count();

        /// <summary>
        /// Retorna o número de elementos que correspondem ao predicado fornecido.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>O número de elementos que correspondem ao filtro.</returns>
        long CountBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Persiste todas as alterações feitas no contexto no banco de dados.
        /// </summary>
        /// <returns>Retorna verdadeiro se as alterações foram salvas com sucesso.</returns>
        bool SaveChanges();

        #endregion

        #region Assinaturas dos Métodos Asincronas

        /// <summary>
        /// Obtém uma entidade pelo seu identificador de forma assíncrona.
        /// </summary>
        /// <param name="id">O identificador da entidade.</param>
        /// <returns>Um <see cref="Task{T}"/> representando a entidade correspondente ao identificador fornecido.</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Retorna o primeiro elemento que corresponde ao predicado fornecido ou o valor padrão se nenhum elemento for encontrado de forma assíncrona.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A primeira entidade que corresponde ao filtro ou null.</returns>
        Task<T> GetFirstOrDefaultAsyncBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o único elemento que corresponde ao predicado fornecido ou o valor padrão se nenhum elemento for encontrado de forma assíncrona.
        /// Lança uma exceção se houver mais de um elemento correspondente.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A única entidade que corresponde ao filtro ou null.</returns>
        Task<T> GetSingleOrDefaultAsyncBy(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o primeiro elemento que corresponde ao predicado fornecido sem fazer o rastreamento das mudanças no contexto, de forma assíncrona.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>A primeira entidade que corresponde ao filtro ou null, sem rastreamento.</returns>
        Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna todos os elementos do conjunto de dados da entidade T, sem rastreamento, de forma assíncrona.
        /// </summary>
        /// <returns>Uma coleção <see cref="IEnumerable{T}"/> contendo todos os elementos.</returns>
        Task<IEnumerable<T>> GetListAsync();

        /// <summary>
        /// Retorna uma lista de elementos que correspondem ao predicado fornecido, sem rastreamento, de forma assíncrona.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>Uma coleção <see cref="IEnumerable{T}"/> contendo os elementos que correspondem ao filtro.</returns>
        Task<IEnumerable<T>> GetListByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Verifica se existe pelo menos um elemento que satisfaça o predicado fornecido de forma assíncrona.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>True se pelo menos um elemento satisfizer o predicado; caso contrário, false.</returns>
        Task<bool> ExistAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retorna o número total de elementos no conjunto de dados da entidade T, sem rastreamento, de forma assíncrona.
        /// </summary>
        /// <returns>O número total de elementos.</returns>
        Task<long> CountAsync();

        /// <summary>
        /// Retorna o número de elementos que correspondem ao predicado fornecido, sem rastreamento, de forma assíncrona.
        /// </summary>
        /// <param name="predicate">Expressão lambda que define as condições do filtro.</param>
        /// <returns>O número de elementos que correspondem ao filtro.</returns>
        Task<long> CountByAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Persiste todas as alterações feitas no contexto no banco de dados de forma assíncrona.
        /// </summary>
        /// <returns>Retorna verdadeiro se as alterações foram salvas com sucesso.</returns>
        Task<bool> SaveChangesAsync();

        #endregion
    }
}
