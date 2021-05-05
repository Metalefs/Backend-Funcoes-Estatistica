using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Estatistica101.Classes;
using Exportacao.Montador;
using Dominio.Classes;

namespace EstatisticaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistribuicaoController
    {
        private readonly ILogger<DistribuicaoController> _logger;

        public DistribuicaoController(ILogger<DistribuicaoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("TabelaDistribuicao/{data}")]
        public IActionResult TabelaDistribuicao(string data)
        {
            string retorno;
            try
            {
                retorno = JsonConvert.SerializeObject(ObterTabelaDistribuicao(data), new Newtonsoft.Json.Converters.StringEnumConverter());
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }
        [HttpGet("ObterTabelaDistribuicao/{id}")]
        public static TabelaDesvioPadrao ObterTabelaDistribuicao(string texto)
        {
            TabelaDesvioPadrao Elemento = new TabelaDesvioPadrao(ObterValores(texto));
            Elemento.Calcular();
            return Elemento;
        }
        [HttpGet("ObterDesvioPadrao/{data}")]
        public IActionResult ObterDesvioPadrao(string data)
        {
            string retorno;
            try
            {
                DesvioPadrao Elemento = new DesvioPadrao(ObterValores(data));
                Elemento.Calcular();
                MontadorEstatistica<DesvioPadrao> montador = new MontadorEstatistica<DesvioPadrao>(Elemento);
                retorno = montador.GerarTexto();
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }
        [HttpGet("ObterVariancia/{data}")]
        public IActionResult ObterVariancia(string data)
        {
            string retorno;
            try
            {

                Variancia Elemento = new Variancia(ObterValores(data));
                Elemento.Calcular();
                MontadorEstatistica<Variancia> montador = new MontadorEstatistica<Variancia>(Elemento);
                retorno = montador.GerarTexto();
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }
        [HttpGet("ObterMedia/{data}")]
        public IActionResult ObterMedia(string data)
        {
            string retorno;
            try
            {

                Media Elemento = new Media(ObterValores(data));
                Elemento.Calcular();
                MontadorEstatistica<Media> montador = new MontadorEstatistica<Media>(Elemento);
                retorno = montador.GerarTexto();
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }
        [HttpGet("ObterModa/{data}")]
        public IActionResult ObterModa(string data)
        {
            string retorno;
            try
            {

                Moda Elemento = new Moda(ObterValores(data));
                Elemento.Calcular();
                MontadorEstatistica<Moda> montador = new MontadorEstatistica<Moda>(Elemento);
                retorno = montador.GerarTexto();
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }
        [HttpGet("ObterMediana/{data}")]
        public IActionResult ObterMediana(string data)
        {
            string retorno;
            try
            {

                Mediana Elemento = new Mediana(ObterValores(data));
                Elemento.Calcular();
                MontadorEstatistica<Mediana> montador = new MontadorEstatistica<Mediana>(Elemento);
                retorno = montador.GerarTexto();
            }
            catch (System.Exception ex)
            {
                retorno = JsonConvert.SerializeObject(new { erro = ex.Message });
            }
            return new JsonResult(retorno);
        }

        private static List<float> ObterValores(string texto)
        {
            List<float> Valores = new List<float>();
            string valorAtual = "";
            int idx = 0;
            try
            {
                foreach (string valor in texto.Split(','))
                {
                    valorAtual = valor;
                    idx++;
                    if (float.TryParse(valor, out float result))
                        Valores.Add(float.Parse(valor, System.Globalization.CultureInfo.InvariantCulture));
                }
            }
            catch(System.Exception ex)
            {
                throw new System.Exception($"Ocorreu um erro ao formatar o valor {valorAtual} na posição {idx}");
            }
            return Valores;
        }
    }
}
