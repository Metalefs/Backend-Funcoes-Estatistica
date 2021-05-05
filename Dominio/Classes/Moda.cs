using Estatistica101.Enums;
using Exportacao.HTML;
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

            var _Repeticoes = Valores.GroupBy(i => i).OrderByDescending(grp => grp.Count())
            .First().Count();

            Modas = Valores.GroupBy(i => i).Where(x => x.Count() == _Repeticoes).Distinct().OrderByDescending(grp => grp.Count()).Select(x=>x.Key).ToList();

            string RepeticoesCSV = String.Join(",", Modas);

            Repeticoes = _Repeticoes == 1 ? 0 : _Repeticoes;

            if (Repeticoes >= 0)
            {
                switch (Modas.Count)
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
                    default:
                        Classificacao = ClassificacaoModa.Polimodal;
                        break;
                }
            }
            else if(Resultado >= 4)
            {
                Classificacao = ClassificacaoModa.Polimodal;
            }
            Passos.WriteLineAsyncCounter($"{Titulo("Moda")}: Valor mais frequente ");
            string ValoresCSV = String.Join(",", Valores);
            Passos.WriteLineAsyncCounter($"{Titulo("Moda")}: " + (Repeticoes == 1 ? "Não existe moda na série": $"{RepeticoesCSV}. Repetiu {Repeticoes} vezes"));
            Passos.WriteLineAsyncCounter($"Essa série é classificada como: {Enum.GetName(typeof(ClassificacaoModa), Classificacao)} ");
            return Resultado;
        }
    }
}
