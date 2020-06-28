using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exportacao.HTML
{
    public class PropertyGenerator
    {
        public static string CreateHTMLProperty(string propName, string propValue)
        {
            return string.Format(" {0}=\"{1}\" ", propName, propValue);
        }

        public static string CreateHTMLProperties(List<KeyValuePair<string, string>> styles)
        {
            StringBuilder properties = new StringBuilder();
            for (int i = 0; i < styles.Count; i++)
            {
                properties.Append($" {styles.ElementAt(i).Key}=\"{styles.ElementAt(i).Value}\" ");
            }
            return properties.ToString();
        }

        public static string CreateFormProperties(string action, string method)
        {
            StringBuilder properties = new StringBuilder();

            properties.Append($" action=\"{action}\" method=\"{method}\" ");

            return properties.ToString();
        }

        public static string CreateHTMLStyle(List<KeyValuePair<string, string>> styles)
        {
            StringBuilder style = new StringBuilder("style=\"");
            for (int i = 0; i < styles.Count; i++)
            {
                style.Append($"{styles.ElementAt(i).Key} : {styles.ElementAt(i).Value};");
            }
            style.Append("\"");
            return style.ToString();
        }

    }
}
