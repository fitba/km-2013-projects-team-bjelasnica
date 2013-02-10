using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class OblastiMap : EntityTypeConfiguration<Oblasti>
    {
        public OblastiMap()
        {
            // Primary Key
            this.HasKey(t => t.OblastID);

            // Properties
            this.Property(t => t.Naziv)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Oblasti");
            this.Property(t => t.OblastID).HasColumnName("OblastID");
            this.Property(t => t.Naziv).HasColumnName("Naziv");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");
        }
    }
}
