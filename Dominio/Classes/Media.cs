using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Media : EstatisticaBase
    {
        public Media(IList<int> Valores) : base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Media(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Media(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public Media(float[] Valores):base()
        {
            this.Valores = Valores;
        }

        public override float Calcular()
        {
            Resultado = Valores.Average();
            Passos.AppendLine($"Média Aritimética: (E Xi)/n = {Valores.Sum()} / {Valores.Count} = {Resultado}");
            return Resultado;
        }
    }
}
