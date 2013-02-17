using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FITWiki.Models
{
    public class PitanjaKorisnici
    {
        public string ime { get; set; }
        public string prezime { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Tekst { get; set; }
        public int Negativni { get; set; }
        public int Pozitivni { get; set; }
    }
}//p.DatumKreiranja,p.Tekst,p.Negativni,p.Pozitivni, stavi koji su tipovi u bazi
