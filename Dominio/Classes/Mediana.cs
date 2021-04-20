using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.Linq;

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
            Valores = Valores.ToList().OrderBy(x => x).ToList();

            Passos.WriteLineAsync($"Elementos: {ValoresCSV}");
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
                Passos.WriteLineAsync($"Lista de contagem par:");
                Passos.WriteLineAsync($"Some os dois elementos do centro ({Valores[pos]} e {Valores[pos + 1]}) e divida por 2(dois):");
                Resultado = Math.Abs((Valores[pos] + Valores[pos + 1])/ 2);
                Passos.WriteLineAsync($"$$ \\dfrac{{ {{ {Valores[pos]} + {Valores[pos + 1]} }}  }} {{2}} = \\dfrac{{ {{ {Valores[pos] + Valores[pos + 1]} }}  }} {{2}} = {Resultado} $$");
            }
            Passos.WriteLineAsync($"{HTMLElements.Hr()} {Titulo("Mediana")}: Valores no centro da lista : {Resultado}");
            return Resultado;
        }
    }
}
