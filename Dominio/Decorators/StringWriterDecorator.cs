using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dominio.Decorators
{
    public class StringWriterDecorator: StringWriter
    {
        public StringWriterDecorator():base()
        {
            NewLine = HTMLElements.Br();
        }

        public string Valor { get => ToString(); }
        [JsonIgnore]
        public override Encoding Encoding { get; }
        [JsonIgnore]
        public override IFormatProvider FormatProvider { get; }
        [JsonIgnore]
        public override string NewLine { get; set; }

        public override Task WriteLineAsync(string? value)
        {
            return base.WriteLineAsync(value + HTMLElements.Br());
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
