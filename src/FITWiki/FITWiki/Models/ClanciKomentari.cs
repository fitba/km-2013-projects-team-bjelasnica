using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class ClanciKomentari
    {
        public int ClanakKomentarID { get; set; }
        public string Komentar { get; set; }
        public int ClanakID { get; set; }
        public int KorisnikID { get; set; }
        public Nullable<int> Pozitivni { get; set; }
        public Nullable<int> Negativni { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual Clanci Clanci { get; set; }
        public virtual Korisnici Korisnici { get; set; }
    }
}
