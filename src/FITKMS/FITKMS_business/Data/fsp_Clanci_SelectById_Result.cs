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
    
    public partial class fsp_Clanci_SelectById_Result
    {
        public int ClanakID { get; set; }
        public string Naslov { get; set; }
        public string Autori { get; set; }
        public string Tekst { get; set; }
        public string KljucneRijeci { get; set; }
        public int VrstaID { get; set; }
        public int TemaID { get; set; }
        public int KorisnikID { get; set; }
        public byte[] Dokument { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Izmjene { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public string DokumentType { get; set; }
        public string Vrsta { get; set; }
        public string Tema { get; set; }
        public Nullable<double> ProsjecnaOcjena { get; set; }
    }
}
