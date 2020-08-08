using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Variancia : EstatisticaBase
    {
        public Variancia(IList<int> Valores):base()
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public Variancia(float[] Valores):base()
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            float Media = Valores.Average();
            Passos.AppendLine($"Variância: medida de dispersão =  $$ \\sum_ {{Xi - Ma²}} \\over n $$ <br>");
            Passos.AppendLine($"$$ Ma = {Media} $$ <br>");
            Passos.AppendLine($"$$ N = {Valores.Count} $$ <hr>");

            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"$$ Xi = {Elemento} $$ ");

                Resultado += (Elemento - Media) * (Elemento - Media);
                Passos.Append(" $$ " + Elemento +" - "+ Media.ToString("F2") + "^ 2 = " + Resultado+ " + $$  <hr>");
            }

            Passos.AppendLine($"\n $$ Resultado = \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}} = {{{Resultado /= Valores.Count()}}} $$");

            return Resultado;
        }
    }
}
