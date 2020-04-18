using Estatistica101.Enums;

namespace Estatistica101.Interfaces
{
    public interface IModa
    {
        public ClassificacaoModa Classificacao { get; }
        public float Resultado { get; }
        public float Calcular();
    }
}
