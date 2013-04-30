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
    
        public virtual int fsp_ClanciTagovi_Insert(Nullable<int> clanakID, Nullable<int> tagID)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            var tagIDParameter = tagID.HasValue ?
                new ObjectParameter("TagID", tagID) :
                new ObjectParameter("TagID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_ClanciTagovi_Insert", clanakIDParameter, tagIDParameter);
        }
    
        public virtual ObjectResult<fsp_Clanci_SelectByTypeTitle_Result> fsp_Clanci_SelectByTypeTitle(Nullable<int> vrstaID, string pretraga, Nullable<int> offset, Nullable<int> maxRows, ObjectParameter totalRows)
        {
            var vrstaIDParameter = vrstaID.HasValue ?
                new ObjectParameter("VrstaID", vrstaID) :
                new ObjectParameter("VrstaID", typeof(int));
    
            var pretragaParameter = pretraga != null ?
                new ObjectParameter("Pretraga", pretraga) :
                new ObjectParameter("Pretraga", typeof(string));
    
            var offsetParameter = offset.HasValue ?
                new ObjectParameter("Offset", offset) :
                new ObjectParameter("Offset", typeof(int));
    
            var maxRowsParameter = maxRows.HasValue ?
                new ObjectParameter("MaxRows", maxRows) :
                new ObjectParameter("MaxRows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_Clanci_SelectByTypeTitle_Result>("fsp_Clanci_SelectByTypeTitle", vrstaIDParameter, pretragaParameter, offsetParameter, maxRowsParameter, totalRows);
        }
    
        public virtual ObjectResult<fsp_Clanci_SelectById_Result> fsp_Clanci_SelectById(Nullable<int> clanakID)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_Clanci_SelectById_Result>("fsp_Clanci_SelectById", clanakIDParameter);
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
    
        public virtual ObjectResult<Tagovi> fsp_Clanci_SelectTags(Nullable<int> clanakID)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Clanci_SelectTags", clanakIDParameter);
        }
    
        public virtual ObjectResult<Tagovi> fsp_Clanci_SelectTags(Nullable<int> clanakID, MergeOption mergeOption)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Clanci_SelectTags", mergeOption, clanakIDParameter);
        }
    
        public virtual int fsp_Clanci_DeleteTags(Nullable<int> clanakID)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Clanci_DeleteTags", clanakIDParameter);
        }
    
        public virtual ObjectResult<Tagovi> fsp_get_AllTagovi()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_get_AllTagovi");
        }
    
        public virtual ObjectResult<Tagovi> fsp_get_AllTagovi(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_get_AllTagovi", mergeOption);
        }
    
        public virtual int fsp_Tagovi_SelectAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Tagovi_SelectAll");
        }
    
        public virtual int fsp_Korisnici_ChangePassword(Nullable<int> korisnikID, string lozinkaSalt, string lozinkaHash)
        {
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            var lozinkaSaltParameter = lozinkaSalt != null ?
                new ObjectParameter("LozinkaSalt", lozinkaSalt) :
                new ObjectParameter("LozinkaSalt", typeof(string));
    
            var lozinkaHashParameter = lozinkaHash != null ?
                new ObjectParameter("LozinkaHash", lozinkaHash) :
                new ObjectParameter("LozinkaHash", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Korisnici_ChangePassword", korisnikIDParameter, lozinkaSaltParameter, lozinkaHashParameter);
        }
    
        public virtual ObjectResult<fsp_getAllTagoviZaPitanjeID_Result> fsp_getAllTagoviZaPitanjeID(Nullable<int> pid)
        {
            var pidParameter = pid.HasValue ?
                new ObjectParameter("Pid", pid) :
                new ObjectParameter("Pid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_getAllTagoviZaPitanjeID_Result>("fsp_getAllTagoviZaPitanjeID", pidParameter);
        }
    
        public virtual ObjectResult<fsp_getAllOdgovoriZaPitanje_Result> fsp_getAllOdgovoriZaPitanje(Nullable<int> pId)
        {
            var pIdParameter = pId.HasValue ?
                new ObjectParameter("PId", pId) :
                new ObjectParameter("PId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_getAllOdgovoriZaPitanje_Result>("fsp_getAllOdgovoriZaPitanje", pIdParameter);
        }
    
        public virtual int fsp_Clanci_Update(Nullable<int> clanakID, string naslov, string autori, string tekst, string kljucneRijeci, Nullable<int> vrstaID, Nullable<int> temaID, byte[] dokument, string dokumentType, string dokumentPath)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            var naslovParameter = naslov != null ?
                new ObjectParameter("Naslov", naslov) :
                new ObjectParameter("Naslov", typeof(string));
    
            var autoriParameter = autori != null ?
                new ObjectParameter("Autori", autori) :
                new ObjectParameter("Autori", typeof(string));
    
            var tekstParameter = tekst != null ?
                new ObjectParameter("Tekst", tekst) :
                new ObjectParameter("Tekst", typeof(string));
    
            var kljucneRijeciParameter = kljucneRijeci != null ?
                new ObjectParameter("KljucneRijeci", kljucneRijeci) :
                new ObjectParameter("KljucneRijeci", typeof(string));
    
            var vrstaIDParameter = vrstaID.HasValue ?
                new ObjectParameter("VrstaID", vrstaID) :
                new ObjectParameter("VrstaID", typeof(int));
    
            var temaIDParameter = temaID.HasValue ?
                new ObjectParameter("TemaID", temaID) :
                new ObjectParameter("TemaID", typeof(int));
    
            var dokumentParameter = dokument != null ?
                new ObjectParameter("Dokument", dokument) :
                new ObjectParameter("Dokument", typeof(byte[]));
    
            var dokumentTypeParameter = dokumentType != null ?
                new ObjectParameter("DokumentType", dokumentType) :
                new ObjectParameter("DokumentType", typeof(string));
    
            var dokumentPathParameter = dokumentPath != null ?
                new ObjectParameter("DokumentPath", dokumentPath) :
                new ObjectParameter("DokumentPath", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("fsp_Clanci_Update", clanakIDParameter, naslovParameter, autoriParameter, tekstParameter, kljucneRijeciParameter, vrstaIDParameter, temaIDParameter, dokumentParameter, dokumentTypeParameter, dokumentPathParameter);
        }
    
        public virtual ObjectResult<fsp_Clanci_SelectByTag_Result> fsp_Clanci_SelectByTag(Nullable<int> tagID, Nullable<int> offset, Nullable<int> maxRows, ObjectParameter totalRows)
        {
            var tagIDParameter = tagID.HasValue ?
                new ObjectParameter("TagID", tagID) :
                new ObjectParameter("TagID", typeof(int));
    
            var offsetParameter = offset.HasValue ?
                new ObjectParameter("Offset", offset) :
                new ObjectParameter("Offset", typeof(int));
    
            var maxRowsParameter = maxRows.HasValue ?
                new ObjectParameter("MaxRows", maxRows) :
                new ObjectParameter("MaxRows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_Clanci_SelectByTag_Result>("fsp_Clanci_SelectByTag", tagIDParameter, offsetParameter, maxRowsParameter, totalRows);
        }
    
        public virtual ObjectResult<Tagovi> fsp_Tagovi_SelectPagination(Nullable<int> pageNo, Nullable<int> recordsPerPage, ObjectParameter totalRows, string search)
        {
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var recordsPerPageParameter = recordsPerPage.HasValue ?
                new ObjectParameter("RecordsPerPage", recordsPerPage) :
                new ObjectParameter("RecordsPerPage", typeof(int));
    
            var searchParameter = search != null ?
                new ObjectParameter("Search", search) :
                new ObjectParameter("Search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Tagovi_SelectPagination", pageNoParameter, recordsPerPageParameter, totalRows, searchParameter);
        }
    
        public virtual ObjectResult<Tagovi> fsp_Tagovi_SelectPagination(Nullable<int> pageNo, Nullable<int> recordsPerPage, ObjectParameter totalRows, string search, MergeOption mergeOption)
        {
            var pageNoParameter = pageNo.HasValue ?
                new ObjectParameter("PageNo", pageNo) :
                new ObjectParameter("PageNo", typeof(int));
    
            var recordsPerPageParameter = recordsPerPage.HasValue ?
                new ObjectParameter("RecordsPerPage", recordsPerPage) :
                new ObjectParameter("RecordsPerPage", typeof(int));
    
            var searchParameter = search != null ?
                new ObjectParameter("Search", search) :
                new ObjectParameter("Search", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Tagovi_SelectPagination", mergeOption, pageNoParameter, recordsPerPageParameter, totalRows, searchParameter);
        }
    
        public virtual ObjectResult<fsp_Pitanja_SelectByTag_Result> fsp_Pitanja_SelectByTag(Nullable<int> tagID, Nullable<int> offset, Nullable<int> maxRows, ObjectParameter totalRows)
        {
            var tagIDParameter = tagID.HasValue ?
                new ObjectParameter("TagID", tagID) :
                new ObjectParameter("TagID", typeof(int));
    
            var offsetParameter = offset.HasValue ?
                new ObjectParameter("Offset", offset) :
                new ObjectParameter("Offset", typeof(int));
    
            var maxRowsParameter = maxRows.HasValue ?
                new ObjectParameter("MaxRows", maxRows) :
                new ObjectParameter("MaxRows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_Pitanja_SelectByTag_Result>("fsp_Pitanja_SelectByTag", tagIDParameter, offsetParameter, maxRowsParameter, totalRows);
        }
    
        public virtual ObjectResult<Tagovi> fsp_Pitanja_SelectTags(Nullable<int> pitanjeID)
        {
            var pitanjeIDParameter = pitanjeID.HasValue ?
                new ObjectParameter("PitanjeID", pitanjeID) :
                new ObjectParameter("PitanjeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Pitanja_SelectTags", pitanjeIDParameter);
        }
    
        public virtual ObjectResult<Tagovi> fsp_Pitanja_SelectTags(Nullable<int> pitanjeID, MergeOption mergeOption)
        {
            var pitanjeIDParameter = pitanjeID.HasValue ?
                new ObjectParameter("PitanjeID", pitanjeID) :
                new ObjectParameter("PitanjeID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Tagovi>("fsp_Pitanja_SelectTags", mergeOption, pitanjeIDParameter);
        }
    
        public virtual ObjectResult<fsp_Pitanja_SelectAll_Result> fsp_Pitanja_SelectAll(Nullable<int> offset, Nullable<int> maxRows, ObjectParameter totalRows)
        {
            var offsetParameter = offset.HasValue ?
                new ObjectParameter("Offset", offset) :
                new ObjectParameter("Offset", typeof(int));
    
            var maxRowsParameter = maxRows.HasValue ?
                new ObjectParameter("MaxRows", maxRows) :
                new ObjectParameter("MaxRows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_Pitanja_SelectAll_Result>("fsp_Pitanja_SelectAll", offsetParameter, maxRowsParameter, totalRows);
        }
    
        public virtual ObjectResult<ClanciOcjene> fsp_ClanciOcjene_SelectByUser(Nullable<int> clanakID, Nullable<int> korisnikID)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClanciOcjene>("fsp_ClanciOcjene_SelectByUser", clanakIDParameter, korisnikIDParameter);
        }
    
        public virtual ObjectResult<ClanciOcjene> fsp_ClanciOcjene_SelectByUser(Nullable<int> clanakID, Nullable<int> korisnikID, MergeOption mergeOption)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            var korisnikIDParameter = korisnikID.HasValue ?
                new ObjectParameter("KorisnikID", korisnikID) :
                new ObjectParameter("KorisnikID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClanciOcjene>("fsp_ClanciOcjene_SelectByUser", mergeOption, clanakIDParameter, korisnikIDParameter);
        }
    
        public virtual ObjectResult<fsp_ClanciKomentari_Select_Result> fsp_ClanciKomentari_Select(Nullable<int> clanakID, Nullable<int> offset, Nullable<int> maxRows, ObjectParameter totalRows)
        {
            var clanakIDParameter = clanakID.HasValue ?
                new ObjectParameter("ClanakID", clanakID) :
                new ObjectParameter("ClanakID", typeof(int));
    
            var offsetParameter = offset.HasValue ?
                new ObjectParameter("Offset", offset) :
                new ObjectParameter("Offset", typeof(int));
    
            var maxRowsParameter = maxRows.HasValue ?
                new ObjectParameter("MaxRows", maxRows) :
                new ObjectParameter("MaxRows", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<fsp_ClanciKomentari_Select_Result>("fsp_ClanciKomentari_Select", clanakIDParameter, offsetParameter, maxRowsParameter, totalRows);
        }
    }
}
