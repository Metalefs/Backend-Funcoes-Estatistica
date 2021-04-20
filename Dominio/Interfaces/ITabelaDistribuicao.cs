using System.Collections.Generic;

namespace Estatistica101.Interfaces
{
    public interface ITabelaDistribuicao
    {
        List<float> Valores { get; }
        int NumeroDeElementos { get; }
        float Amplitude { get; }
        double QuantidadeIntervalos { get; }
        float Intervalo { get; }
        List<string> intervalos { get; }
        List<float> xi { get; }
        List<float> fi { get; }
        List<float> Fi { get; }
        List<float> fr { get; }
        List<float> Fr { get; }

        public float Calcular();
    }
}