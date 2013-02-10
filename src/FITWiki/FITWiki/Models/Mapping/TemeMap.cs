using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class TemeMap : EntityTypeConfiguration<Teme>
    {
        public TemeMap()
        {
            // Primary Key
            this.HasKey(t => t.TemaID);

            // Properties
            this.Property(t => t.Naziv)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Teme");
            this.Property(t => t.TemaID).HasColumnName("TemaID");
            this.Property(t => t.Naziv).HasColumnName("Naziv");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.OblastID).HasColumnName("OblastID");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasOptional(t => t.Oblasti)
                .WithMany(t => t.Temes)
                .HasForeignKey(d => d.OblastID);

        }
    }
}
