using WebApiProfissional.Domain.Models.Pagination;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace WebApiProfissional.WebApi.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, PaginationHeader header)
        {
            JsonSerializerOptions jsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonOptions = jsonSerializerOptions;
            response.Headers.Append("Pagination", JsonSerializer.Serialize(header, jsonOptions));
            response.Headers.Append("Acess-Control-Expose-Headers", "Pagination");
        }
    }
}
