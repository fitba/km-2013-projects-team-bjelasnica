//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FITKMS_business.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tagovi
    {
        public Tagovi()
        {
            this.Clanci = new HashSet<Clanci>();
            this.Pitanja = new HashSet<Pitanja>();
            this.KorisniciTagovi = new HashSet<KorisniciTagovi>();
        }
    
        public int TagID { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual ICollection<Clanci> Clanci { get; set; }
        public virtual ICollection<Pitanja> Pitanja { get; set; }
        public virtual ICollection<KorisniciTagovi> KorisniciTagovi { get; set; }
    }
}
