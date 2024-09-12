using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniValidation;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebApiProfissional.Domain.InputModels.Authentication;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Utils;

namespace WebApiProfissional.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    /// Controlador para operações relacionadas aos usuários, incluindo registro, login, atualização de senha, 
    /// renovação de tokens e logout. Protege endpoints com autenticação e gerencia a geração e revogação de tokens.
    /// </summary>
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuarioLogic _usuario;
        private readonly IAuthenticateLogic _authenticate;
        private readonly IRefreshTokenLogic _refreshToken;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorized _authorized;
        private readonly IValidator<NewUsuarioInput> _registerValidator;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="UsuariosController"/> com os serviços necessários.
        /// </summary>
        /// <param name="logger">O logger para registrar mensagens de log.</param>
        /// <param name="usuario">A lógica de negócios relacionada aos usuários.</param>
        /// <param name="authenticate">O serviço de autenticação para verificar e gerar tokens.</param>
        /// <param name="refreshToken">A lógica de negócios relacionada aos Refresh Tokens.</param>
        public UsuariosController(ILogger<UsuariosController> logger, IUsuarioLogic usuario, IAuthenticateLogic authenticate, IRefreshTokenLogic refreshToken, IHttpContextAccessor httpContextAccessor, IAuthorized authorized, IValidator<NewUsuarioInput> registerValidator)
        {
            _logger = logger;
            _usuario = usuario;
            _authenticate = authenticate;
            _refreshToken = refreshToken;
            _httpContextAccessor = httpContextAccessor;
            _authorized = authorized;
            _registerValidator = registerValidator;
        }

        /// <summary>
        /// Registra um novo usuário. Gera um Access Token e um Refresh Token após a criação do usuário.
        /// </summary>
        /// <param name="model">Os dados do novo usuário a serem registrados.</param>
        /// <returns>Um <see cref="ActionResult{UserToken}"/> contendo o Access Token e o Refresh Token do novo usuário.</returns>
        /// <response code="200">Retorna o Access Token e o Refresh Token do usuário registrado.</response>
        /// <response code="400">Dados inválidos ou login já cadastrado.</response>
        /// <response code="401">Não autorizado, autenticação necessária.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPost("register")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Register([FromBody] NewUsuarioInput model)
        {
            try
            {
                var validationResult = await _registerValidator.ValidateAsync(model);

                if (validationResult.IsValid)
                {
                    var usuario = await _usuario.IncluirUserAsync(model);

                    var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
                    var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

                    return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
                }
                else
                {
                    return BadRequest(validationResult.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Realiza o login de um usuário. Gera um Access Token e um Refresh Token após a autenticação bem-sucedida.
        /// </summary>
        /// <param name="model">Os dados de login do usuário.</param>
        /// <returns>Um <see cref="ActionResult{UserToken}"/> contendo o Access Token e o Refresh Token.</returns>
        /// <response code="200">Retorna o Access Token e o Refresh Token para o usuário autenticado.</response>
        /// <response code="400">Dados inválidos ou login/senha incorretos.</response>
        /// <response code="401">Não autorizado, login ou senha inválidos.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPost("sign-in")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> SignIn([FromBody] LoginUsuarioInput model)
        {
            try
            {
                await _usuario.UserExistByLoginAsync(model.Login);

                await _authenticate.AuthenticateUserAsync(model.Login, model.Password);

                var usuario = await _usuario.GetUserByLoginAsync(model.Login);
                var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
                var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

                return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }            
        }

        /// <summary>
        /// Atualiza a senha do usuário autenticado. Gera novos tokens após a atualização bem-sucedida.
        /// </summary>
        /// <param name="model">Os dados para atualizar a senha.</param>
        /// <returns>Um <see cref="ActionResult{UserToken}"/> contendo o novo Access Token e o novo Refresh Token.</returns>
        /// <response code="200">Retorna o novo Access Token e o novo Refresh Token.</response>
        /// <response code="400">Dados inválidos ou erro ao atualizar a senha.</response>
        /// <response code="401">Não autorizado, usuário não tem permissão para atualizar.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPut("change-password")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> ChangePassword([FromBody] UpdateUsuarioInput model)
        {
            try
            {
                if (model is null)
                    return BadRequest("Dados inválidos");

                var user = _authorized.User(_usuario);

                await _usuario.GetUserByIdAsync(user.Id);

                var usuario = await _usuario.AtualizarSenhaAsync(model);

                var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
                var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

                return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza o Refresh Token do usuário. Verifica se o Refresh Token é válido e não foi revogado.
        /// </summary>
        /// <param name="model">Os dados do Refresh Token.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo o novo Access Token e o novo Refresh Token.</returns>
        /// <response code="200">Retorna o novo Access Token e o novo Refresh Token.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autorizado, Refresh Token inválido ou revogado.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPost("refresh-token")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> RefreshToken([FromBody] RefreshTokenInput model)
        {
            if (!MiniValidator.TryValidate(model, out var errors))
                return BadRequest("Dados inválidos");

            // Verifica se o RefreshToken foi revogado
            var isRevoked = await _authenticate.IsTokenRevoked(model.RefreshToken);

            if (isRevoked)
                return Unauthorized("O Refresh Token foi revogado.");

            // Obtém o usuário associado ao RefreshToken
            var tokenDetails = await _refreshToken.GetAssociarRefreshToken(model);

            if (tokenDetails == null)
                return Unauthorized("Refresh Token inválido.");

            var user = _authorized.User(_usuario);

            if (user == null)
                return Unauthorized("Usuário não encontrado.");

            // Gera novos tokens
            var newAccessToken = await _authenticate.GenerateAccesToken(user.Id, user.Login);
            var newRefreshToken = await _authenticate.GenerateRefreshToken(user.Id);

            // Revoga o RefreshToken antigo
            await _authenticate.WithRevokeRefreshToken(user.Login, model.RefreshToken);

            // Armazena o novo RefreshToken
            await _authenticate.StoreRefreshToken(user.Id, newRefreshToken, Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(1));

            return Ok(new UserToken(newAccessToken, newRefreshToken, user.IsAdmin));
        }

        /// <summary>
        /// Atualiza o Refresh Token do usuário usando o banco de dados. Verifica se o Refresh Token é válido e não foi revogado.
        /// </summary>
        /// <param name="model">Os dados do Refresh Token.</param>
        /// <returns>Um <see cref="ActionResult{UserToken}"/> contendo o novo Access Token e o novo Refresh Token.</returns>
        /// <response code="200">Retorna o novo Access Token e o novo Refresh Token.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autorizado, Refresh Token inválido ou revogado.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPost("refresh-token-data-base")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> RefreshTokenDataBase([FromBody] RefreshTokenInput model)
        {
            if (!MiniValidator.TryValidate(model, out var errors))
                return BadRequest("Dados inválidos");

            // Verifica se o RefreshToken foi revogado
            var isRevoked = await _authenticate.IsTokenRevoked(model.RefreshToken);

            if (isRevoked)
                return Unauthorized("O Refresh Token foi revogado.");

            // Obtém o usuário associado ao RefreshToken
            var tokenDetails = await _refreshToken.GetAssociarRefreshToken(model);

            var user = _authorized.User(_usuario);

            // Gera novos tokens
            var newAccessToken = await _authenticate.GenerateAccesToken(user.Id, user.Login);
            var newRefreshToken = await _authenticate.GenerateRefreshToken(user.Id);

            // Revoga o RefreshToken antigo
            await _authenticate.WithRevokeRefreshToken(user.Login, model.RefreshToken);

            // Armazena o novo RefreshToken
            await _authenticate.StoreRefreshToken(user.Id, newRefreshToken, Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(1));

            return Ok(new UserToken(newAccessToken, newRefreshToken, user.IsAdmin));
        }

        /// <summary>
        /// Realiza o logout do usuário autenticado revogando o Refresh Token fornecido.
        /// </summary>
        /// <param name="model">Os dados do Refresh Token para revogação.</param>
        /// <returns>Um <see cref="IActionResult"/> indicando sucesso no logout.</returns>
        /// <response code="200">Logout realizado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        /// <response code="401">Não autorizado, Refresh Token inválido.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpPost("logout")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout([FromBody] LogoutInput model)
        {
            var userId = _authorized.GetId();
            var refreshToken = model.RefreshToken;

            // Revoga o Refresh Token fornecido
            await _authenticate.RevokeRefreshToken(userId, refreshToken);

            return Ok("Logout realizado com sucesso.");
        }
    }
}
