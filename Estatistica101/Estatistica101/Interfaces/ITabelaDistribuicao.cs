using System.Collections.Generic;

namespace Distribuicao.DadosAgrupados
{
    public interface ITabelaDistribuicao
    {
        List<float> Valores { get; set; }
        int NumeroDeElementos { get; }
        float Amplitude { get; }
        float QuantidadeIntervalos { get; }
        float Intervalo { get; }

        float CalcularAmplitude(float ValorMinimo, float ValorMaximo);
        float CalcularTamanhoIntervalo(float Amplitude, float QuantidadeIntervalos);
        float CalcularQuantidadeIntervalos(int NumeroElementos);
        float CalcularFrequenciaSimples(float Abertura, float Fim);
        void GerarTabela();
    }
}