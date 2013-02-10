using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class KorisniciUloge
    {
        public int KorisnikUlogaID { get; set; }
        public int KorisnikID { get; set; }
        public int UlogaID { get; set; }
        public Nullable<System.DateTime> DatumKreiranja { get; set; }
        public Nullable<System.DateTime> DatumIzmjene { get; set; }
        public virtual Korisnici Korisnici { get; set; }
        public virtual Uloge Uloge { get; set; }
    }
}
