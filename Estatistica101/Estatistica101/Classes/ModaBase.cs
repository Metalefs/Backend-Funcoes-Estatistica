using Estatistica101.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estatistica101.Classes
{
    public abstract class ModaBase : IModa
    {
        public ClassificacaoModa Classificacao { get; protected set; }
        public IList<float> Valores { get; protected set; }
        public float Resultado { get; protected set; }


    }
}
