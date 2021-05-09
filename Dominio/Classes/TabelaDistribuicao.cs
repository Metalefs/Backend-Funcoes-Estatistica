using Dominio.Classes;
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
        public CoeficienteVariacao CoeficienteVariacao { get; set; }

        public bool Simples { get; set; }

        public TabelaDistribuicao(List<float> Valores) : base()
        {
            intervalos = new List<string>();
            xi = new List<float>();
            fi = new List<float>();
            Fi = new List<float>();
            fr = new List<float>();
            Fr = new List<float>();


            Moda = new Moda(Valores);
            Mediana = new Mediana(Valores);
            DesvioPadrao = new DesvioPadrao(Valores);
            Variancia = new Variancia(Valores);
            Media = new Media(Valores);
            CoeficienteVariacao = new CoeficienteVariacao(Valores);

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
            Passos.WriteLineAsyncCounter($"{Titulo("Ordene os dados (K):")} {GerarTabelaDeFrequencia()}");

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

            Moda.Calcular();
            //Passos.Append(Moda.Passos.ToString());

            Mediana.Calcular();
            //Passos.Append(Mediana.Passos.ToString());

            DesvioPadrao.Calcular();
            //Passos.Append(DesvioPadrao.Passos.ToString());

            Variancia.Calcular();
            //Passos.Append(Variancia.Passos.ToString());

            Media.Calcular();
            //Passos.Append(Media.Passos.ToString());

            CoeficienteVariacao.Calcular();

            Passos.Close();
            return 0f;
        }

        private string GerarTabelaDeFrequencia()
        {
            List<KeyValuePair<string, string>> CamposTabelaFrequencia = new List<KeyValuePair<string, string>>();
            ValoresDistintos.ForEach(valor =>
            {
                CamposTabelaFrequencia.Add(new KeyValuePair<string, string>(valor.Key.ToString(), valor.Value.ToString()));
            });
            return ClassToHTML.MontarTabela(CamposTabelaFrequencia, "class='table '");
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
            Passos.WriteLineAsyncCounter($"{Titulo("Calcule a Amplitude (A)")}: $$ A = ValorMaximo - ValorMinimo  $$");
            Passos.WriteLineAsync($"$$ A = {ValorMaximo} - {ValorMinimo} = {Amplitude} $$");
            return Amplitude;
        }

        private double CalcularQuantidadeIntervalosK(int NumeroDeElementos, bool HEraDecimal = false)
        {
            if (HEraDecimal)
            {
                var sqrt = Math.Sqrt(Valores.Count);
                var K = Math.Floor(sqrt);
                if(!Simples)
                QuantidadeIntervalos = K;

                Passos.WriteLineAsync($"$$ K = \\sqrt {NumeroDeElementos} = {sqrt} ... { K} $$");
                return QuantidadeIntervalos;
            }
            Passos.WriteLineAsyncCounter($"{Titulo("Calcule a Quantidade de Intervalos (h)")}:");
            Passos.WriteLineAsyncCounter($"{Titulo("- Tabela de Truman")}:");
            Passos.WriteLineAsyncCounter($"Usado para o caso N descrito:");
            Passos.WriteLineAsync(GerarTabelaDeTruman());

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
            Passos.WriteLineAsync($"{Titulo("Calcule o Tamanho do Intervalo (H)")}: {HTMLElements.Br()} $$ H = \\dfrac{{ {{A}} }} {{K}} $$");
            Passos.WriteLineAsync($"$$ H = \\dfrac{{ {{{Amplitude}}} }} {{{QuantidadeIntervalos}}} = {Intervalo} $$");

            Intervalo = (float) ((Intervalo % 1) > 0 ? Amplitude / RecalcularK() : Intervalo);

            return Intervalo;
        }

        private double RecalcularK()
        {

            Passos.WriteLineAsync($"{Titulo("Se o intervalo possuir casas decimais, considere $$ K =  \\sqrt N  $$ ")}");
            return CalcularQuantidadeIntervalosK(Valores.Count, HEraDecimal:true);
        }

        private void  CalcularTodosOsIntervalos()
        {
            float Abertura = ValorMinimo;
            Passos.WriteLineAsyncCounter($"{Titulo("Calcular Abertura do Intervalo")}: Começa pelo Valor Minimo = {ValorMinimo} {HTMLElements.Hr()}");
            for (int i = 0; i < QuantidadeIntervalos; i++)
            {
                try
                {
                    if (!Simples)
                    {
                        float FimIntervalo = Abertura + Intervalo;
                        intervalos.Add($"{Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")}");
                        Passos.WriteLineAsyncCounter($"{Titulo("Calcule o Final do Intervalo")}: Abertura + Intervalo ");
                        Passos.WriteLineAsync($"$$ {Abertura} + {Intervalo} = {Abertura + Intervalo}$$");
                        Passos.WriteLineAsyncCounter($"{Titulo("Calcule o Intervalo")}: Abertura |-- Fim ");
                        Passos.WriteLineAsync($"$${Abertura.ToString("0.00")}|--{FimIntervalo.ToString("0.00")} $$");

                        xi.Add(CalcularMediaXI(Abertura, FimIntervalo));
                        fi.Add(CalcularFrequenciaSimples(Abertura, FimIntervalo));

                        Fi.Add(CalcularFrequenciaSimplesAcumulada(i));
                
                        fr.Add(CalcularFrequenciaRelativa(i));
                        Fr.Add(CalcularFrequenciaRelativaAcumulada(i));

                        Abertura = FimIntervalo;
                    }
                    else
                    {
                        Passos.WriteLineAsyncCounter($"{Titulo("Freq. Simples de ")} {ClassToHTML.AninharEmEm($"{ValoresDistintos[i].Key} [x{i + 1}]")}: {ValoresDistintos[i].Value}");
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
            Passos.WriteLineAsync($"{Titulo("Média do intervalo")}: $$ Ma = {{ \\dfrac{{ {{Abertura + Fim}} }} {{2}}  }}$$");
            Passos.WriteLineAsync($"$$ Ma = {{ \\dfrac{{ {{{Abertura + Fim}}} }} {{{2}}}  }} = {resultado} $$");
            return resultado;
        }

        private float CalcularFrequenciaSimples(float Abertura, float Fim)
        {
            float resultado = 0;
            List<float> resultadosFS = new List<float>();
            if (Fim >= ValorMaximo)
            {
                resultadosFS = Valores.Where(x => x >= Abertura && x <= Fim).ToList();
                resultado = Valores.Where(x => x >= Abertura && x <= Fim).Count();
            }
            else
            {
                resultadosFS = Valores.Where(x => x >= Abertura && x < Fim).ToList();
                resultado = Valores.Where(x => x >= Abertura && x < Fim).Count();
            }


            Passos.WriteLineAsyncCounter($"{Titulo("Freq. Simples")}: Contagem de valores entre {Abertura} e {Fim} : {resultado}");
            Passos.WriteLineAsync($"{GerarTabelaDeFS(resultadosFS)}");
            return resultado;
        }

        private string GerarTabelaDeFS(List<float> FS)
        {
            List<KeyValuePair<string, string>> CamposTabelaFS = new List<KeyValuePair<string, string>>();
            FS.ForEach(valor =>
            {
                CamposTabelaFS.Add(new KeyValuePair<string, string>("", valor.ToString()));
            });

            return ClassToHTML.MontarTabela(CamposTabelaFS, "class='table '");
        }

        private float CalcularFrequenciaSimplesAcumulada(int pos)
        {
            Passos.WriteLineAsyncCounter($"{Titulo("Freq. Simples Acum. de")} {ClassToHTML.AninharEmEm($"{ValoresDistintos[pos].Key}")} {ClassToHTML.AninharEmEm($"[x{pos + 1}]")}: {(pos > 0 ? fi.Sum().ToString(): fi[pos].ToString())} ");
            float resultado;
            if (pos > 0)
            {
                resultado = fi.Sum();
            }
            else
            {
                resultado = fi[pos];
            }
            Passos.WriteLineAsync($"{GerarTabelaDeFsAcumulada(pos,resultado)}");

            return resultado;
        }

        private string GerarTabelaDeFsAcumulada(int pos, float resultado)
        {
            List<KeyValuePair<string, string>> CamposTabelaFSAcumulada = new List<KeyValuePair<string, string>>();
            int idx = 1;
            fi.ForEach((valor) =>
            {
                CamposTabelaFSAcumulada.Add(new KeyValuePair<string, string>($"FI{idx}", valor.ToString()));
                idx++;
            });
            CamposTabelaFSAcumulada.Add(new KeyValuePair<string, string>("Resultado", resultado.ToString()));

            return ClassToHTML.MontarTabela(CamposTabelaFSAcumulada, "class='table '");
        }

        private float CalcularFrequenciaRelativa(int pos)
        {
            float Fr = fi[pos] / NumeroDeElementos * 100;
            Passos.WriteLineAsyncCounter($"{Titulo(ClassToHTML.AninharEmEm($"Freq. Relativa de {ClassToHTML.AninharEmEm($"{ValoresDistintos[pos].Key}")} [x{pos + 1}]"))}: ");
            if(pos == 0) Passos.WriteLine($" $$ Fr = {{ \\dfrac{{ {{Fi}} }} {{N}}  }} * 100 $$");
            else Passos.WriteLine($" $$ Fi = {fi[pos]} $$ $$ N = {NumeroDeElementos}  $$");
            Passos.WriteLineAsync($"$$  Fr = {{ \\dfrac{{ {{{fi[pos]}}} }} {{{NumeroDeElementos}}}  }} * 100 = {Fr} $$");
            return Fr;
        }

        private float CalcularFrequenciaRelativaAcumulada(int pos)
        {
            float resultado;
            if (pos > 0)
                resultado = fr.Sum();
            else
                resultado = fr[pos];
            Passos.WriteLineAsyncCounter($"{Titulo(ClassToHTML.AninharEmEm($"Freq. Relativa Acumu. de {ClassToHTML.AninharEmEm($"{ValoresDistintos[pos].Key}")} [x{pos + 1}] "))}: {resultado}% {HTMLElements.Hr()}");
            Passos.WriteLineAsync($"{GerarTabelaDeFrAcumulada(pos,resultado)}");

            return resultado;
        }

        private string GerarTabelaDeFrAcumulada(int pos, float resultado)
        {
            List<KeyValuePair<string, string>> CamposTabelaFrAcumulada = new List<KeyValuePair<string, string>>();
            int idx = 1;
            fr.ForEach(valor =>
            {
                CamposTabelaFrAcumulada.Add(new KeyValuePair<string, string>($"FR{idx}", valor.ToString()));
                idx++;
            });
            CamposTabelaFrAcumulada.Add(new KeyValuePair<string, string>("Resultado", resultado.ToString()));

            return ClassToHTML.MontarTabela(CamposTabelaFrAcumulada, "class='table '");
        }

    }
}