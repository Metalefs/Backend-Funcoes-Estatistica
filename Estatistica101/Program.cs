using System;

namespace Estatistica101
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string output = string.Empty;
            switch (Convert.ToInt32(args[0]))
            {
                case 1:
                    output = Estatistica.ObterTextoTabelaDistribuicao(args[1]);
                    break;
                case 2:
                    output = Estatistica.ObterDesvioPadrao(args[1]);
                    break;
                case 3:
                    output = Estatistica.ObterVariancia(args[1]);
                    break;
                case 4:
                    output = Estatistica.ObterMedia(args[1]);
                    break;
                case 5:
                    output = Estatistica.ObterModa(args[1]);
                    break;
                case 6:
                    output = Estatistica.ObterMediana(args[1]);
                    break;
            }
            Console.WriteLine(output);
        }  
    }
}
