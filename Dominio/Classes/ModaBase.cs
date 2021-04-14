using Estatistica101.Enums;
using Estatistica101.Interfaces;
using System.Collections.Generic;

namespace Estatistica101.Classes
{
    public abstract class ModaBase : EstatisticaBase, IModa
    {
        public ModaBase() : base() { }
        public ClassificacaoModa Classificacao { get; protected set; }
        public float Repeticoes { get; protected set; }

        public List<float> Modas { get; protected set; }
    }
}
