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
        public List<KeyValuePair<float, int>> ValoresDistintos { get; set; }
        public float Amplitude { get; private set; }
        public int NumeroDeElementos { get; private set; }
        public float QuantidadeIntervalos { get; private set; }
        public float Intervalo { get; private set; }
        public float ValorMinimo { get; private set; }
        public float ValorMaximo { get; private set; }

        public StringBuilder Passos { get; set; }

        public List<string> intervalos { get; private set;}
        public List<float> xi { get; private set; }
        public List<float> fi { get; private set;}
        public List<float> Fi { get; private set;}
        public List<float> fr { get; private set;}
        public List<float> Fr { get; private set;}

        public Moda Moda { get; private set; }
        public Mediana Mediana { get; private set; }
        public Media Media { get; private set; }
        public DesvioPadrao DesvioPadrao { get; set; }
        public Variancia Variancia { get; set; }

        public bool Simples { get; set; }

        public TabelaDistribuicao(List<float> Valores)
        {
            Passos = new StringBuilder();
            intervalos = new List<string>();
            xi = new List<float>();
            fi = new List<float>();
            Fi = new List<float>();
            fr = new List<float>();
            Fr = new List<float>();
            ValoresDistintos = new List<KeyValuePair<float, int>>();
            this.Valores = Valores;
            var ValoresAgrupados = from s in Valores
                                   group s by s into grp
                                   select new
                                   {
                                       valor = grp.Key,
                                       contagem = grp.Count()
                                   };
            foreach (var k in ValoresAgrupados.OrderBy(x=>x.valor))
            {
                ValoresDistintos.Add(new KeyValuePair<float, int>(k.valor, k.contagem));
            }
            if (ValoresDistintos.Count <= 10)
                Simples = true;
            else
                Simples = false;
        }

        public void Calcular()
        {
            string ValoresCSV = String.Join(",", Valores.OrderBy(x=>x));
            Passos.AppendLine($"<strong>Elementos: </strong> {ValoresCSV} <br>");

            NumeroDeElementos = Valores.Count;
            ValorMinimo = CalcularValorMinimo(Valores);
            ValorMaximo = CalcularValorMaximo(Valores);

            Amplitude = CalcularAmplitude(ValorMinimo, ValorMaximo);
            if (Simples)
                QuantidadeIntervalos = ValoresDistintos.Count();
            else
                QuantidadeIntervalos = CalcularQuantidadeIntervalos(NumeroDeElementos);
            Intervalo = CalcularTamanhoIntervalo(Amplitude, QuantidadeIntervalos);

            CalcularTodosOsIntervalos();

            Moda = new Moda(Valores);
            Moda.Calcular();
            Passos.Append(Moda.Passos.ToString());

            Mediana = new Mediana(Valores);
            Mediana.Calcular();
            Passos.Append(Mediana.Passos.ToString());

            DesvioPadrao = new DesvioPadrao(Valores);
            DesvioPadrao.Calcular();
            Passos.Append(DesvioPadrao.Passos.ToString());

            Variancia = new Variancia(Valores);
            Variancia.Calcular();
            Passos.Append(Variancia.Passos.ToString());

            Media = new Media(Valores);
            Media.Calcular();
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
            Passos.AppendLine($"<strong>Calcular Amplitude</strong>: [ValorMaximo] {ValorMaximo} - [ValorMinimo] {ValorMinimo} = {Amplitude} <br>");
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
            Passos.AppendLine($"<strong>Calcular quantidade de intervalos</strong>: Se 5 elementos = 2, \n10 elementos = 4, \n25 elementos = 6, \n50 elementos = 8, \n100 elementos = 10 \n Se não, Raiz quadrada da quantidade de elementos = Raiz {NumeroDeElementos} -- ({QuantidadeIntervalos}) <br>");
            return QuantidadeIntervalos;
        }

        private float CalcularTamanhoIntervalo(float Amplitude, float QuantidadeIntervalos)
        {
            Intervalo = Amplitude / QuantidadeIntervalos;
            Passos.AppendLine($"<strong>Calcular tamanho do intervalo</strong>: [Amplitude] {Amplitude} / [Quantidade de intervalos] {QuantidadeIntervalos} = {Intervalo} <br>");
            return Intervalo;
        }

        private void  CalcularTodosOsIntervalos()
        {
            float Abertura = ValorMinimo;
            Passos.AppendLine($"<strong>Calcular abertura do intervalo</strong>: Começa pelo Valor Minimo = [{ValorMinimo}] <hr>");
            for (int i = 0; i < QuantidadeIntervalos; i++)
            {
                try
                {
                    if (!Simples)
                    {
                        float FimIntervalo = Abertura + Intervalo;
                        intervalos.Add($"{Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")}");
                        Passos.AppendLine($"<strong>Calcular intervalo</strong>: Abertura ({Abertura}) | Fim = ({FimIntervalo}) (Abertura + Intervalo ({Intervalo})) = {intervalos[i]} <br>");

                        xi.Add(CalcularMediaXI(Abertura, FimIntervalo));
                        fi.Add(CalcularFrequenciaSimples(Abertura, FimIntervalo));

                        Fi.Add(CalcularFrequenciaSimplesAcumulada(i));
                
                        fr.Add(CalcularFrequenciaRelativa(i));
                        Fr.Add(CalcularFrequenciaRelativaAcumulada(i));

                        Abertura = FimIntervalo;
                    }
                    else
                    {
                        Passos.AppendLine($"<strong>Freq. Simples de <em>(x{i+1})[{ValoresDistintos[i].Key}]</em> </strong>: {ValoresDistintos[i].Value}<br>");
                        xi.Add(ValoresDistintos[i].Key);
                        fi.Add(ValoresDistintos[i].Value);

                        Fi.Add(CalcularFrequenciaSimplesAcumulada(i));
                        fr.Add(CalcularFrequenciaRelativa(i));
                        Fr.Add(CalcularFrequenciaRelativaAcumulada(i));
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
                
            }
        }

        private float CalcularMediaXI(float Abertura, float Fim)
        {
            float resultado = (Abertura + Fim) / 2;
            Passos.AppendLine($"<strong>Média do intervalo</strong>: ({Abertura} + {Fim}) / 2 = {resultado} <br>");
            return resultado;
        }

        private float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            float resultado = Valores.Where(x => x >= Abertura && x <= Fim).Count();
            Passos.AppendLine($"<strong>Freq. Simples</strong>: Contagem de valores entre {Abertura} e {Fim} : {resultado} <br>");
            return resultado;
        }

        private float CalcularFrequenciaSimplesAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fi.Sum();
            else
                resultado = fi[pos];

            Passos.AppendLine($"<strong>Freq. Simples Acum. de (x{pos + 1})[{ValoresDistintos[pos].Key}] </strong>: {resultado} <hr>");
            return resultado;
        }

        private float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = fi[pos] / NumeroDeElementos * 100;
            Passos.AppendLine($"<strong><em>Freq. Relativa de (x{pos + 1})[{ValoresDistintos[pos].Key}] </em></strong>: {Fr}% <br>");
            return Fr;
        }

        private float CalcularFrequenciaRelativaAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fr.Sum();
            else
                resultado = fr[pos];

            Passos.AppendLine($"<strong><em>Freq. Relativa Acumu. de (x{pos + 1})[{ValoresDistintos[pos].Key}] </em></strong>: {resultado} <hr>");
            return resultado;
        }

    }
}