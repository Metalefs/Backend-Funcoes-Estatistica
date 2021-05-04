using Estatistica101.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Classes
{
    public class TabelaDesvioPadrao : TabelaDistribuicao
    {
        public List<float> QuadradoDoTermo { get; }
        public List<float> FrequenciaSimplesVezesOTermo { get; }
        public List<float> FrequenciaSimplesVezesOQuadradoDoTermo { get; }

        public float SomatorioFrequenciaSimplesVezesOTermo
        { 
            get {
                return FrequenciaSimplesVezesOTermo.Sum();
            }
        }
        public float SomatorioFrequenciaSimplesVezesOQuadradoDoTermo
        {
            get
            {
                return FrequenciaSimplesVezesOQuadradoDoTermo.Sum();
            }
        }

        public float MediaDp { get {
                return SomatorioFrequenciaSimplesVezesOTermo / 100;
            }
        }

        public double DesvioPadraoTabela { get {
                return Math.Sqrt(Math.Abs((SomatorioFrequenciaSimplesVezesOQuadradoDoTermo / NumeroDeElementos) - (MediaDp * MediaDp)));
            }
        }

        //public float AmostraDp { get {
        //        return (float) Math.Sqrt(MediaDp);
        //    } 
        //}   
        //public float PopulacaoDp { get {
        //        return (float) Math.Sqrt(MediaDp);
        //    } 
        //}

        public TabelaDesvioPadrao(List<float> Valores) : base(Valores)
        {
            QuadradoDoTermo = new List<float>();
            FrequenciaSimplesVezesOTermo = new List<float>();
            FrequenciaSimplesVezesOQuadradoDoTermo = new List<float>();
        }

        public override float Calcular()
        {
            base.Calcular();

            if (Simples)
            {
                for(int i=0; i < ValoresDistintos.Count; i++)
                {
                    var xiAtual = xi[i];
                    var fiAtual = fi[i];
                    var quadradoDeXi = (float)xiAtual * xiAtual;
                    QuadradoDoTermo.Add(quadradoDeXi);
                    FrequenciaSimplesVezesOTermo.Add(fiAtual * xiAtual);
                    FrequenciaSimplesVezesOQuadradoDoTermo.Add((fiAtual * quadradoDeXi));
                }
            }
            else
            {
                for (int i = 0; i < QuantidadeIntervalos; i++)
                {
                    var xiAtual = xi[i];
                    var fiAtual = fi[i];
                    var quadradoDaFI = (float)fiAtual * fiAtual;
                    QuadradoDoTermo.Add(quadradoDaFI);
                    FrequenciaSimplesVezesOTermo.Add(fiAtual * xiAtual);
                    FrequenciaSimplesVezesOQuadradoDoTermo.Add((fiAtual * quadradoDaFI));
                }
            }
            return 0f;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
