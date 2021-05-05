using Dominio.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstatisticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnaliseCombinatoriaController : ControllerBase
    {
        private readonly ILogger<AnaliseCombinatoriaController> _logger;

       public AnaliseCombinatoriaController(ILogger<AnaliseCombinatoriaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Anagramas/{id}")]
        public IActionResult Anagramas(string id)
        {
            string retorno;
            try
            {
                retorno = JsonConvert.SerializeObject(new AnaliseCombinatoria().Anagramas(id), new Newtonsoft.Json.Converters.StringEnumConverter());
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(new { retorno });
        }
    }
}
