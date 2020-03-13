using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Estatistica101.Classes
{
    public class MontadorTabelaDistribuicao
    {
        TabelaDistribuicao Tabela { get; set; }
        List<string> Linhas { get; }
        public MontadorTabelaDistribuicao(TabelaDistribuicao Tabela)
        {
            this.Tabela = Tabela;
        }

        public string GerarTexto()
        {
            Linhas.Add(GerarCabecalho());
            for (int i = 0; i < Tabela.QuantidadeIntervalos; i++)
            {
                Linhas.Add(GerarLinhaTabela(Tabela.intervalo[i], Tabela.xi[i], Tabela.fi[i], Tabela.Fi[i], Tabela.fr[i], Tabela.Fr[i]));
            }
            Linhas.Add(GerarLinhaTabela("", 0f, Tabela.fi.Sum(), 0f, Tabela.fr.Sum(), 0f));
            SalvarResultado(Linhas, "Resultado.txt");
            return Linhas.ToString();
        }

        private string GerarCabecalho()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> KV in Tabela.NomesColunas)
            {
                if (KV.Key == "Dados")
                {
                    sb.Append($"{KV.Key.PadLeft(20),20}\t");
                }
                else
                {
                    sb.Append($"{KV.Key.Normalized(),15} ");
                }
            }
            return sb.ToString();
        }

        private string GerarLinhaTabela(string variavel, float xi, float fi, float Fi, float fr, float Fr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
                $"{variavel,20}" +
                $"{PadronizarLinha(xi),15}" +
                $"{PadronizarLinha(fi),15}" +
                $"{PadronizarLinha(Fi),15}" +
                $"{PadronizarLinha(fr),15}" +
                $"{PadronizarLinha(Fr),15} "
            );
            return sb.ToString();
        }

        private string PadronizarLinha(float value)
        {
            return value.ToString("0.00");
        }

        private void SalvarResultado(List<string> Linhas, string Caminho)
        {
            using (TextWriter tw = new StreamWriter(Caminho, true))
            {
                tw.WriteLine(GerarCabecalho());
                Console.WriteLine(GerarCabecalho());
                foreach (string linha in Linhas)
                {
                    tw.Write(linha);
                    Console.Write(linha);
                }
            }
        }

        private void SalvarResultadoHTML(List<string> Linha)
        {

        }
    }
}
