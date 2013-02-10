using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class ClanciOcjene
    {
        public int ClanakOcjenaID { get; set; }
        public int Ocjena { get; set; }
        public int ClanakID { get; set; }
        public int KorisnikID { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual Clanci Clanci { get; set; }
        public virtual Korisnici Korisnici { get; set; }
    }
}
