﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class FITKMSEntities : DbContext
    {
        public FITKMSEntities()
            : base("name=FITKMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Aktivnosti> Aktivnosti { get; set; }
        public DbSet<Clanci> Clanci { get; set; }
        public DbSet<ClanciKomentari> ClanciKomentari { get; set; }
        public DbSet<ClanciOcjene> ClanciOcjene { get; set; }
        public DbSet<Korisnici> Korisnici { get; set; }
        public DbSet<KorisniciUloge> KorisniciUloge { get; set; }
        public DbSet<Oblasti> Oblasti { get; set; }
        public DbSet<Odgovori> Odgovori { get; set; }
        public DbSet<OdgovoriGlasovi> OdgovoriGlasovi { get; set; }
        public DbSet<Pitanja> Pitanja { get; set; }
        public DbSet<PitanjaGlasovi> PitanjaGlasovi { get; set; }
        public DbSet<Pretrage> Pretrage { get; set; }
        public DbSet<Tagovi> Tagovi { get; set; }
        public DbSet<Teme> Teme { get; set; }
        public DbSet<Uloge> Uloge { get; set; }
        public DbSet<VrsteClanaka> VrsteClanaka { get; set; }
    
        public virtual ObjectResult<VrsteClanaka> fsp_VrsteClanaka_SelectByStatus(Nullable<bool> status)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VrsteClanaka>("fsp_VrsteClanaka_SelectByStatus", statusParameter);
        }
    
        public virtual ObjectResult<VrsteClanaka> fsp_VrsteClanaka_SelectByStatus(Nullable<bool> status, MergeOption mergeOption)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VrsteClanaka>("fsp_VrsteClanaka_SelectByStatus", mergeOption, statusParameter);
        }
    
        public virtual int fsp_Korisnici_Registration(string ime, string prezime, string mail, string spol, Nullable<System.DateTime> datumRodjenja, string korisnickoIme, string lozinkaHash, string lozinkaSalt)
        {
            var imeParameter = ime != null ?
                new ObjectParameter("Ime", ime) :
                new ObjectParameter("Ime", typeof(string));
    
            var prezimeParameter = prezime != null ?
                new ObjectParameter("Prezime", prezime) :
                new ObjectParameter("Prezime", typeof(string));
    
            var mailParameter = mail != null ?
                new ObjectParameter("Mail", mail) :
                new ObjectParameter("Mail", typeof(string));
    
            var spolParameter = spol != null ?
                new ObjectParameter("Spol", spol) :
                new ObjectParameter("Spol", typeof(string));
    
            var datumRodjenjaParameter = datumRodjenja.HasValue ?
                new ObjectParameter("DatumRodjenja", datumRodjenja) :
                new ObjectParameter("DatumRodjenja", typeof(System.DateTime));
    
            var korisnickoImeParameter = korisnickoIme != null ?
                new ObjectParameter("KorisnickoIme", korisnickoIme) :
                new ObjectParameter("KorisnickoIme", typeof(string));
    
            var lozinkaHashParameter = lozinkaHash != null ?
                new ObjectParameter("LozinkaHash", lozinkaHash) :
                new ObjectParameter("LozinkaHash", typeof(string));
    
            var lozinkaSaltParameter = lozinkaSalt != null ?
                new ObjectParameter("LozinkaSalt", lozinkaSalt) :
                new ObjectParameter("LozinkaSalt", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Korisnici_Registration", imeParameter, prezimeParameter, mailParameter, spolParameter, datumRodjenjaParameter, korisnickoImeParameter, lozinkaHashParameter, lozinkaSaltParameter);
        }
    
        public virtual ObjectResult<Korisnici> fsp_Korisnici_SelectByUsername(string korisnickoIme)
        {
            var korisnickoImeParameter = korisnickoIme != null ?
                new ObjectParameter("KorisnickoIme", korisnickoIme) :
                new ObjectParameter("KorisnickoIme", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Korisnici>("fsp_Korisnici_SelectByUsername", korisnickoImeParameter);
        }
    
        public virtual ObjectResult<Korisnici> fsp_Korisnici_SelectByUsername(string korisnickoIme, MergeOption mergeOption)
        {
            var korisnickoImeParameter = korisnickoIme != null ?
                new ObjectParameter("KorisnickoIme", korisnickoIme) :
                new ObjectParameter("KorisnickoIme", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Korisnici>("fsp_Korisnici_SelectByUsername", mergeOption, korisnickoImeParameter);
        }
    
        public virtual int fsp_Korisnici_Update(Nullable<int> korisnikID, string ime, string prezime, string mail, string spol, Nullable<System.DateTime> datumRodjenja, byte[] slika, string slikaType)
        {
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            var imeParameter = ime != null ?
                new ObjectParameter("Ime", ime) :
                new ObjectParameter("Ime", typeof(string));
    
            var prezimeParameter = prezime != null ?
                new ObjectParameter("Prezime", prezime) :
                new ObjectParameter("Prezime", typeof(string));
    
            var mailParameter = mail != null ?
                new ObjectParameter("Mail", mail) :
                new ObjectParameter("Mail", typeof(string));
    
            var spolParameter = spol != null ?
                new ObjectParameter("Spol", spol) :
                new ObjectParameter("Spol", typeof(string));
    
            var datumRodjenjaParameter = datumRodjenja.HasValue ?
                new ObjectParameter("DatumRodjenja", datumRodjenja) :
                new ObjectParameter("DatumRodjenja", typeof(System.DateTime));
    
            var slikaParameter = slika != null ?
                new ObjectParameter("Slika", slika) :
                new ObjectParameter("Slika", typeof(byte[]));
    
            var slikaTypeParameter = slikaType != null ?
                new ObjectParameter("SlikaType", slikaType) :
                new ObjectParameter("SlikaType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Korisnici_Update", korisnikIDParameter, imeParameter, prezimeParameter, mailParameter, spolParameter, datumRodjenjaParameter, slikaParameter, slikaTypeParameter);
        }
    
        public virtual ObjectResult<Teme> fsp_Teme_SelectByStatus(Nullable<bool> status)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Teme>("fsp_Teme_SelectByStatus", statusParameter);
        }
    
        public virtual ObjectResult<Teme> fsp_Teme_SelectByStatus(Nullable<bool> status, MergeOption mergeOption)
        {
            var statusParameter = status.HasValue ?
                new ObjectParameter("Status", status) :
                new ObjectParameter("Status", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Teme>("fsp_Teme_SelectByStatus", mergeOption, statusParameter);
        }
    
        public virtual ObjectResult<Korisnici> fsp_Korisnici_SelectByID(Nullable<int> korisnikID)
        {
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Korisnici>("fsp_Korisnici_SelectByID", korisnikIDParameter);
        }
    
        public virtual ObjectResult<Korisnici> fsp_Korisnici_SelectByID(Nullable<int> korisnikID, MergeOption mergeOption)
        {
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Korisnici>("fsp_Korisnici_SelectByID", mergeOption, korisnikIDParameter);
        }
    }
}
