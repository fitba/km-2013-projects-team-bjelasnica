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
    
    public partial class ClanciKomentari
    {
        public int ClanakKomentarID { get; set; }
        public string Komentar { get; set; }
        public int ClanakID { get; set; }
        public int KorisnikID { get; set; }
        public Nullable<bool> Status { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
    
        public virtual Clanci Clanci { get; set; }
        public virtual Korisnici Korisnici { get; set; }
    }
}
