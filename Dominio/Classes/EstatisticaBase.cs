using Dominio.Decorators;
using Exportacao.HTML;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Estatistica101.Classes
{
    public abstract class EstatisticaBase
    {
        public IList<float> Valores { get; protected set; }
        public float Resultado { get; protected set; }
        public StringWriterDecorator Passos { get; protected set; }

        public abstract float Calcular();
        public EstatisticaBase()
        {
            Passos = new StringWriterDecorator();
        }

        public string Titulo(string titulo)
        {
            return ClassToHTML.AninharEmStrong(titulo);
        }
    }
}
