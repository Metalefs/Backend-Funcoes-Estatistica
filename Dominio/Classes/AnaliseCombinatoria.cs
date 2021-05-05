using System.Collections.Generic;
using System.Linq;

namespace Dominio.Classes
{
    public class AnaliseCombinatoria
    {
        public List<string> ListaAnagramas { get; }

        public AnaliseCombinatoria()
        {
            this.ListaAnagramas = new List<string>();
        }
        public float Fatorial(int valor)
        {
            var resultado = 1;
            for (int cont = valor; cont > 1; cont--)
            {
                resultado *= cont;
            }
            return resultado;
        }

        public float Fatorial(int valor, int possibilidades)
        {
            return valor * possibilidades;
        }

        public float PermutacaoSimples(int valor)
        {
            return Fatorial(valor);
        }

        public float PermutacaoComRepeticao(int valor, int repeticoes)
        {
            return Fatorial(valor, repeticoes);
        }

        public List<string> Anagramas(string palavra)
        {
            return palavra.Permutations().ToList();
        }
       
    }
}
