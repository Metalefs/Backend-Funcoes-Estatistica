using Dominio.Decorators;
using Estatistica101.Interfaces;
using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using static Exportacao.HTML.ClassToHTML;
namespace Estatistica101.Classes
{
    public class TabelaDistribuicao : EstatisticaBase, ITabelaDistribuicao
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

        public new List<float> Valores { get; private set; }
        public List<KeyValuePair<float, int>> ValoresDistintos { get; set; }
        public float Amplitude { get; private set; }
        public int NumeroDeElementos { get; private set; }
        public double QuantidadeIntervalos { get; private set; }
        public float Intervalo { get; private set; }
        public float ValorMinimo { get; private set; }
        public float ValorMaximo { get; private set; }
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

        public TabelaDistribuicao(List<float> Valores) : base()
        {
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

        public override float Calcular()
        {
            Valores = Valores.ToList().OrderBy(x => x).ToList();
            string ValoresCSV = String.Join(",", Valores);
            Passos.WriteLineAsync($"{Titulo("Elementos (N): ")} {ValoresCSV}");

            NumeroDeElementos = Valores.Count;
            ValorMinimo = CalcularValorMinimo(Valores);
            ValorMaximo = CalcularValorMaximo(Valores);

            Amplitude = CalcularAmplitudeA(ValorMinimo, ValorMaximo);
            if (Simples)
                QuantidadeIntervalos = ValoresDistintos.Count();
            else
                QuantidadeIntervalos = CalcularQuantidadeIntervalosK(NumeroDeElementos);
            Intervalo = CalcularTamanhoIntervaloH(Amplitude, QuantidadeIntervalos);

            CalcularTodosOsIntervalos();

            Moda = new Moda(Valores);
            Moda.Calcular();
            //Passos.Append(Moda.Passos.ToString());

            Mediana = new Mediana(Valores);
            Mediana.Calcular();
            //Passos.Append(Mediana.Passos.ToString());

            DesvioPadrao = new DesvioPadrao(Valores);
            DesvioPadrao.Calcular();
            //Passos.Append(DesvioPadrao.Passos.ToString());

            Variancia = new Variancia(Valores);
            Variancia.Calcular();
            //Passos.Append(Variancia.Passos.ToString());

            Media = new Media(Valores);
            Media.Calcular();
            //Passos.Append(Media.Passos.ToString());

            Passos.Close();
            return 0f;
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

        private float CalcularAmplitudeA(float ValorMinimo, float ValorMaximo)
        {
            Amplitude = ValorMaximo - ValorMinimo;
            Passos.WriteLineAsync($"{Titulo("Calcular Amplitude (A)")}: $$ A = ValorMaximo - ValorMinimo  $$");
            Passos.WriteLineAsync($"$$ A = {ValorMaximo} - {ValorMinimo} = {Amplitude} $$");
            return Amplitude;
        }

        private double CalcularQuantidadeIntervalosK(int NumeroDeElementos, bool HEraDecimal = false)
        {
            if (HEraDecimal)
            {

                Passos.WriteLineAsync($"{Titulo("Se o resultado for quebrado, considerar $$ K =  \\sqrt N  $$ ")}");
                var K = Math.Sqrt(Valores.Count);
                QuantidadeIntervalos = K;

                return QuantidadeIntervalos;
            }
            Passos.WriteLineAsync($"{Titulo("Calcular Quantidade de Intervalos - Tabela de Truman")}:");
            Passos.WriteLineAsync(GerarTabelaDeTruman());
            Passos.WriteLineAsync($"$$ \\sqrt N $$");
            Passos.WriteLineAsync($"$$ \\sqrt {NumeroDeElementos} = {Math.Sqrt(NumeroDeElementos)} $$");

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

        private string GerarTabelaDeTruman()
        {
            List<KeyValuePair<string, string>> CamposTabelaTruman = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("N","K"),
                new KeyValuePair<string, string>("5","2"),
                new KeyValuePair<string, string>("10","4"),
                new KeyValuePair<string, string>("25","6"),
                new KeyValuePair<string, string>("50","8"),
                new KeyValuePair<string, string>("100","10"),
            };
            return ClassToHTML.MontarTabela(CamposTabelaTruman, "class='table '");
        }

        private float CalcularTamanhoIntervaloH(float Amplitude, double QuantidadeIntervalos)
        {
            var Intervalo = (float) (Amplitude / QuantidadeIntervalos);
            Intervalo = (float) ((Intervalo % 1) > 0 ? Amplitude / RecalcularK() : Intervalo);


            Passos.WriteLineAsync($"{Titulo("Calcular Tamanho do Intervalo")}: {HTMLElements.Br()} $$ H = \\dfrac{{ {{A}} }} {{K}} $$");

            Passos.WriteLineAsync($"$$ H =  \\dfrac{{ {{{Amplitude}}} }} {{{QuantidadeIntervalos}}} = {Intervalo} $$");
            return Intervalo;
        }

        private double RecalcularK()
        {
            return CalcularQuantidadeIntervalosK(Valores.Count, HEraDecimal:true);
        }

        private void  CalcularTodosOsIntervalos()
        {
            float Abertura = ValorMinimo;
            Passos.WriteLineAsync($"{Titulo("Calcular Abertura do Intervalo")}: Começa pelo Valor Minimo = [{ValorMinimo}] {HTMLElements.Hr()}");
            for (int i = 0; i < QuantidadeIntervalos; i++)
            {
                try
                {
                    if (!Simples)
                    {
                        float FimIntervalo = Abertura + Intervalo;
                        intervalos.Add($"{Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")}");
                        Passos.WriteLineAsync($"{Titulo("Calcular Intervalo")}: Abertura | Fim = Último Intervalo + H");
                        Passos.WriteLineAsync($"$$ {Abertura} + {Intervalo} = {FimIntervalo} $$");

                        xi.Add(CalcularMediaXI(Abertura, FimIntervalo));
                        fi.Add(CalcularFrequenciaSimples(Abertura, FimIntervalo));

                        Fi.Add(CalcularFrequenciaSimplesAcumulada(i));
                
                        fr.Add(CalcularFrequenciaRelativa(i));
                        Fr.Add(CalcularFrequenciaRelativaAcumulada(i));

                        Abertura = FimIntervalo;
                    }
                    else
                    {
                        Passos.WriteLineAsync($"{Titulo("Freq. Simples de ")} {ClassToHTML.AninharEmEm($"{ValoresDistintos[i].Key} [x{i + 1}]")}: {ValoresDistintos[i].Value}");
                        xi.Add(ValoresDistintos[i].Key);
                        fi.Add(ValoresDistintos[i].Value);

                        Fi.Add(CalcularFrequenciaSimplesAcumulada(i));
                        fr.Add(CalcularFrequenciaRelativa(i));
                        Fr.Add(CalcularFrequenciaRelativaAcumulada(i));
                    }
                }
                catch (Exception)
                {
                    continue;
                }
                
            }
        }

        private float CalcularMediaXI(float Abertura, float Fim)
        {
            float resultado = (Abertura + Fim) / 2;
            Passos.WriteLineAsync($"{Titulo("Média do intervalo")}: ({Abertura} + {Fim}) / 2 = {resultado}");
            return resultado;
        }

        private float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            float resultado = Valores.Where(x => x >= Abertura && x <= Fim).Count();
            Passos.WriteLineAsync($"{Titulo("Freq. Simples")}: Contagem de valores entre {Abertura} e {Fim} : {resultado}");
            return resultado;
        }

        private float CalcularFrequenciaSimplesAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fi.Sum();
            else
                resultado = fi[pos];

            Passos.WriteLineAsync($"{Titulo("Freq. Simples Acum. de")} {ClassToHTML.AninharEmEm($"{ValoresDistintos[pos].Key} [x{pos + 1}]")}: {resultado} {HTMLElements.Hr()}");
            return resultado;
        }

        private float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = fi[pos] / NumeroDeElementos * 100;
            Passos.WriteLineAsync($"{Titulo(ClassToHTML.AninharEmEm($"Freq. Relativa de {ValoresDistintos[pos].Key} [x{pos + 1}]"))}: $$ Fr = {{ \\dfrac{{ {{Fi[{fi[pos]}]}} }} {{N}}  }} * 100 $$");
            Passos.WriteLineAsync($"{Titulo(ClassToHTML.AninharEmEm($"Freq. Relativa de {ValoresDistintos[pos].Key} [x{pos + 1}]"))}: $$ {{ \\dfrac{{ {{{fi[pos]}}} }} {{{NumeroDeElementos}}}  }} * 100 = {Fr} $$");
            return Fr;
        }

        private float CalcularFrequenciaRelativaAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fr.Sum();
            else
                resultado = fr[pos];

            Passos.WriteLineAsync($"{Titulo(ClassToHTML.AninharEmEm($"Freq. Relativa Acumu. de {ValoresDistintos[pos].Key} [x{pos + 1}] "))}: {resultado}% {HTMLElements.Hr()}");
            return resultado;
        }

    }
}