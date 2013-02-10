using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class ClanciKomentariMap : EntityTypeConfiguration<ClanciKomentari>
    {
        public ClanciKomentariMap()
        {
            // Primary Key
            this.HasKey(t => t.ClanakKomentarID);

            // Properties
            this.Property(t => t.Komentar)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("ClanciKomentari");
            this.Property(t => t.ClanakKomentarID).HasColumnName("ClanakKomentarID");
            this.Property(t => t.Komentar).HasColumnName("Komentar");
            this.Property(t => t.ClanakID).HasColumnName("ClanakID");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.Pozitivni).HasColumnName("Pozitivni");
            this.Property(t => t.Negativni).HasColumnName("Negativni");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasRequired(t => t.Clanci)
                .WithMany(t => t.ClanciKomentaris)
                .HasForeignKey(d => d.ClanakID);
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.ClanciKomentaris)
                .HasForeignKey(d => d.KorisnikID);

        }
    }
}
