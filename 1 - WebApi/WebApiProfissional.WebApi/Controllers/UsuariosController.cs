using WebApiProfissional.CrossCutting.IoC.Claims;
using WebApiProfissional.Domain.InputModels.User;
using WebApiProfissional.Domain.InputModels.Usuarios;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Interfaces.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniValidation;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace WebApiProfissional.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(ILogger<UsuariosController> logger, IUsuarioLogic usuario, IAuthenticate authenticate, IRefreshTokenRepository refreshToken) : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger = logger;
        private readonly IUsuarioLogic _usuario = usuario;
        private readonly IAuthenticate _authenticate = authenticate;
        private readonly IRefreshTokenRepository _refreshToken = refreshToken;

        // POST api/<UsuariosController>
        [HttpPost("register")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Incluir([FromBody] NewUsuarioInput model)
        {
            if (model is null)
                return BadRequest("Dados inválidos");

            if (await _usuario.UserExistByLoginAsync(model.Login))
                return BadRequest("Este Login já possui um cadastro");

            var usuario = await _usuario.IncluirUserAsync(model);

            if (usuario is null)
                return BadRequest("Ocorreu um erro ao cadastrar");

            var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
            var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

            return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
        }

        // POST api/<UsuariosController>
        [HttpPost("sign-in")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Selecionar([FromBody] LoginUsuarioInput model)
        {
            if (!await _usuario.UserExistByLoginAsync(model.Login))
                return Unauthorized("O login é inválido");

            if (!await _authenticate.AuthenticateUserAsync(model.Login, model.Password))
                return Unauthorized("A senha é inválida");

            var usuario = await _usuario.GetUserByLoginAsync(model.Login);
            var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
            var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

            return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
        }

        [HttpPut("change-password")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> ChangePassword([FromBody] UpdateUsuarioInput model)
        {
            if (model is null)
                return BadRequest("Dados inválidos");

            var userId = User.GetId();

            var verifica = await _usuario.GetUserByIdAsync(userId);

            if (!verifica.IsAdmin)
                return Unauthorized("Você não tem permissão para atualizar um usuário");

            var usuario = await _usuario.AtualizarSenhaAsync(model);

            if (usuario is null)
                return BadRequest("Ocorreu um erro ao atualizar");

            var accesToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
            var refreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

            return new UserToken(accesToken, refreshToken, usuario.IsAdmin);
        }

        [HttpPost("refresh-token")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenInput model)
        {
            if (!MiniValidator.TryValidate(model, out var errors))
                return BadRequest("Dados inválidos");

            // Verifica se o RefreshToken foi revogado
            var isRevoked = await _authenticate.IsTokenRevoked(model.RefreshToken);

            if (isRevoked)
                return Unauthorized("O Refresh Token foi revogado.");

            // Obtém o usuário associado ao RefreshToken
            var tokenDetails = await _refreshToken.GetSingleOrDefaultAsyncBy(rt => rt.Token == model.RefreshToken);

            if (tokenDetails == null)
                return Unauthorized("Refresh Token inválido.");

            var usuario = await _usuario.GetUserByIdAsync(tokenDetails.IdUsuario);

            if (usuario == null)
                return Unauthorized("Usuário não encontrado.");

            // Gera novos tokens
            var newAccessToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
            var newRefreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

            // Revoga o RefreshToken antigo
            await _authenticate.WithRevokeRefreshToken(usuario.Login, model.RefreshToken);

            // Armazena o novo RefreshToken
            await _authenticate.StoreRefreshToken(usuario.Id, newRefreshToken, Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(1));

            var Token = new UserToken(newAccessToken, newRefreshToken, usuario.IsAdmin);

            return (IActionResult)Results.Ok(Token);
        }

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
            var tokenDetails = await _refreshToken.GetSingleOrDefaultAsyncBy(rt => rt.Token == model.RefreshToken);

            if (tokenDetails == null)
                return Unauthorized("Refresh Token inválido.");

            var usuario = await _usuario.GetUserByIdAsync(tokenDetails.IdUsuario);

            if (usuario == null)
                return Unauthorized("Usuário não encontrado.");

            // Gera novos tokens
            var newAccessToken = await _authenticate.GenerateAccesToken(usuario.Id, usuario.Login);
            var newRefreshToken = await _authenticate.GenerateRefreshToken(usuario.Id);

            // Revoga o RefreshToken antigo
            await _authenticate.WithRevokeRefreshToken(usuario.Login, model.RefreshToken);

            // Armazena o novo RefreshToken
            await _authenticate.StoreRefreshToken(usuario.Id, newRefreshToken, Guid.NewGuid().ToString(), DateTime.UtcNow.AddDays(1));

            return new UserToken(newAccessToken, newRefreshToken, usuario.IsAdmin);
        }


        [HttpPost("logout")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout([FromBody] LogoutInput model)
        {
            var userId = User.GetId();
            var refreshToken = model.RefreshToken;

            // Revoga o Refresh Token fornecido
            await _authenticate.RevokeRefreshToken(userId, refreshToken);

            return Ok("Logout realizado com sucesso.");
        }


    }
}
