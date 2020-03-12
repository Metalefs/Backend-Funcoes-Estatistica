using Distribuicao.DadosAgrupados;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Estatistica101.Classes
{
    public class TabelaDistribuicao : ITabelaDistribuicao
    {
        public List<KeyValuePair<string, int>> NomesColunas = new List<KeyValuePair<string, int>>()
        {
            new KeyValuePair<string, int>("Classes", 15),
            new KeyValuePair<string, int>("P.Médio(xi)", 10),
            new KeyValuePair<string, int>("F.Abs(fi)", 10),
            new KeyValuePair<string, int>("F.Rel(Fi)", 10),
            new KeyValuePair<string, int>("F.Abs.Acum(Fi)", 10),
            new KeyValuePair<string, int>("F.Rel.Acum(Fr)", 10)
        };

        private List<string> Linhas = new List<string>();
        private List<float> FrequenciasSimples = new List<float>();
        private List<float> FrequenciasRelativas = new List<float>();

        public List<float> Valores { get; set; }
        public float Amplitude { get; private set; }
        public int NumeroDeElementos { get; private set; }
        public float QuantidadeIntervalos { get; private set; }
        public float Intervalo { get; private set; }
        private float ValorMinimo;
        private float ValorMaximo;

        public TabelaDistribuicao(List<float> Valores)
        {
            this.Valores = Valores;
            NumeroDeElementos = Valores.Count;
            ValorMinimo = CalcularValorMinimo(Valores);
            ValorMaximo = CalcularValorMaximo(Valores);

            Amplitude = CalcularAmplitude(ValorMinimo, ValorMaximo);
            QuantidadeIntervalos = CalcularQuantidadeIntervalos(NumeroDeElementos);
            Intervalo = CalcularTamanhoIntervalo(Amplitude, QuantidadeIntervalos);
        }

        public float CalcularValorMinimo(List<float> Valores)
        {
            ValorMinimo = Valores.Min();
            return ValorMinimo;
        }

        public float CalcularValorMaximo(List<float> Valores)
        {
            ValorMaximo = Valores.Max();
            return ValorMaximo;
        }

        public float CalcularAmplitude(float ValorMinimo, float ValorMaximo)
        {
            Amplitude = ValorMaximo - ValorMinimo;
            return Amplitude;
        }

        public float CalcularQuantidadeIntervalos(int NumeroDeElementos)
        {
            switch (NumeroDeElementos)
            {
                case 5:
                    QuantidadeIntervalos = 2;
                    break;
                case 10:
                    QuantidadeIntervalos = 4;
                    break;
                case 25:
                    QuantidadeIntervalos = 6;
                    break;
                case 50:
                    QuantidadeIntervalos = 8;
                    break;
                case 100:
                    QuantidadeIntervalos = 10;
                    break;
                default:
                    QuantidadeIntervalos = (float)Math.Sqrt(NumeroDeElementos);
                    break;
            }
            return QuantidadeIntervalos;
        }

        public float CalcularTamanhoIntervalo(float Amplitude, float QuantidadeIntervalos)
        {
            Intervalo = Amplitude / QuantidadeIntervalos;
            return Intervalo;
        }

        public void GerarTabela()
        {
            float xi, fi, Fi, fr, Fr = 0;
            float Abertura = ValorMinimo;
            for (int i = 0; i <= QuantidadeIntervalos; i++)
            {
                float Fim = Abertura + Intervalo;
                string variavel = $"{Abertura.ToString("0.00")}|--{Fim.ToString("0.00")}";

                xi = CalcularMediaXI(Abertura, Fim);
                fi = CalcularFrequenciaSimples(Abertura, Fim);
                FrequenciasSimples.Add(fi);

                Fi = CalcularFrequenciaSimplesAcumulada(i, fi);
                fr = CalcularFrequenciaRelativa(i);
                FrequenciasRelativas.Add(fr);

                Fr = CalcularFrequenciaRelativaAcumulada(i, fr);

                Linhas.Add(GerarLinha(variavel, xi, fi, Fi, fr, Fr));

                Abertura = Fim;
            }
            Linhas.Add(GerarLinha("", 0f, FrequenciasSimples.Sum(), 0f, FrequenciasRelativas.Sum(), 0f));
            SalvarResultado(Linhas);
        }

        private void SalvarResultado(List<string> Linhas)
        {
            using (TextWriter tw = new StreamWriter("Resultado.txt", true))
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

        private string GerarCabecalho()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, int> KV in NomesColunas)
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

        private string GerarLinha(string variavel, float xi, float fi, float Fi, float fr, float Fr)
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

        private float CalcularMediaXI(float Abertura, float Fim)
        {
            return (Abertura + Fim) / 2;
        }

        public float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            return Valores.Where(x => x >= Abertura && x < Fim).Count();
        }

        public float CalcularFrequenciaSimplesAcumulada(int pos, float fi)
        {
            if (pos > 0)
                return FrequenciasSimples.Sum();
            else
                return fi;
        }

        public float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = FrequenciasSimples[pos] / NumeroDeElementos * 100;
            return Fr;
        }

        public float CalcularFrequenciaRelativaAcumulada(int pos, float fr)
        {
            if (pos > 0)
                return FrequenciasRelativas.Sum();
            else
                return fr;
        }

    }
}