using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class VrsteClanaka
    {
        public VrsteClanaka()
        {
            this.Clancis = new List<Clanci>();
        }

        public int VrstaID { get; set; }
        public string Naziv { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual ICollection<Clanci> Clancis { get; set; }
    }
}
