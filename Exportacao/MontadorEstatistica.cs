using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Estatistica101.Classes
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
            Linhas.Append($"{Tabela.Passos.ToString()}");
            Linhas.Append($"{Tabela.Resultado}");
            SalvarResultado(Linhas, "Resultado.txt");
            return Linhas.ToString();
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
