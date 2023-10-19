using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.Model
{
    public class Lice
    {
        public Lice(string idl, string imel, string przl, string vrstal, int mes_PrihodL)
        {
            Idl = idl;
            Imel = imel;
            Przl = przl;
            Vrstal = vrstal;
            Mes_PrihodL = mes_PrihodL;
        }

        public string Idl { get; set; }
        public string Imel { get; set; }
        public string Przl { get; set; }
        public string Vrstal { get; set; }
        public int Mes_PrihodL { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }




    }
}
