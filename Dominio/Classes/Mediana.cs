using System;
using System.Collections.Generic;

namespace Estatistica101.Classes
{
    public class Mediana : ModaBase
    {
        public Mediana(IList<int> Valores) : base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Mediana(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Mediana(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public Mediana(float[] Valores):base()
        {
            this.Valores = Valores;
        }

        public override float Calcular()
        {
            int pos = Math.Abs((Valores.Count / 2)-1);
            string ValoresCSV = String.Join(",", Valores);
            Passos.AppendLine($"Elementos: {ValoresCSV} <hr>");
            if (Valores.Count == 1)
            {
                Resultado = Valores[0];
            }
            else if (Valores.Count % 2 != 0)
            {
                Resultado = Valores[Valores.Count / 2];
            }
            else
            {
                Resultado = Math.Abs((Valores[pos] + Valores[pos + 1])/ 2);
                Passos.AppendLine($"Lista de contagem par: $$ \\dfrac{{ {{ {Valores[pos]} + {Valores[pos + 1]} }}  }} {{2}} = \\dfrac{{ {{ {Valores[pos] + Valores[pos + 1]} }}  }} {{2}} = {Resultado} $$");
            }
            Passos.AppendLine($"Mediana: Valores no centro da lista : {Resultado}<br>");
            return Resultado;
        }
    }
}
