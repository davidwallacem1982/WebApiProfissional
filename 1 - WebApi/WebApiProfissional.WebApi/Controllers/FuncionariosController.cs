using WebApiProfissional.Domain.Interfaces.Account;
using WebApiProfissional.Domain.Interfaces.Logic;
using WebApiProfissional.Domain.Models.Pagination;
using WebApiProfissional.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProfissional.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    /// <summary>
    /// Controlador para operações relacionadas aos funcionários. Fornece um endpoint para recuperar uma lista
    /// paginada de funcionários.
    /// </summary>
    public class FuncionariosController : ControllerBase
    {
        private readonly ILogger<FuncionariosController> _logger;
        private readonly IFuncionariosLogic _funcionario;
        private readonly IAuthenticate _authenticate;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="FuncionariosController"/> com os serviços necessários.
        /// </summary>
        /// <param name="logger">O logger para registrar mensagens de log.</param>
        /// <param name="funcionario">A lógica de negócios relacionada aos funcionários.</param>
        /// <param name="authenticate">O serviço de autenticação para verificar a identidade dos usuários.</param>
        public FuncionariosController(ILogger<FuncionariosController> logger, IFuncionariosLogic funcionario, IAuthenticate authenticate)
        {
            _logger = logger;
            _funcionario = funcionario;
            _authenticate = authenticate;
        }

        /// <summary>
        /// Obtém uma lista paginada de funcionários. Este método é protegido por autenticação e retorna a lista de funcionários
        /// com base nos parâmetros de paginação fornecidos na consulta. Também adiciona um cabeçalho de paginação à resposta
        /// para fornecer informações sobre a paginação dos dados.
        /// </summary>
        /// <param name="paginationParams">Os parâmetros de paginação, incluindo o número da página e o tamanho da página.</param>
        /// <returns>Um <see cref="IActionResult"/> contendo a lista paginada de funcionários.</returns>
        /// <response code="200">Retorna a lista paginada de funcionários.</response>
        /// <response code="400">Requisição inválida devido a parâmetros incorretos.</response>
        /// <response code="401">Não autorizado, autenticação necessária.</response>
        /// <response code="500">Erro interno do servidor ao processar a solicitação.</response>
        [HttpGet]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PaginarFuncionarios([FromQuery] PaginationParams paginationParams)
        {
            var pagina = await _funcionario.PaginarFuncionariosAsync(paginationParams.PageNumber, paginationParams.PageSize);
            Response.AddPaginationHeader(new PaginationHeader(pagina.CurrentPage,
                                                              pagina.PageSize,
                                                              pagina.TotalCount,
                                                              pagina.TotalPages));
            return Ok(pagina);
        }
    }

}
