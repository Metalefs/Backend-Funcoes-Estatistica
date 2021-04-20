using Estatistica101;
using Estatistica101.Classes;
using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Exportacao.HTML.ClassToHTML;
namespace Exportacao.Montador
{
    public class MontadorTabelaDistribuicao
    {
        TabelaDistribuicao Tabela { get; set; }
        StringBuilder Linhas { get; }
        public MontadorTabelaDistribuicao(TabelaDistribuicao Tabela)
        {
            Linhas = new StringBuilder();
            this.Tabela = Tabela;
        }

        public string GerarTexto()
        {
            Linhas.Append(GerarCabecalho());
            for (int i = 0; i < Tabela.QuantidadeIntervalos; i++)
            {
                Linhas.Append(GerarLinhaTabela(Tabela.intervalos[i], Tabela.xi[i], Tabela.fi[i], Tabela.Fi[i], Tabela.fr[i], Tabela.Fr[i]));
            }
            Linhas.Append(GerarLinhaTabela("", 0f, Tabela.fi.Sum(), 0f, Tabela.fr.Sum(), 0f));
            Linhas.Append($"\n Amplitude: {Tabela.ValorMaximo} - {Tabela.ValorMinimo} = {Tabela.Amplitude}");
            Linhas.Append($" Quantidade Intervalos: sqr.root {Tabela.NumeroDeElementos} = {Tabela.QuantidadeIntervalos}");
            Linhas.Append($" Intervalo: {Tabela.Amplitude} / {Tabela.QuantidadeIntervalos} = {Tabela.Intervalo}");
            Linhas.Append($" Média: {Tabela.Valores.Sum()} / {Tabela.NumeroDeElementos} = {Tabela.Valores.Average()}");
            Linhas.Append($" Mediana = {Tabela.Valores[(int)Tabela.Valores.Count / 2]}");
            Linhas.Append($"{Tabela.Passos.ToString()}");
            SalvarResultado(Linhas, "Resultado.txt");
            return Linhas.ToString();
        }

        public string GerarHTMLTabelaDeTruman()
        {
            List<KeyValuePair<string, string>> CamposTabelaTruman = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("N","K"),
                new KeyValuePair<string, string>("5","2"),
                new KeyValuePair<string, string>("10","4"),
                new KeyValuePair<string, string>("25","6"),
                new KeyValuePair<string, string>("50","8"),
                new KeyValuePair<string, string>("100","10"),
            };
            return ClassToHTML.MontarTabela(CamposTabelaTruman);
        }

        private void SalvarResultado(StringBuilder Linhas, string Caminho)
        {
            using (TextWriter tw = new StreamWriter(Caminho, true))
            {
                tw.Write(Linhas.ToString());
                Console.Write(Linhas.ToString());
            }
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

        private void SalvarResultadoHTML(List<string> Linha)
        {

        }
    }
}
