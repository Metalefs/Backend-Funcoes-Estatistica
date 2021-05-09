using Estatistica101.Classes;
using Exportacao.HTML;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Classes
{
    public class CoeficienteVariacao : EstatisticaBase
    {
        public CoeficienteVariacao(List<float> Valores)
        {
        }

        public override float Calcular()
        {
            DesvioPadrao dp = new DesvioPadrao(Valores);
            Media ma = new Media(Valores);
            dp.Calcular();
            ma.Calcular();
            Passos.WriteLineAsyncCounter($"O {Titulo("Coeficiênte de Variação")} é igual ao quociente entre o desvio padrão e a média:");

            var operacao = dp.Resultado / ma.Resultado;
            Passos.WriteLineAsync($"$$ \\dfrac{{ {{{dp.Resultado}}} }} {{{ma.Resultado}}} = {operacao} $$  {HTMLElements.Hr()}");

            Resultado = operacao;
            return 0f;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
