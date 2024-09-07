using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.ViewModels;

namespace WebApiProfissional.WebApi.Configurations
{
    /// <summary>
    /// Classe responsável por fornecer funcionalidades relacionadas ao usuário autorizado.
    /// Implementa a interface IAuthorized para expor os métodos de obtenção de dados do usuário.
    /// Utiliza o IHttpContextAccessor para acessar as informações do usuário autenticado no contexto HTTP.
    /// </summary>
    public class Authorized : IAuthorized
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Construtor da classe Authorized.
        /// Inicializa uma nova instância com o IHttpContextAccessor para acessar o contexto HTTP.
        /// </summary>
        /// <param name="httpContextAccessor">Interface para acessar o contexto HTTP e obter dados do usuário autenticado.</param>
        public Authorized(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetId()
        {
            var claimsArray = _httpContextAccessor.HttpContext?.User.Claims.ToArray();

            // Verifica se os claims estão presentes e tenta converter o valor do primeiro claim para um inteiro
            if (claimsArray?.Length > 0 && int.TryParse(claimsArray[0]?.Value, out var userId))
            {
                return userId;
            }
            throw new InvalidOperationException("Não foi possível obter o ID do usuário.");
        }

        public UserAuthorizedViewModel User(IUsuarioLogic _usuario)
        {
            // Obtém o ID do usuário autenticado usando o método GetId
            var authorized = new Authorized(_httpContextAccessor);
            var id = authorized.GetId();

            // Busca o usuário no banco de dados de forma síncrona
            var userTask = _usuario.GetUserByIdAsync(id);
            userTask.Wait();

            var user = userTask.Result;

            if (user == null)
            {
                throw new InvalidOperationException("Usuário não encontrado.");
            }

            return new UserAuthorizedViewModel(user.Id, user.Login, user.IsAdmin);
        }
    }
}
