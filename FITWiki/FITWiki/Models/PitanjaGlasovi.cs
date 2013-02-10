using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class PitanjaGlasovi
    {
        public int GlasID { get; set; }
        public System.DateTime Datum { get; set; }
        public int KorisnikID { get; set; }
        public int PitanjeID { get; set; }
        public bool Pozitivni { get; set; }
        public virtual Korisnici Korisnici { get; set; }
        public virtual Pitanja Pitanja { get; set; }
    }
}
