using Estatistica101.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private void Calcular()
        {
            Resultado = Valores.GroupBy(i => i).OrderByDescending(grp => grp.Count())
            .Select(grp => grp.Key).First();
        }
    }
}
