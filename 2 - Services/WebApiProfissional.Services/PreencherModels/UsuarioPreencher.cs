using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Utils;
using System;

namespace WebApiProfissional.Services.PreencherModels
{
    public static class UsuarioPreencher
    {
        /// <summary>
        /// Constrói um objeto <see cref="Usuarios"/> a partir de um modelo <see cref="NewUsuarioInput"/>.
        /// Este método gera o hash e o salt da senha e preenche as propriedades do objeto de usuário.
        /// </summary>
        /// <param name="model">O modelo <see cref="NewUsuarioInput"/> contendo as informações de entrada do usuário.</param>
        /// <returns>Um objeto <see cref="Usuarios"/> com as informações preenchidas a partir do modelo.</returns>
        public static Usuarios UsuarioWithNewUsuarioInput(NewUsuarioInput model)
        {
            // Gera o hash e o salt da senha do usuário.
            var password = PasswordHelper.GerarHashAndSalt(model.Password);
            var passwordHash = password.Hash;
            var passwordSalt = password.Salt;

            // Cria um novo objeto de usuário preenchido com as informações fornecidas no modelo.
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

        /// <summary>
        /// Constrói um objeto <see cref="NewUsuarioInput"/> a partir de um modelo <see cref="Usuarios"/>.
        /// Este método é útil para mapear as propriedades de um usuário existente para um novo modelo de entrada.
        /// </summary>
        /// <param name="model">O modelo <see cref="Usuarios"/> contendo as informações do usuário.</param>
        /// <returns>Um objeto <see cref="NewUsuarioInput"/> com as informações mapeadas a partir do usuário existente.</returns>
        public static NewUsuarioInput NewUsuarioInputWithUsuario(Usuarios model)
        {
            // Mapeia as propriedades do objeto de usuário para um novo modelo de entrada.
            var usuario = new NewUsuarioInput(model.Id, model.Login, model.PasswordHash.ToString(), model.IdFuncionario);

            return usuario;
        }

        /// <summary>
        /// Atualiza um objeto <see cref="Usuarios"/> com uma nova senha.
        /// Este método gera um novo hash e salt para a senha e atualiza as propriedades do usuário.
        /// </summary>
        /// <param name="password">A nova senha a ser aplicada ao usuário.</param>
        /// <param name="model">O objeto <see cref="Usuarios"/> que será atualizado.</param>
        /// <returns>O objeto <see cref="Usuarios"/> com as informações atualizadas.</returns>
        public static Usuarios UsuarioToUpdate(string password, Usuarios model)
        {
            // Gera o novo hash e salt para a senha.
            var newPassword = PasswordHelper.GerarHashAndSalt(password);
            var passwordHash = newPassword.Hash;
            var passwordSalt = newPassword.Salt;

            // Atualiza as propriedades do objeto de usuário com a nova senha e data de modificação.
            model.PasswordHash = passwordHash;
            model.PasswordSalt = passwordSalt;
            model.DtModificacao = DateTime.Now;

            return model;
        }
    }

}
