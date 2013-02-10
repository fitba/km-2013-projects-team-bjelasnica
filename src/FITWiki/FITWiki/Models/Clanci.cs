using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Clanci
    {
        public Clanci()
        {
            this.ClanciKomentaris = new List<ClanciKomentari>();
            this.ClanciOcjenes = new List<ClanciOcjene>();
            this.Pitanjas = new List<Pitanja>();
        }

        public int ClanakID { get; set; }
        public string Naslov { get; set; }
        public string Autori { get; set; }
        public string Sazetak { get; set; }
        public string Tekst { get; set; }
        public string KljucneRijeci { get; set; }
        public int VrstaID { get; set; }
        public int TemaID { get; set; }
        public int KorisnikID { get; set; }
        public string Slike { get; set; }
        public byte[] Dokument { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Izmjene { get; set; }
        public System.DateTime DatumKreiranja { get; set; }
        public System.DateTime DatumIzmjene { get; set; }
        public virtual Korisnici Korisnici { get; set; }
        public virtual Teme Teme { get; set; }
        public virtual VrsteClanaka VrsteClanaka { get; set; }
        public virtual ICollection<ClanciKomentari> ClanciKomentaris { get; set; }
        public virtual ICollection<ClanciOcjene> ClanciOcjenes { get; set; }
        public virtual ICollection<Pitanja> Pitanjas { get; set; }
    }
}
