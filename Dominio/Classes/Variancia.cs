using Estatistica101.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Variancia : ModaBase
    {
        public Variancia(IList<int> Valores)
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(int[] Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(IList<float> Valores)
        {
            this.Valores = Valores;
        }
        public Variancia(float[] Valores)
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            float Media = Valores.Average();
            foreach (var Elemento in Valores)
            {
               Resultado += (Elemento - Media) * (Elemento - Media);
            }
            return Resultado / Valores.Count();
        }
    }
}
