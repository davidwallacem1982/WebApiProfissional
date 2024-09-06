using WebApiProfissional.Domain.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace WebApiProfissional.Infra.Helpers
{
    /// <summary>
    /// Classe auxiliar estática responsável por criar uma lista paginada de itens de forma assíncrona.
    /// Esta classe utiliza duas tarefas paralelas para otimizar o processo de paginação, melhorando o 
    /// desempenho ao evitar consultas duplicadas ao banco de dados.
    /// </summary>
    public static class PaginationHelper
    {
        /// <summary>
        /// Cria de forma assíncrona uma lista paginada de itens a partir de uma fonte de dados <see cref="IQueryable{T}"/>.
        /// Executa em paralelo a contagem total de itens e a consulta dos itens da página atual, 
        /// retornando um objeto <see cref="PagedList{T}"/> com as informações de paginação.
        /// </summary>
        /// <typeparam name="T">O tipo de dados que está sendo paginado.</typeparam>
        /// <param name="source">A consulta <see cref="IQueryable{T}"/> que será paginada.</param>
        /// <param name="pageNumber">O número da página a ser retornada.</param>
        /// <param name="pageSize">O número de itens por página.</param>
        /// <returns>Um objeto <see cref="PagedList{T}"/> contendo os itens da página solicitada e informações de paginação.</returns>
        public static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var itens = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(itens, pageNumber, pageSize, count);
        }
    }

}
