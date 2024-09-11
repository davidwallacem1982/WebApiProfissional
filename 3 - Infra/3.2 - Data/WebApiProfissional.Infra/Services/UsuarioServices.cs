using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.Interfaces.Repository;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WebApiProfissional.Infra.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ILogger<UsuarioServices> _logger;
        private readonly IUsuarioRepository _usuario;

        public UsuarioServices(ILogger<UsuarioServices> logger, IUsuarioRepository usuario)
        {
            _logger = logger;
            _usuario = usuario;
        }

        public bool SelectExistByLogin(string login)
        {
            try
            {
                _logger.LogInformation("SelectExistByLogin no Banco - Inicio");

                var result = _usuario.Exist(u => u.Login == login);

                _logger.LogInformation($"O SelectExistByLogin foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<bool> SelectExistByLoginAsync(string login)
        {
            try
            {
                _logger.LogInformation("SelectExistByLoginAsync no Banco - Inicio");

                var result = await _usuario.ExistAsync(u => u.Login == login);

                _logger.LogInformation($"O SelectExistByLoginAsync foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<bool> SelectUserExistByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("SelectUserExistByIdAsync no Banco - Inicio");

                var result = await _usuario.ExistAsync(u => u.Id == id);

                _logger.LogInformation($"O SelectUserExistByIdAsync foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<Usuarios> SelectUserByLoginAsync(string login)
        {
            try
            {
                _logger.LogInformation("SelectUserByLoginAsync no Banco - Inicio");

                var result = await _usuario.GetSingleOrDefaultAsyncBy(u => u.Login == login);

                _logger.LogInformation($"O SelectUserByLoginAsync foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<Usuarios> SelectUserByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("SelectUserByIdAsync no Banco - Inicio");

                var result = await _usuario.GetByIdAsync(id);

                _logger.LogInformation($"O SelectUserByIdAsync foi concluído - Select no Banco - Fim");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        public async Task<Usuarios> InsertUserAsync(Usuarios entity)
        {

            try
            {
                _logger.LogInformation("InsertUserAsync no Banco - Inicio");

                _usuario.Add(entity);
                await _usuario.SaveChangesAsync();

                _logger.LogInformation($"O InsertUserAsync foi concluído - Select no Banco - Fim");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }

        }

        public async Task<Usuarios> UpdateUserAsync(Usuarios entity)
        {
            try
            {
                _logger.LogInformation("UpdateUserAsync no Banco - Inicio");

                _usuario.Update(entity);
                await _usuario.SaveChangesAsync();

                _logger.LogInformation($"O UpdateUserAsync foi concluído - Select no Banco - Fim");
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
