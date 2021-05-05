using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class DesvioPadrao : EstatisticaBase
    {
        public DesvioPadrao(IList<int> Valores) : base()
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public DesvioPadrao(float[] Valores):base()
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            float Media = Valores.Average();
            Passos.WriteLineAsyncCounter($"O {Titulo("Desvio padrão")} é uma medida que expressa o grau de dispersão de um conjunto de dados:");
            Passos.WriteLineAsyncCounter($" {HTMLElements.Img(Properties: "src='https://dados-agrupados-api.herokuapp.com/Imagens/desvio-padrao-1.png'")}");
            Passos.WriteLineAsyncCounter($"Obter a média aritimética dos dados (Ma) = {Media}");
            Passos.WriteLineAsyncCounter($"Obter o número de termos (N) = {Valores.Count}");
            Passos.WriteLineAsyncCounter($"(Somátorio de Xi = 1 até a posição N ({Valores.Count}) menos a média ({Media})) elevado ao quadrado{HTMLElements.Hr()}");
            int xi = 1;
            foreach (var Elemento in Valores)
            {
                //Passos.WriteLineAsync($"Termo (x{xi}) = {Elemento}");
                var operacao = (Elemento - Media) * (Elemento - Media);
                Resultado += operacao;
                Passos.WriteLineAsync($"$$ {(xi > 1 ? '+' : ' ')}(" + Elemento + " - " + Media.ToString("F2") + $")^ 2 = {operacao} ..." + Resultado + $"  $$  {HTMLElements.Hr()}");
                xi++;
            }

            Passos.WriteLineAsyncCounter($"Obter a raíz quadrada da divisão do somatório ({Resultado}) pelo numero de termos ({Valores.Count}) {HTMLElements.Hr()}");
            var operacaoFinal = (float)Math.Sqrt(Resultado / Valores.Count());
            Passos.WriteLineAsync($"\n $$ Resultado = \\sqrt {{ \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}}  }} = {{{operacaoFinal}}}$$");
            Resultado = operacaoFinal;
            return Resultado;
        }
    }
}
