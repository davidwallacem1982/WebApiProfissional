using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Domain.Pagination;
using WebApiProfissional.Domain.ViewModels.DropDown;
using WebApiProfissional.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProfissional.Services.Logic
{
    /// <summary>
    /// Classe responsável pela lógica de operações relacionadas aos funcionários.
    /// Implementa a interface <see cref="IFuncionariosLogic"/>.
    /// </summary>
    public class FuncionariosLogic : IFuncionariosLogic
    {
        private readonly ILogger<FuncionariosLogic> _logger;
        private readonly IFuncionariosServices _funcionarios;

        /// <summary>
        /// Construtor da classe <see cref="FuncionariosLogic"/>. Inicializa o logger e os serviços relacionados a funcionários.
        /// </summary>
        /// <param name="logger">Instância do logger para registrar informações.</param>
        /// <param name="funcionarios">Serviço responsável pelas operações de funcionários.</param>
        public FuncionariosLogic(ILogger<FuncionariosLogic> logger, IFuncionariosServices funcionarios)
        {
            _logger = logger;
            _funcionarios = funcionarios;
        }

        /// <summary>
        /// Obtém uma lista de todos os funcionários formatados para dropdown (com ID e nome completo).
        /// </summary>
        /// <returns>Retorna uma lista de <see cref="DropIntViewModel"/> representando os funcionários formatados para dropdown.</returns>
        public IList<DropIntViewModel> DropIntAllFuncionarios()
        {
            try
            {
                _logger.LogInformation("SelectAllDepartamento - Inicio");

                // Recupera a lista de funcionários e transforma em DropIntViewModel.
                var result = _funcionarios.SelectDropDownFuncionarios()
                                          .Select(s => new DropIntViewModel(s.Id, s.NomeCompleto))
                                          .OrderBy(s => s.Descricao)
                                          .ToList();

                _logger.LogInformation($"O método SelectAllDepartamento preencheu a model DropIntModel com {result.Count} itens - SelectAllDepartamento - Fim");
                return result;
            }
            catch (Exception ex)
            {
                // Log de erro e relançamento da exceção com informações adicionais.
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Obtém um funcionário específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do funcionário a ser recuperado.</param>
        /// <returns>Retorna um objeto <see cref="Funcionarios"/> representando o funcionário encontrado.</returns>
        public Funcionarios GetFuncionarioById(int id)
        {
            try
            {
                _logger.LogInformation("GetFuncionarioById - Inicio");

                // Recupera o funcionário pelo ID.
                var result = _funcionarios.SelectFuncionariosById(id);

                _logger.LogInformation("GetFuncionarioById - Fim");
                return result;
            }
            catch (Exception ex)
            {
                // Log de erro e relançamento da exceção com informações adicionais.
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Pagina a lista de funcionários de acordo com o número da página e o tamanho da página fornecidos.
        /// </summary>
        /// <param name="pageNumber">Número da página que será paginada.</param>
        /// <param name="pageSize">Quantidade de itens por página.</param>
        /// <returns>Retorna uma lista paginada de <see cref="Funcionarios"/>.</returns>
        public async Task<PagedList<Funcionarios>> PaginarFuncionariosAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("PaginarFuncionariosAsync - Inicio");

                // Realiza a paginação dos funcionários.
                var result = await _funcionarios.PaginationFuncionariosAsync(pageNumber, pageSize);

                _logger.LogInformation("PaginarFuncionariosAsync - Fim");
                return result;
            }
            catch (Exception ex)
            {
                // Log de erro e relançamento da exceção com informações adicionais.
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }
    }
}
