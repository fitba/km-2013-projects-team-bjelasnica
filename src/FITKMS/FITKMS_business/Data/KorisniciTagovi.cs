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
    
    public partial class KorisniciTagovi
    {
        public int KorisnikID { get; set; }
        public int TagID { get; set; }
        public Nullable<System.DateTime> Datum { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual Korisnici Korisnici { get; set; }
        public virtual Tagovi Tagovi { get; set; }
    }
}