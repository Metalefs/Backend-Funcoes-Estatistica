using Estatistica101.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Moda : ModaBase
    {
        public Moda(IList<int> Valores):base()
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(int[] Valores):base()
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(IList<float> Valores):base()
        {
            this.Valores = Valores;
        }
        public Moda(float[] Valores) : base()
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            Resultado = Valores.GroupBy(i => i).OrderByDescending(grp => grp.Count())
            .Select(grp => grp.Key).First();

            Repeticoes = Valores.GroupBy(i => i).OrderByDescending(grp => grp.Count())
            .First().Count();

            if (Repeticoes >= 0 && Repeticoes <= 4)
            {
                switch (Resultado)
                {
                    case 0:
                        Classificacao = ClassificacaoModa.Amodal;
                        break;
                    case 1:
                        Classificacao = ClassificacaoModa.Unimodal;
                        break;
                    case 2:
                        Classificacao = ClassificacaoModa.Bimodal;
                        break;
                    case 3:
                        Classificacao = ClassificacaoModa.Trimodal;
                        break;
                    case 4:
                        Classificacao = ClassificacaoModa.Polimodal;
                        break;
                }
            }
            else if(Resultado >= 4)
            {
                Classificacao = ClassificacaoModa.Polimodal;
            }
            Passos.AppendLine($"Moda: Valor mais frequente <br>");
            string ValoresCSV = String.Join(",", Valores);
            Passos.AppendLine($"Elementos: {ValoresCSV} <br>");
            Passos.AppendLine($"Moda: " + (Repeticoes == 1 ? "Não existe moda na série": $"{Resultado}. Repetiu {Repeticoes} vezes"));
            Passos.AppendLine($"Essa série é classificada como: {Enum.GetName(typeof(ClassificacaoModa), Classificacao)}");
            return Resultado;
        }
    }
}
