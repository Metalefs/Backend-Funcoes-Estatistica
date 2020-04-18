using System;
using System.Collections.Generic;
using System.IO;

namespace Estatistica101.Classes
{
    public class MontadorEstatistica<T> where T: EstatisticaBase
    {
        T Tabela { get; set; }
        List<string> Linhas { get; }
        public MontadorEstatistica(T Tabela)
        {
            this.Tabela = Tabela;
        }

        public string GerarTexto()
        {
            Linhas.Add($"{Tabela.Passos.ToString()}");
            Linhas.Add($"{Tabela.Resultado}");
            SalvarResultado(Linhas, "Resultado.txt");
            return Linhas.ToString();
        }

        private void SalvarResultado(List<string> Linhas, string Caminho)
        {
            using (TextWriter tw = new StreamWriter(Caminho, true))
            {
                foreach (string linha in Linhas)
                {
                    tw.Write(linha);
                    Console.Write(linha);
                }
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
