using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.Model
{
    public class BilansStanja
    {
        public BilansStanja()
        {
        }

        public BilansStanja(int idbs, string idl, decimal saldo, decimal dug, decimal kamata)
        {
            Idbs = idbs;
            Idl = idl;
            Saldo = saldo;
            Dug = dug;
            Kamata = kamata;
        }

        public int Idbs { get; set; }
        public string Idl { get; set; }
        public decimal Saldo { get; set; }
        public decimal Dug { get; set; }
        public decimal Kamata { get; set; }



    }
}
