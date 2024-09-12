using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using WebApiProfissional.Domain.InputModels.Authentication;
using WebApiProfissional.Domain.InputModels.Funcionarios;
using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Models.Pagination;
using WebApiProfissional.Utils;
using WebApiProfissional.WebApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProfissional.WebApi.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas aos funcionários.
    /// Contém ações para registrar novos funcionários e paginar a lista de funcionários.
    /// Requer autenticação para acessar seus métodos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FuncionariosController : ControllerBase
    {
        private readonly ILogger<FuncionariosController> _logger;
        private readonly IFuncionariosLogic _funcionario;
        private readonly IAuthenticateLogic _authenticate;
        private readonly IUsuarioLogic _usuario;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthorized _authorized;
        private readonly IValidator<NewFuncionarioInput> _registerValidator;

        /// <summary>
        /// Construtor do controlador de funcionários.
        /// Inicializa as dependências injetadas via DI (Dependency Injection), como lógica de negócios de funcionários, autenticação, usuário, entre outros.
        /// </summary>
        /// <param name="logger">Logger para rastreamento e depuração de erros.</param>
        /// <param name="funcionario">Serviço que lida com a lógica de negócios de funcionários.</param>
        /// <param name="authenticate">Serviço responsável pela geração de tokens de autenticação.</param>
        /// <param name="usuario">Serviço que lida com a lógica de negócios de usuários.</param>
        /// <param name="httpContextAccessor">Acessor do contexto HTTP, utilizado para obter informações do contexto da requisição.</param>
        /// <param name="authorized">Serviço responsável por fornecer dados do usuário autorizado.</param>
        public FuncionariosController(ILogger<FuncionariosController> logger,
                                      IFuncionariosLogic funcionario,
                                      IAuthenticateLogic authenticate,
                                      IUsuarioLogic usuario,
                                      IHttpContextAccessor httpContextAccessor,
                                      IAuthorized authorized,
                                      IValidator<NewFuncionarioInput> registerValidator)
        {
            _logger = logger;
            _funcionario = funcionario;
            _authenticate = authenticate;
            _usuario = usuario;
            _httpContextAccessor = httpContextAccessor;
            _authorized = authorized;
            _registerValidator = registerValidator;
        }

        /// <summary>
        /// Registra um novo funcionário no sistema.
        /// Verifica se os dados são válidos e se o CPF já está cadastrado.
        /// Se o cadastro for bem-sucedido, gera tokens de acesso e de refresh para o usuário autorizado.
        /// </summary>
        /// <param name="model">Modelo de entrada contendo os dados do novo funcionário.</param>
        /// <returns>Retorna um UserToken contendo os tokens de acesso e de refresh, bem como a informação se o usuário é administrador.</returns>
        /// <response code="200">Cadastro realizado com sucesso.</response>
        /// <response code="400">Os dados fornecidos são inválidos ou o CPF já está cadastrado.</response>
        /// <response code="401">O usuário não está autorizado a acessar este recurso.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpPost("register")]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserToken>> Register([FromBody] NewFuncionarioInput model)
        {
            try
            {
                // Verifica se o modelo fornecido é nulo ou inválido
                if (model is null)
                    return BadRequest("Dados inválidos");

                var validationResult = await _registerValidator.ValidateAsync(model);

                if (validationResult.IsValid)
                {
                    // Realiza o cadastro do novo funcionário
                    var funcionario = await _funcionario.IncluirFuncionarioAsync(model);

                    // Obtém as informações do usuário autorizado
                    var user = _authorized.User(_usuario);

                    // Gera os tokens de acesso e de refresh para o usuário
                    var accesToken = await _authenticate.GenerateAccesToken(user.Id, user.Login);
                    var refreshToken = await _authenticate.GenerateRefreshToken(user.Id);

                    // Retorna os tokens gerados para o cliente
                    return new UserToken(accesToken, refreshToken, user.IsAdmin);
                }
                else
                {
                    return BadRequest(validationResult.Errors.Select(e => new { e.ErrorCode, e.ErrorMessage }));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(Util.ExceptionService(ex.Message, ex.StackTrace));
                throw new Exception(Util.ExceptionService(ex.Message, ex.StackTrace));
            }
        }

        /// <summary>
        /// Retorna uma lista paginada de funcionários.
        /// Adiciona um cabeçalho de paginação à resposta, indicando a página atual, o tamanho da página, o total de itens e o total de páginas.
        /// </summary>
        /// <param name="paginationParams">Parâmetros de paginação, como número da página e tamanho da página.</param>
        /// <returns>Uma lista paginada de funcionários.</returns>
        /// <response code="200">A paginação dos funcionários foi realizada com sucesso.</response>
        /// <response code="400">Os parâmetros de paginação fornecidos são inválidos.</response>
        /// <response code="401">O usuário não está autorizado a acessar este recurso.</response>
        /// <response code="500">Erro interno do servidor.</response>
        [HttpGet]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PaginarFuncionarios([FromQuery] PaginationParams paginationParams)
        {
            // Realiza a paginação dos funcionários com base nos parâmetros fornecidos
            var pagina = await _funcionario.PaginarFuncionariosAsync(paginationParams.PageNumber, paginationParams.PageSize);

            // Adiciona cabeçalho de paginação à resposta HTTP
            Response.AddPaginationHeader(new PaginationHeader(pagina.CurrentPage,
                                                              pagina.PageSize,
                                                              pagina.TotalCount,
                                                              pagina.TotalPages));

            // Retorna a lista paginada de funcionários
            return Ok(pagina);
        }
    }

}
