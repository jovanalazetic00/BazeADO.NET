using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jovana.Model
{
    public class Objekat
    {
        public Objekat(int ido, string idl, int idvo, int povrsina, string adresa, int vrednost)
        {
            Ido = ido;
            Idl = idl;
            Idvo = idvo;
            Povrsina = povrsina;
            Adresa = adresa;
            Vrednost = vrednost;
        }

        public Objekat()
        {

        }

        public int Ido { get; set; }
        public string Idl { get; set; }
        public int Idvo { get; set; }
        public int Povrsina { get; set; }
        public string Adresa { get; set; }
        public int Vrednost { get; set; }


        public override string ToString()
        {
            return "Ido - " + Ido + ", Idl" + Idl + ", Idvo - " + Idvo + ", Povrsina" + Povrsina + ", Adresa - " + Adresa + ", Vrednost" + Vrednost;
        }
    }
}
