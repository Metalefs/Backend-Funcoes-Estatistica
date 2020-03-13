using System.Collections.Generic;

namespace Estatistica101.Interfaces
{
    public interface ITabelaDistribuicao
    {
        List<float> Valores { get; }
        int NumeroDeElementos { get; }
        float Amplitude { get; }
        float QuantidadeIntervalos { get; }
        float Intervalo { get; }
        string[] intervalo { get; }
        float[] xi { get; }
        float[] fi { get; }
        float[] Fi { get; }
        float[] fr { get; }
        float[] Fr { get; }

        public void Calcular();
    }
}