using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class PitanjaGlasoviMap : EntityTypeConfiguration<PitanjaGlasovi>
    {
        public PitanjaGlasoviMap()
        {
            // Primary Key
            this.HasKey(t => t.GlasID);

            // Properties
            // Table & Column Mappings
            this.ToTable("PitanjaGlasovi");
            this.Property(t => t.GlasID).HasColumnName("GlasID");
            this.Property(t => t.Datum).HasColumnName("Datum");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.PitanjeID).HasColumnName("PitanjeID");
            this.Property(t => t.Pozitivni).HasColumnName("Pozitivni");

            // Relationships
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.PitanjaGlasovis)
                .HasForeignKey(d => d.KorisnikID);
            this.HasRequired(t => t.Pitanja)
                .WithMany(t => t.PitanjaGlasovis)
                .HasForeignKey(d => d.PitanjeID);

        }
    }
}
