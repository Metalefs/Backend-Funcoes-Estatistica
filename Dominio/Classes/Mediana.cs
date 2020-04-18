using System;
using System.Collections.Generic;

namespace Estatistica101.Classes
{
    public class Mediana : ModaBase
    {
        public Mediana(IList<int> Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Mediana(int[] Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Mediana(IList<float> Valores)
        {
            this.Valores = Valores;
        }
        public Mediana(float[] Valores)
        {
            this.Valores = Valores;
        }

        public override float Calcular()
        {
            Resultado = Valores[Math.Abs(Valores.Count / 2)];
            return Resultado;
        }
    }
}
