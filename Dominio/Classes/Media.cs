using System;
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
            Passos.AppendLine($"Média Aritimética: $$ \\sum {{ Xi }} \\over n $$ <br>");
            Passos.AppendLine($"Elementos:");

            string ValoresCSV = String.Join(",", Valores);
            Passos.AppendLine($"{ValoresCSV} ");

            Passos.AppendLine("<br>Some todos os termos: ");
            for (int i = 0; i< Valores.Count; i++)
            {
                Passos.AppendLine(Valores[i].ToString());
                if(i < Valores.Count -1)
                    Passos.AppendLine(" +");
            }
            Passos.AppendLine(" = "+Valores.Sum().ToString());
            Passos.AppendLine($"<br>Divida o resultado ({Valores.Sum()}) pelo numero de termos ({Valores.Count()}) ");
            Passos.AppendLine($"<br>Resultado :");
            Passos.AppendLine($"$$ \\dfrac{{ {{{Valores.Sum()}}} }} {{{Valores.Count()}}} = {Resultado} $$");
            return Resultado;
        }
    }
}
