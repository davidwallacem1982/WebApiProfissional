using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Utils;
using System;

namespace WebApiProfissional.Services.PreencherModels
{
    public static class UsuarioPreencher
    {
        public static Usuarios UsuarioWithNewUsuarioInput(NewUsuarioInput model)
        {
            var password = PasswordHelper.GerarHashAndSalt(model.Password);
            var passwordHash = password.Hash;
            var passwordSalt = password.Salt;

            var usuario = new Usuarios()
            {
                Login = model.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DtCadastro = DateTime.Now,
                IdFuncionario = model.IdFuncionario
            };

            return usuario;
        }

        public static NewUsuarioInput NewUsuarioInputWithUsuario(Usuarios model)
        {
            var usuario = new NewUsuarioInput(model.Id, model.Login, model.PasswordHash.ToString(), model.IdFuncionario);

            return usuario;
        }

        public static Usuarios UsuarioToUpdate(string password, Usuarios model)
        {
            var newPassword = PasswordHelper.GerarHashAndSalt(password);
            var passwordHash = newPassword.Hash;
            var passwordSalt = newPassword.Salt;

            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;
            model.DtModificacao = DateTime.Now;

            return model;
        }
    }
}
