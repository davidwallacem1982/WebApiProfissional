using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Pagination;
using WebApiProfissional.Domain.ViewModels.DropDown;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiProfissional.Domain.InputModels.Funcionarios;

namespace WebApiProfissional.Domain.Interfaces.Logic
{
    public interface IFuncionariosLogic
    {

        /// <summary>
        /// Recupera uma lista de funcionários formatada para exibição em um dropdown, convertendo os dados para o modelo <see cref="DropIntViewModel"/>.
        /// Ordena os itens pela descrição e retorna a lista resultante.
        /// </summary>
        /// <returns>Uma lista de <see cref="DropIntViewModel"/> contendo os funcionários.</returns>
        /// <exception cref="Exception">Lança uma exceção com detalhes se ocorrer um erro durante a operação.</exception>
        IList<DropIntViewModel> DropIntAllFuncionarios();

        /// <summary>
        /// Recupera um funcionário específico com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do funcionário a ser recuperado.</param>
        /// <returns>Um objeto <see cref="Funcionarios"/> correspondente ao ID fornecido.</returns>
        /// <exception cref="Exception">Lança uma exceção com detalhes se ocorrer um erro durante a recuperação.</exception>
        Funcionarios GetFuncionarioById(int id);

        /// <summary>
        /// Realiza a paginação dos funcionários e retorna uma lista paginada com base no número da página e no tamanho da página fornecidos.
        /// </summary>
        /// <param name="pageNumber">O número da página para a paginação.</param>
        /// <param name="pageSize">O tamanho da página para a paginação.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona, contendo uma instância de <see cref="PagedList{Funcionarios}"/> com a lista paginada de funcionários.</returns>
        /// <exception cref="Exception">Lança uma exceção com detalhes se ocorrer um erro durante a paginação.</exception>
        Task<PagedList<Funcionarios>> PaginarFuncionariosAsync(int pageNumber, int pageSize);

        Task<bool> GetFuncionarioExistByCpfAsync(long cpf);

        Task<Funcionarios> IncluirFuncionarioAsync(NewFuncionarioInput model);
    }
}
