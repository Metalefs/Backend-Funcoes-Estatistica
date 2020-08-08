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
            Passos.AppendLine($"Cálculo base: <img src='https://localhost:5001/Imagens/desvio-padrao-1.png'>");
            Passos.AppendLine($"$$ Ma = {Media} $$ <br>");
            Passos.AppendLine($"$$ N = {Valores.Count} $$ <hr>");

            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"$$ Xi = {Elemento} $$ ");

                Resultado += (Elemento - Media) * (Elemento - Media);
                Passos.Append(" $$ (" + Elemento + " - " + Media.ToString("F2") + ")^ 2 = " + Resultado + " + $$  <hr>");
            }

            Passos.AppendLine($"\n $$ Resultado = \\sqrt {{ \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}}  }} = {{{(float)Math.Sqrt(Resultado)}}}$$");
            Resultado = (float)Math.Sqrt(Resultado);
            return Resultado;
        }
    }
}
