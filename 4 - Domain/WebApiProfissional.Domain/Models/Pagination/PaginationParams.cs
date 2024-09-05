using System.ComponentModel.DataAnnotations;

namespace WebApiProfissional.Domain.Models.Pagination
{
    /// <summary>
    /// Representa os parâmetros de paginação usados para controlar o número da página e o tamanho da página em uma consulta paginada.
    /// </summary>
    /// <remarks>
    /// Esta classe é utilizada para definir os parâmetros de controle de paginação, incluindo o número da página e a quantidade de itens por página.
    /// Ela também inclui validações que garantem que os valores estão dentro dos limites especificados.
    /// </remarks>
    /// <remarks>
    /// Inicializa uma nova instância da classe <see cref="PaginationParams"/> com o número da página e o tamanho da página especificados.
    /// </remarks>
    public class PaginationParams
    {
        /// <summary>
        /// Obtém ou define o número da página atual. Deve ser um valor maior ou igual a 1.
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        /// <summary>
        /// Obtém ou define o número de itens por página. O valor máximo permitido é 50.
        /// </summary>
        [Range(1, 50, ErrorMessage = "O Máximo de itens por página é 50")]
        public int PageSize { get; set; }
    }
}
