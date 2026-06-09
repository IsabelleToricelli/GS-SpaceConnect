using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace GS_SpaceConnect.Models
{
    internal class RegraAgricola
    {
        public string Cenario { get; set; } = string.Empty;
        public string Regiao { get; set; } = string.Empty;
        public double TemperaturaMin { get; set; }
        public double TemperaturaMax { get; set; }
        public double UmidadeMin { get; set; }
        public double UmidadeMax { get; set; }
        public double LuminosidadeMin { get; set; }
        public string Recomendacao { get; set; } = string.Empty;

        /// <summary>
        /// Compara se a leitura do sensor está dentro dos limites definidos para o cenário agrícola
        /// </summary>
        /// <param name="temperatura"></param>
        /// <param name="umidade"></param>
        /// <param name="luminosidade"></param>
        /// <returns></returns>
        
        public bool ValidarCondicao(double temperatura, double umidade, double luminosidade)
        {
            return temperatura >= TemperaturaMin && 
                   temperatura <= TemperaturaMax &&
                   umidade >= UmidadeMin
                   && umidade <= UmidadeMax 
                   && luminosidade >= LuminosidadeMin;
        }

    }
}
