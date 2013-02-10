using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Oblasti
    {
        public Oblasti()
        {
            this.Temes = new List<Teme>();
        }

        public int OblastID { get; set; }
        public string Naziv { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual ICollection<Teme> Temes { get; set; }
    }
}
