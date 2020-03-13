using System;
using System.Collections.Generic;
using System.Text;

namespace Estatistica101
{
    public static class StringExtention
    {
        public static string Normalized(this string value)
        {
            return value.PadLeft(15, ' ');
        }
    }
}
