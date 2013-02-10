using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FITWiki.Models.Mapping;

namespace FITWiki.Models
{
    public partial class FITWikiContext : DbContext
    {
        static FITWikiContext()
        {
            Database.SetInitializer<FITWikiContext>(null);
        }

        public FITWikiContext()
            : base("Name=FITWikiContext")
        {
        }

        public DbSet<Aktivnosti> Aktivnostis { get; set; }
        public DbSet<Clanci> Clancis { get; set; }
        public DbSet<ClanciKomentari> ClanciKomentaris { get; set; }
        public DbSet<ClanciOcjene> ClanciOcjenes { get; set; }
        public DbSet<Korisnici> Korisnicis { get; set; }
        public DbSet<KorisniciUloge> KorisniciUloges { get; set; }
        public DbSet<Oblasti> Oblastis { get; set; }
        public DbSet<Odgovori> Odgovoris { get; set; }
        public DbSet<OdgovoriGlasovi> OdgovoriGlasovis { get; set; }
        public DbSet<Pitanja> Pitanjas { get; set; }
        public DbSet<PitanjaGlasovi> PitanjaGlasovis { get; set; }
        public DbSet<Pretrage> Pretrages { get; set; }
        public DbSet<Teme> Temes { get; set; }
        public DbSet<Uloge> Uloges { get; set; }
        public DbSet<VrsteClanaka> VrsteClanakas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AktivnostiMap());
            modelBuilder.Configurations.Add(new ClanciMap());
            modelBuilder.Configurations.Add(new ClanciKomentariMap());
            modelBuilder.Configurations.Add(new ClanciOcjeneMap());
            modelBuilder.Configurations.Add(new KorisniciMap());
            modelBuilder.Configurations.Add(new KorisniciUlogeMap());
            modelBuilder.Configurations.Add(new OblastiMap());
            modelBuilder.Configurations.Add(new OdgovoriMap());
            modelBuilder.Configurations.Add(new OdgovoriGlasoviMap());
            modelBuilder.Configurations.Add(new PitanjaMap());
            modelBuilder.Configurations.Add(new PitanjaGlasoviMap());
            modelBuilder.Configurations.Add(new PretrageMap());
            modelBuilder.Configurations.Add(new TemeMap());
            modelBuilder.Configurations.Add(new UlogeMap());
            modelBuilder.Configurations.Add(new VrsteClanakaMap());
        }
    }
}
