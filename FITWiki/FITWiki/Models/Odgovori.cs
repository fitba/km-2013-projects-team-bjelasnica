using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Odgovori
    {
        public Odgovori()
        {
            this.OdgovoriGlasovis = new List<OdgovoriGlasovi>();
        }

        public int OdgovorID { get; set; }
        public string Tekst { get; set; }
        public int KorisnikID { get; set; }
        public int PitanjeID { get; set; }
        public Nullable<int> Pozitivni { get; set; }
        public Nullable<int> Negativni { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<System.DateTime> DatumKreiranja { get; set; }
        public Nullable<System.DateTime> DatumIzmjene { get; set; }
        public virtual Pitanja Pitanja { get; set; }
        public virtual ICollection<OdgovoriGlasovi> OdgovoriGlasovis { get; set; }
    }
}
