using Estatistica101.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Moda : ModaBase
    {
        public Moda(IList<int> Valores)
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(int[] Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(IList<float> Valores)
        {
            this.Valores = Valores;
        }
        public Moda(float[] Valores)
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
