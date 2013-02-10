using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class OdgovoriGlasoviMap : EntityTypeConfiguration<OdgovoriGlasovi>
    {
        public OdgovoriGlasoviMap()
        {
            // Primary Key
            this.HasKey(t => t.GlasID);

            // Properties
            // Table & Column Mappings
            this.ToTable("OdgovoriGlasovi");
            this.Property(t => t.GlasID).HasColumnName("GlasID");
            this.Property(t => t.Datum).HasColumnName("Datum");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.OdgovorID).HasColumnName("OdgovorID");
            this.Property(t => t.Pozitivni).HasColumnName("Pozitivni");

            // Relationships
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.OdgovoriGlasovis)
                .HasForeignKey(d => d.KorisnikID);
            this.HasRequired(t => t.Odgovori)
                .WithMany(t => t.OdgovoriGlasovis)
                .HasForeignKey(d => d.OdgovorID);

        }
    }
}
