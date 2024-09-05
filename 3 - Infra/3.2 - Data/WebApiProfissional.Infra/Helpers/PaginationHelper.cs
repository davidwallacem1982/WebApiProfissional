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
    public static class PaginationHelper
    {
        /// <summary>
        /// Criamos duas tarefas assíncronas para obter o total de itens (countTask) e os itens da página 
        /// atual (itemsTask).
        ///Usamos Task.WhenAll para esperar que ambas as tarefas sejam concluídas de forma paralela.
        ///Em seguida, obtemos o resultado das tarefas assíncronas.
        ///Assim, criamos e retornamos um novo objeto PagedList<T> com os itens e as informações de 
        ///paginação.Essa abordagem evita múltiplas execuções de consulta ao banco de dados, o que pode 
        ///melhorar significativamente o desempenho, especialmente em cenários onde o acesso ao banco de 
        ///dados é a parte mais lenta da operação.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<PagedList<T>> CreateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var itens = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PagedList<T>(itens, pageNumber, pageSize, count);
        }
    }
}
