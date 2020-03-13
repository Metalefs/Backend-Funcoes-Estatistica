using System.Collections.Generic;
using System.Linq;

namespace Estatistica101.Classes
{
    public class Moda : ModaBase
    {
        public Moda(IList<int> Valores)
        {
            foreach(int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(int[] Valores)
        {
            foreach (int valor in Valores)
            {
                this.Valores.Add((float)valor);
            }
        }
        public Moda(IList<float> Valores)
        {
            this.Valores = Valores;
        }
        public Moda(float[] Valores)
        {
            this.Valores = Valores;
        }
        public override float Calcular()
        {
            Resultado = Valores.GroupBy(i => i).OrderByDescending(grp => grp.Count())
            .Select(grp => grp.Key).First();

            if(Resultado >= 0 && Resultado <= 4)
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
            return Resultado;
        }
    }
}
