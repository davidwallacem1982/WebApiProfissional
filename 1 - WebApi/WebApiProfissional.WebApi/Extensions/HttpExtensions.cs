using WebApiProfissional.Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WebApiProfissional.WebApi.Extensions
{
    /// <summary>
    /// Contém métodos de extensão para o tipo <see cref="HttpResponse"/>.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Adiciona um cabeçalho de paginação à resposta HTTP. O cabeçalho inclui informações sobre a paginação, como
        /// número da página, tamanho da página e total de itens. Esse cabeçalho é usado para fornecer informações adicionais
        /// sobre a paginação dos dados em uma resposta de API.
        /// </summary>
        /// <param name="response">A resposta HTTP à qual o cabeçalho de paginação será adicionado.</param>
        /// <param name="header">O objeto <see cref="PaginationHeader"/> contendo as informações de paginação.</param>
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            // Configura opções de serialização JSON para usar a política de nomenclatura em camel case.
            JsonSerializerOptions jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonOptions = jsonSerializerOptions;

            // Serializa o objeto PaginationHeader para JSON e adiciona ao cabeçalho da resposta.
            response.Headers.Append("Pagination", JsonSerializer.Serialize(header, jsonOptions));

            // Adiciona o cabeçalho "Acess-Control-Expose-Headers" para garantir que o cabeçalho "Pagination" seja exposto
            // nas respostas CORS.
            response.Headers.Append("Acess-Control-Expose-Headers", "Pagination");
        }
    }

}
