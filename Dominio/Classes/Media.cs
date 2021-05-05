using Exportacao.HTML;
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
            Passos.WriteLineAsyncCounter($"{Titulo("Média Aritimética")}: $$ \\sum {{ Xi }} \\over n $$");
            Passos.WriteLineAsyncCounter($"Elementos:");

            string ValoresCSV = String.Join(",", Valores);
            string ValoresSoma = String.Join(" + ", Valores);
            Passos.WriteLineAsyncCounter($"{ValoresCSV}");

            Passos.WriteLineAsyncCounter("Some todos os termos: ");
           

            Passos.WriteLineAsync($"$$ {ValoresSoma} $$");

            Passos.WriteLineAsync($"$$ = {Valores.Sum().ToString()} $$");
            Passos.WriteLineAsyncCounter($"Divida o resultado ({Valores.Sum()}) pelo numero de termos ({Valores.Count()}) ");
            Passos.WriteLineAsync($"$$ Resultado = \\dfrac{{ {{{Valores.Sum()}}} }} {{{Valores.Count()}}} = {Resultado} $$");
            return Resultado;
        }
    }
}
