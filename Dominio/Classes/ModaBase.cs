using Estatistica101.Enums;
using Estatistica101.Interfaces;

namespace Estatistica101.Classes
{
    public abstract class ModaBase : EstatisticaBase<float>, IModa
    {
        public ModaBase() : base() { }
        public ClassificacaoModa Classificacao { get; protected set; }
        public float Repeticoes { get; protected set; }
    }
}
