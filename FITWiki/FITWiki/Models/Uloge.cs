using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Uloge
    {
        public Uloge()
        {
            this.KorisniciUloges = new List<KorisniciUloge>();
        }

        public int UlogaID { get; set; }
        public string Naziv { get; set; }
        public Nullable<System.DateTime> DatumKreiranja { get; set; }
        public Nullable<System.DateTime> DatumIzmjene { get; set; }
        public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; }
    }
}
