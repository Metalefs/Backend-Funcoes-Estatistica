using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estatistica101;

namespace EstatisticaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuscaController : Controller
    {
        private readonly ILogger<BuscaController> _logger;

        public BuscaController(ILogger<BuscaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult TabelaDistribuicao(string Valores)
        {
            string retorno = JsonConvert.SerializeObject(Estatistica.ObterTabelaDistribuicao(Valores));
            return Json(retorno);
        }
        [HttpGet]
        public string TextoTabelaDistribuicao(string Valores)
        { 
            return Estatistica.ObterTextoTabelaDistribuicao(Valores);
        }
        [HttpGet]
        public string ObterDesvioPadrao(string Valores)
        { 
            return Estatistica.ObterDesvioPadrao(Valores);
        }
        [HttpGet]
        public string ObterVariancia(string Valores)
        {
            return Estatistica.ObterVariancia(Valores);
        }
        [HttpGet]
        public string ObterMedia(string Valores)
        {
            return Estatistica.ObterMedia(Valores);
        }
        [HttpGet]
        public string ObterModa(string Valores)
        { 
            return Estatistica.ObterModa(Valores);
        }
        [HttpGet]
        public string ObterMediana(string Valores)
        {
            return Estatistica.ObterMediana(Valores);
        }
    }
}
