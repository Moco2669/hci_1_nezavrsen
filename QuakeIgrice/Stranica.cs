using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuakeIgrice
{
    public class Stranica
    {
        public int godinaIzdavanja { get; set; }
        public string naslov { get; set; }
        public string slikaPutanja { get; set; }
        public string rtfPutanja { get; set; }
        public DateTime datumDodavanja { get; set; }
        public bool cekiran { get; set; }

        public Stranica()
        {
            godinaIzdavanja = 0;
            naslov = "";
            slikaPutanja = "";
            rtfPutanja = "";
            datumDodavanja = DateTime.MinValue;
            cekiran = false;
        }

        public Stranica(int gi, string nas, string slikput, string rtfput, DateTime datumdod)
        {
            godinaIzdavanja = gi;
            naslov = nas;
            slikaPutanja = slikput;
            rtfPutanja = rtfput;
            datumDodavanja = datumdod;
            cekiran = false;
        }
    }
}
