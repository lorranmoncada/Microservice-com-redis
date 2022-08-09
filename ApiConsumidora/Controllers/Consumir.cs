using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiConsumidora.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Consumir : ControllerBase
    {

        private readonly ILogger<Conteudo> _logger;

        public Consumir(ILogger<Conteudo> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("obter-resultado")]
        public async Task<IActionResult> Get()
        {
            return Ok(new Conteudo
            {
                Descicao = "Conteudo retornado!"

            });
        }
    }
}
