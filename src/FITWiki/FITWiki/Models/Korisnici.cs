using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FITWiki.Models
{
    public partial class Korisnici
    {
        public Korisnici()
        {
            this.Clancis = new List<Clanci>();
            this.ClanciKomentaris = new List<ClanciKomentari>();
            this.ClanciOcjenes = new List<ClanciOcjene>();
            this.KorisniciUloges = new List<KorisniciUloge>();
            this.OdgovoriGlasovis = new List<OdgovoriGlasovi>();
            this.Pitanjas = new List<Pitanja>();
            this.PitanjaGlasovis = new List<PitanjaGlasovi>();
            DatumRegistracije = DateTime.Now;
            PosljednjaPrijava = DateTime.Now;
            DatumIzmjene = DateTime.Now;
            Aktivan = true;
        }

        public int KorisnikID { get; set; }
        [Required(ErrorMessage = "Unesite ime.")]

        public string Ime { get; set; }

        [Required(ErrorMessage = "Unesite prezime.")]
        public string Prezime { get; set; }

        [Required(ErrorMessage = "Unesite e-mail adresu.")]
        [RegularExpression(@"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@(([0-9a-zA-Z])+([-\w]*[0-9a-zA-Z])*\.)+[a-zA-Z]{2,9})$", ErrorMessage="Neispravna mail adresa.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Odaberite spol.")]
        public string Spol { get; set; }

        [Required(ErrorMessage = "Unesite datum roðenja.")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [DisplayName("Datum roðenja")]
        public System.DateTime DatumRodjenja { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [DisplayName("Registracija")]
        public System.DateTime DatumRegistracije { get; set; }

        [DisplayName("Posljednja prijava")]
        public System.DateTime PosljednjaPrijava { get; set; }

        [Required(ErrorMessage = "Unesite korisnièko ime.")]
        [DisplayName("Korisnièko ime")]
        public string KorisnickoIme { get; set; }

        public string LozinkaHash { get; set; }

        public string LozinkaSalt { get; set; }

        [DisplayName("Aktivan/na")]
        public bool Aktivan { get; set; }

        public Nullable<System.DateTime> DatumIzmjene { get; set; }
        public virtual ICollection<Clanci> Clancis { get; set; }
        public virtual ICollection<ClanciKomentari> ClanciKomentaris { get; set; }
        public virtual ICollection<ClanciOcjene> ClanciOcjenes { get; set; }
        public virtual ICollection<KorisniciUloge> KorisniciUloges { get; set; }
        public virtual ICollection<OdgovoriGlasovi> OdgovoriGlasovis { get; set; }
        public virtual ICollection<Pitanja> Pitanjas { get; set; }
        public virtual ICollection<PitanjaGlasovi> PitanjaGlasovis { get; set; }

        #region NotMapped properties
        //Dodatni atributi za unos clear text lozinke na formi
        [NotMapped]
        [Required(ErrorMessage = "Unesite lozinku.")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Polje {0} treba imati najmanje {2} karaktera.", MinimumLength = 6)]
        public string Lozinka { get; set; }

        [NotMapped]
        [DisplayName("Potvrda lozinke")]
        [Required(ErrorMessage = "Unesite potvrdu lozinke.")]
        [DataType(DataType.Password)]
        [Compare("Lozinka", ErrorMessage = "Unijeli ste pogrešnu lozinku.")]
        public string PotvrdaLozinke { get; set; }
        #endregion
    }
}
