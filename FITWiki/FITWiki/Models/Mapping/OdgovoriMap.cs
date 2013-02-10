using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class OdgovoriMap : EntityTypeConfiguration<Odgovori>
    {
        public OdgovoriMap()
        {
            // Primary Key
            this.HasKey(t => t.OdgovorID);

            // Properties
            this.Property(t => t.Tekst)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Odgovori");
            this.Property(t => t.OdgovorID).HasColumnName("OdgovorID");
            this.Property(t => t.Tekst).HasColumnName("Tekst");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.PitanjeID).HasColumnName("PitanjeID");
            this.Property(t => t.Pozitivni).HasColumnName("Pozitivni");
            this.Property(t => t.Negativni).HasColumnName("Negativni");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasRequired(t => t.Pitanja)
                .WithMany(t => t.Odgovoris)
                .HasForeignKey(d => d.PitanjeID);

        }
    }
}
