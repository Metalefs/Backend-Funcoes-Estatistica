using Estatistica101.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Estatistica101.Interfaces
{
    public interface IModa
    {
        public ClassificacaoModa Classificacao { get; }
        public float Resultado { get; }
    }
}
