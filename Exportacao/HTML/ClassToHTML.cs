using static Exportacao.HTML.HTMLElements;
namespace Exportacao.HTML
{
    public static class ClassToHTML
    {
        public static string AninharEmElemento(string elemento, string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, $"<{elemento} {propriedades}>");
            conteudo = conteudo.Insert(conteudo.Length, $"</{elemento}>");
            return conteudo;
        }

        public static string AninharEmDiv(string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, HTMLElements.Div(false, propriedades));
            conteudo = conteudo.Insert(conteudo.Length, HTMLElements.Div(true));
            return conteudo;
        }

        public static string AninharEmDiv(ref string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, HTMLElements.Div(false, propriedades));
            conteudo = conteudo.Insert(conteudo.Length, HTMLElements.Div(true));
            return conteudo;
        }

        public static string AninharEmForm(string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, HTMLElements.Form(false, propriedades));
            conteudo = conteudo.Insert(conteudo.Length, HTMLElements.Form(true));
            return conteudo;
        }

        public static string AninharEmParagrafo(string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, HTMLElements.P(false, propriedades));
            conteudo = conteudo.Insert(conteudo.Length, HTMLElements.P(true));
            return conteudo;
        }

        public static string AninharEmLabel(string conteudo, string propriedades = null)
        {
            conteudo = conteudo.Insert(0, HTMLElements.Label(false, propriedades));
            conteudo = conteudo.Insert(conteudo.Length, HTMLElements.Label(true));
            return conteudo;
        }
    }
}
