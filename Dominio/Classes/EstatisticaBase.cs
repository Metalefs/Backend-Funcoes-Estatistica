using System.Collections.Generic;
using System.Text;

namespace Estatistica101.Classes
{
    public abstract class EstatisticaBase<T>
    {
        public IList<float> Valores { get; protected set; }
        public float Resultado { get; protected set; }
        public StringBuilder Passos { get; protected set; }

        public abstract T Calcular();

        public EstatisticaBase()
        {
            Passos = new StringBuilder();
        }
    }
}
