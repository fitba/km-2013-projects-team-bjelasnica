using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class KorisniciUlogeMap : EntityTypeConfiguration<KorisniciUloge>
    {
        public KorisniciUlogeMap()
        {
            // Primary Key
            this.HasKey(t => new { t.KorisnikUlogaID, t.KorisnikID, t.UlogaID });

            // Properties
            this.Property(t => t.KorisnikUlogaID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.KorisnikID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.UlogaID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("KorisniciUloge");
            this.Property(t => t.KorisnikUlogaID).HasColumnName("KorisnikUlogaID");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.UlogaID).HasColumnName("UlogaID");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.KorisniciUloges)
                .HasForeignKey(d => d.KorisnikID);
            this.HasRequired(t => t.Uloge)
                .WithMany(t => t.KorisniciUloges)
                .HasForeignKey(d => d.UlogaID);

        }
    }
}
