using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Classes
{
    public class AnaliseCombinatoria
    {
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
            List<string> Anagramas = new List<string>();
            for(int i = 0; i < palavra.Length; i++)
            {
                string letrasRestantes = palavra.Remove(i,1);
                for (int j = 0; j < letrasRestantes.Length; j++)
                {
                    Anagramas.Add(palavra[i] + letrasRestantes[j] + letrasRestantes.Remove(j, 1));
                }
            }
            return Anagramas;
        }

        //public float CombinacaoSimples(int valor)
        //{

        //}

        //public float Arranjo(int valor)
        //{

        //}
    }
}
