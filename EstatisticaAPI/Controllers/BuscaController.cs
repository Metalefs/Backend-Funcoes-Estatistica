using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Estatistica101;

namespace EstatisticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuscaController : Controller
    {
        private readonly ILogger<BuscaController> _logger;

        public BuscaController(ILogger<BuscaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IActionResult TabelaDistribuicao(string Valores)
        {
            string retorno = JsonConvert.SerializeObject(Estatistica.ObterTabelaDistribuicao(Valores));
            return Json(retorno);
        }
        [HttpGet]
        public string TextoTabelaDistribuicao()
        { 
            return Estatistica.ObterTextoTabelaDistribuicao("1,2,3,4,1,1,1,0,2,2,2");
        }
        [HttpGet]
        [Route("ObterDesvioPadrao/{id}")]
        public string ObterDesvioPadrao(string Valores)
        { 
            return Estatistica.ObterDesvioPadrao(Valores);
        }
        [HttpGet]
        [Route("ObterVariancia/{id}")]
        public string ObterVariancia(string Valores)
        {
            return Estatistica.ObterVariancia(Valores);
        }
        [HttpGet]
        [Route("ObterMedia/{id}")]
        public string ObterMedia(string Valores)
        {
            return Estatistica.ObterMedia(Valores);
        }
        [HttpGet]
        [Route("ObterModa/{id}")]
        public string ObterModa(string Valores)
        { 
            return Estatistica.ObterModa(Valores);
        }
        [HttpGet]
        [Route("ObterMediana/{id}")]
        public string ObterMediana(string Valores)
        {
            return Estatistica.ObterMediana(Valores);
        }
    }
}
