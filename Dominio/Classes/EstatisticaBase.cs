using Dominio.Decorators;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Estatistica101.Classes
{
    public abstract class EstatisticaBase
    {
        public IList<float> Valores { get; protected set; }
        public float Resultado { get; protected set; }
        public TextWriter Passos { get; protected set; }

        public abstract float Calcular();
        public Exportacao.HTML.ClassToHTML ClassToHTML;
        public EstatisticaBase()
        {
            ClassToHTML = new Exportacao.HTML.ClassToHTML();
            Passos = new StringWriterDecorator();
        }

        public string Titulo(string titulo)
        {
            return ClassToHTML.AninharEmStrong(titulo);
        }
    }
}
