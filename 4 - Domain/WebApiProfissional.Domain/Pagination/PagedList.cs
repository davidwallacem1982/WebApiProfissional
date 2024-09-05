using System;
using System.Collections.Generic;

namespace WebApiProfissional.Domain.Pagination
{
    /// <summary>
    /// Representa uma lista paginada de elementos de um tipo específico, contendo informações sobre a página atual, total de páginas, tamanho da página e contagem total de itens.
    /// </summary>
    /// <typeparam name="T">O tipo dos elementos contidos na lista.</typeparam>
    /// <remarks>
    /// Esta classe é usada para encapsular coleções de itens em um formato paginado, permitindo fácil navegação através de grandes conjuntos de dados.
    /// Inclui informações sobre a página atual, total de páginas, tamanho da página, e a contagem total de itens.
    /// </remarks>
    public class PagedList<T>
    {
        private readonly List<T> _items;

        /// <summary>
        /// Obtém os itens da página atual.
        /// </summary>
        public IEnumerable<T> Items => _items;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PagedList{T}"/> com os itens paginados e as informações de paginação calculadas automaticamente.
        /// </summary>
        /// <param name="items">Os itens contidos na página atual.</param>
        /// <param name="currentPage">O número da página atual.</param>
        /// <param name="pageSize">O número de itens por página.</param>
        /// <param name="totalCount">O número total de itens.</param>
        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
        {
            _items = new List<T>(items);
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="PagedList{T}"/> com os itens paginados e as informações de paginação fornecidas.
        /// </summary>
        /// <param name="items">Os itens contidos na página atual.</param>
        /// <param name="currentPage">O número da página atual.</param>
        /// <param name="totalPages">O número total de páginas.</param>
        /// <param name="pageSize">O número de itens por página.</param>
        /// <param name="totalCount">O número total de itens.</param>
        public PagedList(IEnumerable<T> items, int currentPage, int totalPages, int pageSize, int totalCount)
        {
            _items = new List<T>(items);
            CurrentPage = currentPage;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        /// <summary>
        /// Obtém o número da página atual.
        /// </summary>
        public int CurrentPage { get; }

        /// <summary>
        /// Obtém o número total de páginas.
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Obtém o número de itens por página.
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Obtém o número total de itens.
        /// </summary>
        public int TotalCount { get; }
    }
}
