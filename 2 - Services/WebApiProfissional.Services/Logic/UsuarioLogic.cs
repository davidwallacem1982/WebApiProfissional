using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Services.PreencherModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace WebApiProfissional.Services.Logic
{
    public class UsuarioLogic : IUsuarioLogic
    {
        private readonly ILogger<UsuarioLogic> _logger;
        private readonly IUsuarioServices _usuario;

        public UsuarioLogic(ILogger<UsuarioLogic> logger,
                            IUsuarioServices usuario)
        {
            _logger = logger;
            _usuario = usuario;
        }

        public async Task<bool> UserExistByLoginAsync(string login) => await _usuario.SelectExistByLoginAsync(login);

        public async Task<bool> UserExistByIdAsync(int id) => await _usuario.SelectUserExistByIdAsync(id);

        public async Task<Usuarios> GetUserByIdAsync(int id) => await _usuario.SelectUserByIdAsync(id);

        public async Task<Usuarios> GetUserByLoginAsync(string login) => await _usuario.SelectUserByLoginAsync(login);

        public async Task<Usuarios> IncluirUserAsync(NewUsuarioInput model)
        {
            var usuario = UsuarioPreencher.UsuarioWithNewUsuarioInput(model);
            return await _usuario.InsertUserAsync(usuario);
        }

        public async Task<Usuarios> AtualizarSenhaAsync(UpdateUsuarioInput model)
        {
            var usuario = await _usuario.SelectUserByIdAsync(model.Id);
            var update = UsuarioPreencher.UsuarioToUpdate(model.Password, usuario);
            return await _usuario.UpdateUserAsync(update);
        }
    }
}
