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
            Passos.AppendLine($"Variância: medida de dispersão = E Raiz(Xi - Ma)² /n");
            Passos.AppendLine($"Ma = {Media}");
            Passos.AppendLine($"N = {Valores.Count}");

            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"Xi = {Elemento}");

                Resultado += (Elemento - Media) * (Elemento - Media);
                Passos.Append($" ({Elemento - Media}²) = {Resultado} +");
            }

            Passos.AppendLine($"\n {Resultado} / {Valores.Count()} = {Resultado /= Valores.Count()}");
            return (float)Math.Sqrt(Resultado);
        }
    }
}
