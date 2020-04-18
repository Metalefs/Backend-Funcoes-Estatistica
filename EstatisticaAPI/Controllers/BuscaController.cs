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
    }
}
