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
            Passos.AppendLine($"O desvio padrão é uma medida que expressa o grau de dispersão de um conjunto de dados:");
            Passos.AppendLine($"<br><img src='https://dados-agrupados-api.herokuapp.com/Imagens/desvio-padrao-1.png'>");
            Passos.AppendLine($"<br>Obter a média aritimética dos dados (Ma) = {Media}");
            Passos.AppendLine($"<br>Obter o número de termos (N) = {Valores.Count}");
            Passos.AppendLine($"<hr>(Somátorio de Xi = 1 até a posição N ({Valores.Count}) vezes a média ({Media})) elevado ao quadrado<hr>");
            int xi = 1;
            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"termo (X{xi}) = {Elemento} <br>");
                var operacao = (Elemento - Media) * (Elemento - Media);
                Resultado += operacao;
                Passos.Append($" $$ {(xi > 1 ? '+' : ' ')}(" + Elemento + " - " + Media.ToString("F2") + $")^ 2 [{operacao}]= " + Resultado + "  $$  <hr>");
                xi++;
            }

            Passos.AppendLine($"Obter a raíz quadrada da divisão do somatório ({Resultado}) pelo numero de termos ({Valores.Count}) <hr>");

            Passos.AppendLine($"\n $$ Resultado = \\sqrt {{ \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}}  }} = {{{(float)Math.Sqrt(Resultado)}}}$$");
            Resultado = (float)Math.Sqrt(Resultado);
            return Resultado;
        }
    }
}
