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
            Passos.AppendLine($"Variância: medida de dispersão = E(Xi - Ma²)/n");
            Passos.AppendLine($"Ma = {Media}");
            Passos.AppendLine($"N = {Valores.Count}");

            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"Xi = {Elemento}");

                Resultado += (Elemento - Media) * (Elemento - Media);
                Passos.Append($" ({Elemento - Media}²) = {Resultado} +");
            }

            Passos.AppendLine($"\n {Resultado} / {Valores.Count()} = {Resultado /= Valores.Count()}");

            return Resultado;
        }
    }
}
