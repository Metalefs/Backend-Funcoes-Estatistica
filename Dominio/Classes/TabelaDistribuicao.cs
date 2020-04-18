using Estatistica101.Interfaces;
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

        public List<float> Valores { get; private set; }
        public float Amplitude { get; private set; }
        public int NumeroDeElementos { get; private set; }
        public float QuantidadeIntervalos { get; private set; }
        public float Intervalo { get; private set; }
        public float ValorMinimo { get; private set; }
        public float ValorMaximo { get; private set; }

        public string[] intervalo { get; private set;}
        public float[] xi { get; private set; }
        public float[] fi { get; private set;}
        public float[] Fi { get; private set;}
        public float[] fr { get; private set;}
        public float[] Fr { get; private set;}

        public Moda Moda { get; private set; }
        public Mediana Mediana { get; private set; }
        public float Media { get; private set; }
        public DesvioPadrao DesvioPadrao { get; set; }
        public Variancia Variancia { get; set; }

        public TabelaDistribuicao(List<float> Valores)
        {
            this.Valores = Valores;
        }

        public void Calcular()
        {
            NumeroDeElementos = Valores.Count;
            ValorMinimo = CalcularValorMinimo(Valores);
            ValorMaximo = CalcularValorMaximo(Valores);

            Amplitude = CalcularAmplitude(ValorMinimo, ValorMaximo);
            QuantidadeIntervalos = CalcularQuantidadeIntervalos(NumeroDeElementos);
            Intervalo = CalcularTamanhoIntervalo(Amplitude, QuantidadeIntervalos);

            CalcularTodosOsIntervalos();
            Moda = new Moda(Valores);
            Mediana = new Mediana(Valores);
            DesvioPadrao = new DesvioPadrao(Valores);
            Variancia = new Variancia(Valores);
            Media = Valores.Average();
        }

        private float CalcularValorMinimo(List<float> Valores)
        {
            ValorMinimo = Valores.Min();
            return ValorMinimo;
        }

        private float CalcularValorMaximo(List<float> Valores)
        {
            ValorMaximo = Valores.Max();
            return ValorMaximo;
        }

        private float CalcularAmplitude(float ValorMinimo, float ValorMaximo)
        {
            Amplitude = ValorMaximo - ValorMinimo;
            return Amplitude;
        }

        private float CalcularQuantidadeIntervalos(int NumeroDeElementos)
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

        private float CalcularTamanhoIntervalo(float Amplitude, float QuantidadeIntervalos)
        {
            Intervalo = Amplitude / QuantidadeIntervalos;
            return Intervalo;
        }

        private void  CalcularTodosOsIntervalos()
        {
            float Abertura = ValorMinimo;
            for (int i = 0; i <= QuantidadeIntervalos; i++)
            {
                float FimIntervalo = Abertura + Intervalo;
                intervalo[i] = $"{Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")}";

                xi[i] = CalcularMediaXI(Abertura, FimIntervalo);

                fi[i] = CalcularFrequenciaSimples(Abertura, FimIntervalo);
                Fi[i] = CalcularFrequenciaSimplesAcumulada(i);

                fr[i] = CalcularFrequenciaRelativa(i);
                Fr[i] = CalcularFrequenciaRelativaAcumulada(i);

                Abertura = FimIntervalo;
            }
        }

        private float CalcularMediaXI(float Abertura, float Fim)
        {
            return (Abertura + Fim) / 2;
        }

        private float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            return Valores.Where(x => x >= Abertura && x < Fim).Count();
        }

        private float CalcularFrequenciaSimplesAcumulada(int pos)
        {
            if (pos > 0)
                return fi.Sum();
            else
                return fi[pos];
        }

        private float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = fi[pos] / NumeroDeElementos * 100;
            return Fr;
        }

        private float CalcularFrequenciaRelativaAcumulada(int pos)
        {
            if (pos > 0)
                return fr.Sum();
            else
                return fr[pos];
        }

    }
}