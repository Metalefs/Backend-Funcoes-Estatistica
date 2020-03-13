using Estatistica101.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estatistica101
{
    public class Estatistica
    {
        public static string ObterTextoTabelaDistribuicao(string texto) 
        {
            List<float> Valores = new List<float>();
            foreach(string valor in texto.Split(','))
            {
                if(float.TryParse(valor, out float result))
                    Valores.Add(float.Parse(valor));
            }
            TabelaDistribuicao tabela = new TabelaDistribuicao(Valores);
            tabela.Calcular();

            MontadorTabelaDistribuicao montador = new MontadorTabelaDistribuicao(tabela);
            return montador.GerarTexto();
        }

        public static TabelaDistribuicao ObterTabelaDistribuicao(string texto)
        {
            List<float> Valores = new List<float>();
            foreach (string valor in texto.Split(','))
            {
                if (float.TryParse(valor, out float result))
                    Valores.Add(float.Parse(valor));
            }
            TabelaDistribuicao tabela = new TabelaDistribuicao(Valores);
            tabela.Calcular();
            return tabela;
        }
    }
}
