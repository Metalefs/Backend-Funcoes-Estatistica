using System.Collections.Generic;
using System.Text;
using static Exportacao.HTML.HTMLElements;
namespace Exportacao.HTML
{
    public static class ClassToHTML
    {
        public static string AninharEmElemento(string elemento, string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, $"<{elemento} {propriedades}>");
            sb.Insert(sb.Length, $"</{elemento}>");
            return sb.ToString();
        }

        public static string MontarTabela(List<KeyValuePair<string,string>> tabela, string propriedades = null)
        {
            StringBuilder conteudo = new StringBuilder($"<table {propriedades}>");
            conteudo.Append("<thead>");
            conteudo.Append("<tr>");
            tabela.ForEach(x =>
            {
                conteudo.Append(AninharEmElemento("th", x.Key));
            });
            conteudo.Append("</tr>");
            conteudo.Append("</thead>");
            conteudo.Append("<tbody>");
            conteudo.Append("<tr>");
            tabela.ForEach(x =>
            {
                conteudo.Append(AninharEmElemento("td", x.Value));
            });
            conteudo.Append("</tr>");
            conteudo.Append("</tbody>");
            conteudo.Append("</table>");
            return conteudo.ToString();
        }

        public static string AninharEmDiv(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Div(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.Div(true));
            return sb.ToString();
        }

        public static string AninharEmDiv(ref string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Div(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.Div(true));
            return sb.ToString();
        }

        public static string AninharEmForm(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Form(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.Form(true));
            return sb.ToString();
        }

        public static string AninharEmParagrafo(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.P(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.P(true));
            return sb.ToString();
        }
        public static string AninharEmEm(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Em(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.Em(true));
            return sb.ToString();
        }
        
        public static string AninharEmStrong(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Strong(false, propriedades));
            sb.Insert(sb.Length, HTMLElements.Strong(true));
            return sb.ToString();
        }

        public static string AninharEmLabel(string conteudo, string propriedades = null)
        {
            StringBuilder sb = new StringBuilder(conteudo);
            sb.Insert(0, HTMLElements.Label(false, propriedades));
            sb.Insert(conteudo.Length, HTMLElements.Label(true));
            return sb.ToString();
        }

    }
}
