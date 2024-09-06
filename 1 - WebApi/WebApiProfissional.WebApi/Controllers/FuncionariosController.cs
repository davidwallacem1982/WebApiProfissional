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
    public class FuncionariosController : ControllerBase
    {
        private readonly ILogger<FuncionariosController> _logger;
        private readonly IFuncionariosLogic _funcionario;
        private readonly IAuthenticate _authenticate;

        public FuncionariosController(ILogger<FuncionariosController> logger, IFuncionariosLogic funcionario, IAuthenticate authenticate)
        {
            _logger = logger;
            _funcionario = funcionario;
            _authenticate = authenticate;
        }

        // GET api/<FuncionariosController>/5
        [HttpGet]
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PaginarFuncionarios([FromQuery]PaginationParams paginationParams)
        {
            var pagina = await _funcionario.PaginarFuncionariosAsync(paginationParams.PageNumber, paginationParams.PageSize); ;
            Response.AddPaginationHeader(new PaginationHeader(pagina.CurrentPage, 
                                                              pagina.PageSize, 
                                                              pagina.TotalCount, 
                                                              pagina.TotalPages));
            return Ok(pagina);
        }
    }
}
