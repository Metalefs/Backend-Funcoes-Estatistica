using Estatistica101.Classes;
using Exportacao.Montador;
using System.Collections.Generic;

namespace Estatistica101
{
    public static class Estatistica
    {
        public static string ObterTextoTabelaDistribuicao(string texto) 
        {
            TabelaDistribuicao Elemento = new TabelaDistribuicao(ObterValores(texto));
            Elemento.Calcular();
            MontadorTabelaDistribuicao montador = new MontadorTabelaDistribuicao(Elemento);
            return montador.GerarTexto();
        }

        public static TabelaDistribuicao ObterTabelaDistribuicao(string texto)
        {
            TabelaDistribuicao Elemento = new TabelaDistribuicao(ObterValores(texto));
            Elemento.Calcular();
            return Elemento;
        }

        public static string ObterDesvioPadrao(string texto)
        {
            DesvioPadrao Elemento = new DesvioPadrao(ObterValores(texto));
            Elemento.Calcular();
            MontadorEstatistica<DesvioPadrao> montador = new MontadorEstatistica<DesvioPadrao>(Elemento);
            return montador.GerarTexto();
        }
        public static string ObterVariancia(string texto)
        {
            Variancia Elemento = new Variancia(ObterValores(texto));
            Elemento.Calcular();
            MontadorEstatistica<Variancia> montador = new MontadorEstatistica<Variancia>(Elemento);
            return montador.GerarTexto();
        }
        public static string ObterMedia(string texto)
        {
            Media Elemento = new Media(ObterValores(texto));
            Elemento.Calcular();
            MontadorEstatistica<Media> montador = new MontadorEstatistica<Media>(Elemento);
            return montador.GerarTexto();
        }
        public static string ObterModa(string texto) 
        {
            Moda Elemento = new Moda(ObterValores(texto));
            Elemento.Calcular();
            MontadorEstatistica<Moda> montador = new MontadorEstatistica<Moda>(Elemento);
            return montador.GerarTexto();
        }
        public static string ObterMediana(string texto) 
        {
            Mediana Elemento = new Mediana(ObterValores(texto));
            Elemento.Calcular();
            MontadorEstatistica<Mediana> montador = new MontadorEstatistica<Mediana>(Elemento);
            return montador.GerarTexto();
        }

        private static List<float> ObterValores(string texto)
        {
            List<float> Valores = new List<float>();
            foreach (string valor in texto.Split(','))
            {
                if (float.TryParse(valor, out float result))
                    Valores.Add(float.Parse(valor));
            }
            return Valores;
        }
    }
}
