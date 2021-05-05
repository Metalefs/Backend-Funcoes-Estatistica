using Exportacao.HTML;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Variancia : EstatisticaBase
    {
        public Variancia(IList<int> Valores):base()
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Variancia(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public Variancia(float[] Valores):base()
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            float Media = Valores.Average();
            Passos.WriteLineAsyncCounter($"{Titulo("Variância")}: A variância mede quão dispersos estão os dados na amostra. =  $$ \\sum_ {{Xi - Ma²}} \\over n $$");
            Passos.WriteLineAsyncCounter($"Calcule a média (Ma) = {Media}");
            Passos.WriteLineAsyncCounter($"Calcule o número de termos (N) = {Valores.Count} {HTMLElements.Hr()}");
            int xi = 1;
            foreach (var Elemento in Valores)
            {
                //Passos.WriteLineAsync($" $$ ( x{xi} - Ma ) ^2 $$");

                var operacao = (Elemento - Media) * (Elemento - Media);
                Resultado += operacao;
                Passos.WriteLineAsync(" $$ (" + Elemento +" - "+ Media.ToString("F2") + $")^ 2 = {operacao} ..." + Resultado+ $" + $$ {HTMLElements.Hr()}");
                xi++;
            }

            Passos.WriteLineAsync($"\n $$ Resultado: \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}} = {{{Resultado /= Valores.Count()}}} $$");

            return Resultado;
        }
    }
}
