using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.Model
{
    public class VrstaObjekta
    {
        public int Idvo { get; set; }
        public string Nazivvo { get; set; }

        public VrstaObjekta(int Idvo)
        {
            this.Idvo = Idvo;
        }

        public VrstaObjekta(int idvo, string nazivvo)
        {
            Idvo = idvo;
            Nazivvo = nazivvo;
        }

        public override string ToString()
        {
            string s = "Naziv: " + Nazivvo + "/nId: " + Idvo;
            return s;
        }

    }
}
