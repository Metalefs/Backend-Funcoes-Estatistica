using Estatistica101.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class DesvioPadrao
    {
        public IList<float> Valores { get; protected set; }
        public float Resultado { get; protected set; }
        public DesvioPadrao(IList<int> Valores)
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(int[] Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(IList<float> Valores)
        {
            this.Valores = Valores;
        }
        public DesvioPadrao(float[] Valores)
        {
            this.Valores = Valores;
        }
        public double Calcular()
        {
            float Resultado = 0;
            float Media = Valores.Average();
            foreach (var Elemento in Valores)
            {
               Resultado += (Elemento - Media) * (Elemento - Media);
            }
            return Math.Sqrt(Resultado / Valores.Count());
        }
    }
}
