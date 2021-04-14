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
            Passos.AppendLine($"<strong>Variância</strong>: A variância mede quão dispersos estão os dados na amostra. =  $$ \\sum_ {{Xi - Ma²}} \\over n $$ <br>");
            Passos.AppendLine($"Calcule a média (Ma) = {Media}<br>");
            Passos.AppendLine($"Calcule o número de termos (N) = {Valores.Count}<hr>");
            int xi = 1;
            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"( elemento na posição (X{xi}): {Elemento} - Média aritimética: {Media} ) elevado ao quadrado ");

                var operacao = (Elemento - Media) * (Elemento - Media);
                Resultado += operacao;
                Passos.Append(" $$ (" + Elemento +" - "+ Media.ToString("F2") + $")^ 2 [{operacao}]= " + Resultado+ " + $$  <hr>");
                xi++;
            }

            Passos.AppendLine($"\n $$ Resultado: \\dfrac{{ {{{Resultado}}} }} {{{Valores.Count()}}} = {{{Resultado /= Valores.Count()}}} $$");

            return Resultado;
        }
    }
}
