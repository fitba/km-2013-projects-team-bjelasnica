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
    
    public partial class fsp_Pitanja_SelectAll_Result
    {
        public int PitanjeID { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public string Naslov { get; set; }
        public string Tekst { get; set; }
        public Nullable<int> BrojPregleda { get; set; }
        public Nullable<int> Pozitivni { get; set; }
        public Nullable<int> Negativni { get; set; }
        public string KorisnickoIme { get; set; }
        public Nullable<int> BrojOdgvora { get; set; }
    }
}