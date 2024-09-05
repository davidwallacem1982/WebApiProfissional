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
    public class FuncionariosLogic : IFuncionariosLogic
    {
        private readonly ILogger<FuncionariosLogic> _logger;
        private readonly IFuncionariosServices _funcionarios;

        public FuncionariosLogic(ILogger<FuncionariosLogic> logger, IFuncionariosServices funcionarios)
        {
            _logger = logger;
            _funcionarios = funcionarios;
        }

        public IList<DropIntViewModel> DropIntAllFuncionarios()
        {
            try
            {
                _logger.LogInformation("SelectAllDepartamento - Inicio");

                var result = _funcionarios.SelectDropDownFuncionarios()
                                         .Select(s => new DropIntViewModel(s.Id, s.NomeCompleto))
                                         .OrderBy(s => s.Descricao)
                                         .ToList();

                _logger.LogInformation($"O método SelectAllDepartamento preencheu a model DropIntModel com {result.Count} itens - SelectAllDepartamento - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public Funcionarios GetFuncionarioById(int id)
        {
            try
            {
                _logger.LogInformation("GetFuncionarioById - Inicio");

                var result = _funcionarios.SelectFuncionariosById(id);

                _logger.LogInformation("GetFuncionarioById - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<PagedList<Funcionarios>> PaginarFuncionariosAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("PaginarFuncionariosAsync - Inicio");
                var result = await _funcionarios.PaginationFuncionariosAsync(pageNumber, pageSize);
                _logger.LogInformation("PaginarFuncionariosAsync - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }
    }
}
