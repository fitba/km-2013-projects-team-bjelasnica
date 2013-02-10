using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class UlogeMap : EntityTypeConfiguration<Uloge>
    {
        public UlogeMap()
        {
            // Primary Key
            this.HasKey(t => t.UlogaID);

            // Properties
            this.Property(t => t.Naziv)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Uloge");
            this.Property(t => t.UlogaID).HasColumnName("UlogaID");
            this.Property(t => t.Naziv).HasColumnName("Naziv");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");
        }
    }
}
