using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace QuakeIgrice
{
    [Serializable]
    public class Korisnik
    {
        public string ime { get; set; }
        public string sifra { get; set; }
        public Tip tip { get; set; }

        public Korisnik(string i, string s, Tip t)
        {
            ime = i;
            sifra = s;
            tip = t;
        }
        public Korisnik()
        {
            ime = "";
            sifra = "";
            tip = Tip.K;
        }
    }
}
