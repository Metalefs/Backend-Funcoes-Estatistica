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

        public StringBuilder Passos { get; set; }

        public string[] intervalo { get; private set;}
        public float[] xi { get; private set; }
        public float[] fi { get; private set;}
        public float[] Fi { get; private set;}
        public float[] fr { get; private set;}
        public float[] Fr { get; private set;}

        public Moda Moda { get; private set; }
        public Mediana Mediana { get; private set; }
        public Media Media { get; private set; }
        public DesvioPadrao DesvioPadrao { get; set; }
        public Variancia Variancia { get; set; }

        public TabelaDistribuicao(List<float> Valores)
        {
            Passos = new StringBuilder();
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
            Passos.Append(Moda.Passos.ToString());

            Mediana = new Mediana(Valores);
            Passos.Append(Mediana.Passos.ToString());

            DesvioPadrao = new DesvioPadrao(Valores);
            Passos.Append(DesvioPadrao.Passos.ToString());

            Variancia = new Variancia(Valores);
            Passos.Append(Variancia.Passos.ToString());

            Media = new Media(Valores);
            Passos.Append(Media.Passos.ToString());
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
            Passos.AppendLine($"Calcular amplitude: ValorMaximo ({ValorMaximo}) - ValorMinimo ({ValorMinimo}) = {Amplitude}");
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
            Passos.AppendLine($"Calcular quantidade de intervalos: Se 5 elementos = 2, \n10 elementos = 4, \n25 elementos = 6, \n50 elementos = 8, \n100 elementos = 10 \n Se não, Raiz quadrada da quantidade de elementos = Raiz {NumeroDeElementos} -- ({QuantidadeIntervalos})");
            return QuantidadeIntervalos;
        }

        private float CalcularTamanhoIntervalo(float Amplitude, float QuantidadeIntervalos)
        {
            Intervalo = Amplitude / QuantidadeIntervalos;
            Passos.AppendLine($"Calcular tamanho do intervalo: Amplitude ({Amplitude}) / Quantidade de intervalos ({QuantidadeIntervalos}) = {Intervalo}");
            return Intervalo;
        }

        private void  CalcularTodosOsIntervalos()
        {
            float Abertura = ValorMinimo;
            Passos.AppendLine($"Calcular abertura intervalo: Valor minimo = {ValorMinimo}");
            for (int i = 0; i <= QuantidadeIntervalos; i++)
            {
                float FimIntervalo = Abertura + Intervalo;
                intervalo[i] = $"{Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")}";
                Passos.AppendLine($"Calcular intervalo: Abertura ({Abertura}) | Fim = ({FimIntervalo}) (Abertura + Intervalo ({Intervalo})) = {Intervalo}");

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
            float resultado = (Abertura + Fim) / 2;
            Passos.AppendLine($"Calcular Média do intervalo: ({Abertura} + {Fim}) / 2 = {resultado}");
            return resultado;
        }

        private float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            float resultado = Valores.Where(x => x >= Abertura && x < Fim).Count();
            Passos.AppendLine($"Calcular Frequencia Simples: Contagem de valores entre {Abertura} e {Fim} = {resultado}");
            return resultado;
        }

        private float CalcularFrequenciaSimplesAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fi.Sum();
            else
                resultado = fi[pos];

            Passos.AppendLine($"Calcular Frequencia Simples Acumulada: Soma da Frequencia Simples na posição i = {pos + 1} = {resultado}");
            return resultado;
        }

        private float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = fi[pos] / NumeroDeElementos * 100;
            Passos.AppendLine($"Calcular Frequencia Relativa Simples: Porcentagem da Frequencia Simples na posição i = {pos + 1} = {Fr}");
            return Fr;
        }

        private float CalcularFrequenciaRelativaAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fr.Sum();
            else
                resultado = fr[pos];

            Passos.AppendLine($"Calcular Frequencia Relativa Acumulada: Soma da Frequencia Relativa Simples na posição i = {pos + 1} = {resultado}");
            return resultado;
        }

    }
}