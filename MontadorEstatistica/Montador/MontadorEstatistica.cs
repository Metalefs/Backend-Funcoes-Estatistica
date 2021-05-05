using Estatistica101.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Exportacao.Montador
{
    public class MontadorEstatistica<T> where T: EstatisticaBase
    {
        T Tabela { get; set; }
        StringBuilder Linhas { get; }
        public MontadorEstatistica(T Tabela)
        {
            Linhas = new StringBuilder();
            this.Tabela = Tabela;
        }

        public string GerarTexto()
        {
            Linhas.Append($"{Tabela.Passos}");
            Linhas.Append($"{Tabela.Resultado}");
            SalvarResultado(Linhas, "Resultado.txt");
            //return Linhas.ToString();
            return JsonConvert.SerializeObject(Tabela);
        }

        private void SalvarResultado(StringBuilder Linhas, string Caminho)
        {
            using (TextWriter tw = new StreamWriter(Caminho, true))
            {
                tw.Write(Linhas.ToString());
                Console.Write(Linhas.ToString());
            }
        }

        private string PadronizarLinha(float value)
        {
            return value.ToString("0.00");
        }

        private void SalvarResultadoHTML(List<string> Linha)
        {

        }
    }
}
