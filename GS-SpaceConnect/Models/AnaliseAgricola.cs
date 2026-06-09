using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS_SpaceConnect.Models
{
    public class AnaliseAgricola
    {
        public int Id { get; set; }
        public string NomeSensor { get; set; }
        public double Temperatura { get; set; }
        public double Umidade { get; set; }

        public double Luminosidade { get; set; }
        public int PropriedadeRuralId { get; set; }

        public PropriedadeRural PropriedadeRural { get; set; }

        public AnaliseAgricola()
        {
            Id = 0;
            NomeSensor = string.Empty;
            Temperatura = 0.0;
            Umidade = 0.0;
            Luminosidade = 0.0;

        }
    }
}
