using WebApiProfissional.Domain.Interfaces.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiProfissional.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SistemaController : ControllerBase
    {
        //private readonly ILogger<UsuariosController> _logger;
        //private readonly IUsuarioLogic _usuario;

        // GET api/<SistemaController>/5
        [HttpGet("VerificaPrimeiroUso")]
        public string PrimeiroUso()
        {
            return "value";
        }

        // POST api/<SistemaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SistemaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SistemaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
