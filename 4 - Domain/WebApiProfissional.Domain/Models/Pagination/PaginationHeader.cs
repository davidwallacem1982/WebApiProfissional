namespace WebApiProfissional.Domain.Models.Pagination
{
    /// <summary>
    /// Representa o cabeçalho de paginação contendo informações sobre a página atual, itens por página, total de itens e total de páginas.
    /// </summary>
    /// <remarks>
    /// Esta classe é utilizada para encapsular informações de paginação que podem ser retornadas em uma resposta HTTP ou usadas para fornecer
    /// detalhes de controle da paginação em uma interface de usuário ou API.
    /// </remarks>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="PaginationHeader"/> com a página atual, itens por página, total de itens e total de páginas.
    /// </remarks>
    /// <param name="currentPage">O número da página atual.</param>
    /// <param name="itensPerPage">O número de itens por página.</param>
    /// <param name="totalItens">O número total de itens.</param>
    /// <param name="totalPages">O número total de páginas.</param>
    public class PaginationHeader(int currentPage, int itensPerPage, int totalItens, int totalPages)
    {
        /// <summary>
        /// Obtém ou define o número da página atual.
        /// </summary>
        public int CurrentPage { get; set; } = currentPage;

        /// <summary>
        /// Obtém ou define o número de itens por página.
        /// </summary>
        public int ItensPerPage { get; set; } = itensPerPage;

        /// <summary>
        /// Obtém ou define o número total de itens.
        /// </summary>
        public int TotalItens { get; set; } = totalItens;

        /// <summary>
        /// Obtém ou define o número total de páginas.
        /// </summary>
        public int TotalPages { get; set; } = totalPages;
    }
}
