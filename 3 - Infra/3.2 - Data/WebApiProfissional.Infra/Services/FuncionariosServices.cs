using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Domain.Pagination;
using WebApiProfissional.Infra.Helpers;
using WebApiProfissional.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProfissional.Infra.Services
{
    public class FuncionariosServices(ILogger<FuncionariosServices> logger, IFuncionariosRepository funcionarios) : IFuncionariosServices
    {
        private readonly ILogger<FuncionariosServices> _logger = logger;
        private readonly IFuncionariosRepository _funcionarios = funcionarios;

        public Funcionarios SelectFuncionariosById(int id)
        {
            try
            {
                _logger.LogInformation("Select no Banco - Inicio");

                var funcionario = _funcionarios.GetById(id);


                var result = new Funcionarios
                    {
                        Id = funcionario.Id,
                        Nome = funcionario.Nome,
                        Sobrenome = funcionario.Sobrenome
                };

                _logger.LogInformation($"O Select retornou o Funcionário {result.NomeCompleto} - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro durante o Select no Banco: {ex.Message}\nStack Trace: {ex.StackTrace}");
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public IList<Funcionarios> SelectAllFuncionarios()
        {
            _logger.LogInformation("Select no Banco - Inicio");
            var result = new List<Funcionarios>();

            try
            {
                result = [.. _funcionarios.GetList()];

            }
            catch (Exception ex)
            {
                _logger.LogError($"O Select retornou {result.Count} resultados - Select no Banco - Fim");
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }

            _logger.LogInformation($"O Select retornou {result.Count} resultados - Select no Banco - Fim");
            return result;
        }

        public IList<Funcionarios> SelectDropDownFuncionarios()
        {
            try
            {
                _logger.LogInformation("Select no Banco - Inicio");

                var result = _funcionarios.GetList()
                    .OrderBy(s => s.Nome)
                    .Select(s => new Funcionarios
                    {
                        Id = s.Id,
                        Nome = s.Nome,
                        Sobrenome = s.Sobrenome
                    })
                    .ToList();

                _logger.LogInformation($"O Select retornou {result.Count} resultados - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<PagedList<Funcionarios>> PaginationFuncionariosAsync(int pageNumber, int pageSize)
        {
            try
            {
                _logger.LogInformation("Select no Banco - Inicio");

                var query = _funcionarios.GetList()
                                         .OrderBy(s => s.Nome)
                                         .Select(s => new Funcionarios
                                         {
                                             Id = s.Id,
                                             Nome = s.Nome,
                                             Sobrenome = s.Sobrenome,
                                             Cpf = s.Cpf,
                                             DtNascimento = s.DtNascimento
                                         });

                _logger.LogInformation($"O Select retornou {query.Count()} resultados - Select no Banco - Fim");
                return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }            
        }

        public bool SelectFuncionarioExistByCpf(long cpf)
        {
            try
            {
                _logger.LogInformation("SelectFuncionarioExistByCpf no Banco - Inicio");

                var result = _funcionarios.Exist(u => u.Cpf == cpf);

                _logger.LogInformation($"O SelectFuncionarioExistByCpf foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<bool> SelectFuncionarioExistByCpfAsync(long cpf)
        {
            try
            {
                _logger.LogInformation("SelectFuncionarioExistByLoginAsync no Banco - Inicio");

                var result = await _funcionarios.ExistAsync(u => u.Cpf == cpf);

                _logger.LogInformation($"O SelectFuncionarioExistByLoginAsync foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<Funcionarios> InsertFuncionarioAsync(Funcionarios entity)
        {

            try
            {
                _logger.LogInformation("InsertFuncionarioAsync no Banco - Inicio");

                _funcionarios.Add(entity);
                await _funcionarios.SaveChangesAsync();

                _logger.LogInformation($"O InsertFuncionarioAsync foi concluído - Select no Banco - Fim");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }

        }
    }
}
