using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class OdgovoriGlasovi
    {
        public int GlasID { get; set; }
        public System.DateTime Datum { get; set; }
        public int KorisnikID { get; set; }
        public int OdgovorID { get; set; }
        public bool Pozitivni { get; set; }
        public virtual Korisnici Korisnici { get; set; }
        public virtual Odgovori Odgovori { get; set; }
    }
}
