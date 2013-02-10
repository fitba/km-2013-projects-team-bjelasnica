using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class AktivnostiMap : EntityTypeConfiguration<Aktivnosti>
    {
        public AktivnostiMap()
        {
            // Primary Key
            this.HasKey(t => t.AktivnostID);

            // Properties
            this.Property(t => t.Naziv)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Aktivnosti");
            this.Property(t => t.AktivnostID).HasColumnName("AktivnostID");
            this.Property(t => t.Naziv).HasColumnName("Naziv");
        }
    }
}
