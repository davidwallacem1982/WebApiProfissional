﻿using WebApiProfissional.Domain.Entities;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Services;
using WebApiProfissional.Services.PreencherModels;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System;

namespace WebApiProfissional.Services.Logic
{
    /// <summary>
    /// Classe responsável pela lógica de operações relacionadas a usuários.
    /// Implementa a interface <see cref="IUsuarioLogic"/>.
    /// </summary>
    public class UsuarioLogic : IUsuarioLogic
    {
        private readonly ILogger<UsuarioLogic> _logger;
        private readonly IUsuarioServices _usuario;

        /// <summary>
        /// Construtor da classe <see cref="UsuarioLogic"/>. Inicializa o logger e os serviços relacionados a usuários.
        /// </summary>
        /// <param name="logger">Instância do logger para registrar informações.</param>
        /// <param name="usuario">Serviço responsável pelas operações de usuário.</param>
        public UsuarioLogic(ILogger<UsuarioLogic> logger, IUsuarioServices usuario)
        {
            _logger = logger;
            _usuario = usuario;
        }

        public bool UserExistByLogin(string login) => _usuario.SelectExistByLogin(login);

        /// <summary>
        /// Verifica se um usuário existe no banco de dados com base no login fornecido.
        /// </summary>
        /// <param name="login">O login do usuário a ser verificado.</param>
        /// <returns>Retorna true se o usuário existir, caso contrário, false.</returns>
        public async Task<bool> UserExistByLoginAsync(string login)
        {
            var result = await _usuario.SelectExistByLoginAsync(login);

            return !result ? throw new InvalidOperationException("Login inválido") : result;
        }

        /// <summary>
        /// Verifica se um usuário existe no banco de dados com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser verificado.</param>
        /// <returns>Retorna true se o usuário existir, caso contrário, false.</returns>
        public async Task<bool> UserExistByIdAsync(int id) => await _usuario.SelectUserExistByIdAsync(id);

        /// <summary>
        /// Obtém um usuário com base no ID fornecido.
        /// </summary>
        /// <param name="id">O ID do usuário a ser recuperado.</param>
        /// <returns>Retorna um objeto <see cref="Usuarios"/> se o usuário for encontrado.</returns>
        public async Task<Usuarios> GetUserByIdAsync(int id) 
        {
            var result = await _usuario.SelectUserByIdAsync(id);

            return !result.IsAdmin ? throw new InvalidOperationException("Você não tem permissão para atualizar um usuário") : result;
        }

        /// <summary>
        /// Obtém um usuário com base no login fornecido.
        /// </summary>
        /// <param name="login">O login do usuário a ser recuperado.</param>
        /// <returns>Retorna um objeto <see cref="Usuarios"/> se o usuário for encontrado.</returns>
        public async Task<Usuarios> GetUserByLoginAsync(string login) => await _usuario.SelectUserByLoginAsync(login);

        /// <summary>
        /// Insere um novo usuário no banco de dados com base nas informações fornecidas.
        /// Gera o hash e o salt da senha do usuário e preenche os demais campos necessários.
        /// </summary>
        /// <param name="model">Modelo <see cref="NewUsuarioInput"/> com as informações do novo usuário.</param>
        /// <returns>Retorna o objeto <see cref="Usuarios"/> inserido no banco de dados.</returns>
        public async Task<Usuarios> IncluirUserAsync(NewUsuarioInput model)
        {
            // Preenche o objeto de usuário com as informações fornecidas no modelo de entrada.
            var usuario = UsuarioPreencher.UsuarioWithNewUsuarioInput(model);

            return usuario is null
                ? throw new InvalidOperationException("Ocorreu um erro ao cadastrar")
                : await _usuario.InsertUserAsync(usuario);
        }

        /// <summary>
        /// Atualiza a senha de um usuário existente no banco de dados.
        /// Gera um novo hash e salt para a senha fornecida e atualiza o registro do usuário.
        /// </summary>
        /// <param name="model">Modelo <see cref="UpdateUsuarioInput"/> contendo o ID do usuário e a nova senha.</param>
        /// <returns>Retorna o objeto <see cref="Usuarios"/> atualizado no banco de dados.</returns>
        public async Task<Usuarios> AtualizarSenhaAsync(UpdateUsuarioInput model)
        {
            // Recupera o usuário existente pelo ID.
            var usuario = await _usuario.SelectUserByIdAsync(model.Id);

            // Atualiza o objeto de usuário com a nova senha.
            var update = UsuarioPreencher.UsuarioToUpdate(model.Password, usuario);
            var result = await _usuario.UpdateUserAsync(update);

            return result is null ? throw new InvalidOperationException("Ocorreu um erro ao atualizar") : result;
        }
    }

}
