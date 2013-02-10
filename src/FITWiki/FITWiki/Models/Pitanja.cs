using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Pitanja
    {
        public Pitanja()
        {
            this.Odgovoris = new List<Odgovori>();
            this.PitanjaGlasovis = new List<PitanjaGlasovi>();
        }

        public int PitanjeID { get; set; }
        public string Tekst { get; set; }
        public int KorisnikID { get; set; }
        public Nullable<int> ClanakID { get; set; }
        public Nullable<int> TemaID { get; set; }
        public Nullable<int> Pozitivni { get; set; }
        public Nullable<int> Negativni { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual Clanci Clanci { get; set; }
        public virtual Korisnici Korisnici { get; set; }
        public virtual ICollection<Odgovori> Odgovoris { get; set; }
        public virtual Teme Teme { get; set; }
        public virtual ICollection<PitanjaGlasovi> PitanjaGlasovis { get; set; }
    }
}
