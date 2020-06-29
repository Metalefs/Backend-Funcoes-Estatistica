using System;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class DesvioPadrao : EstatisticaBase
    {
        public DesvioPadrao(IList<int> Valores) : base()
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public DesvioPadrao(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public DesvioPadrao(float[] Valores):base()
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            float Media = Valores.Average();
            Passos.AppendLine($"Cálculo base: <img src='https://localhost:5001/Imagens/desvio-padrao-1.png'>");
            Passos.AppendLine($"<br> Ma (Média Aritimética)= {Media}");
            Passos.AppendLine($"<br> N (Quantidade de dados do conjunto)= {Valores.Count}");

            foreach (var Elemento in Valores)
            {
                Passos.AppendLine($"<br> Xi = {Elemento}");

                Resultado += (Elemento - Media) * (Elemento - Media);
                Passos.Append($"<br>  ({Elemento - Media}²) = {Resultado} +");
            }

            Passos.AppendLine($"\n Raiz[{Resultado} / {Valores.Count()}] = {(float)Math.Sqrt(Resultado)}");
            Resultado = (float)Math.Sqrt(Resultado);
            return Resultado;
        }
    }
}
