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
            Linhas.Add($"\n Amplitude: {Tabela.ValorMaximo} - {Tabela.ValorMinimo} = {Tabela.Amplitude}");
            Linhas.Add($" Quantidade Intervalos: sqr.root {Tabela.NumeroDeElementos} = {Tabela.QuantidadeIntervalos}");
            Linhas.Add($" Intervalo: {Tabela.Amplitude} / {Tabela.QuantidadeIntervalos} = {Tabela.Intervalo}");
            Linhas.Add($" Média: {Tabela.Valores.Sum()} / {Tabela.NumeroDeElementos} = {Tabela.Valores.Average()}");
            Linhas.Add($" Mediana = {Tabela.Valores[(int)Tabela.Valores.Count / 2]}");
            Linhas.Add($"{Tabela.Passos.ToString()}");
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
