using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Teme
    {
        public Teme()
        {
            this.Clancis = new List<Clanci>();
            this.Pitanjas = new List<Pitanja>();
        }

        public int TemaID { get; set; }
        public string Naziv { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<int> OblastID { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual ICollection<Clanci> Clancis { get; set; }
        public virtual Oblasti Oblasti { get; set; }
        public virtual ICollection<Pitanja> Pitanjas { get; set; }
    }
}
