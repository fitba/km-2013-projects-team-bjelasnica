using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class VrsteClanakaMap : EntityTypeConfiguration<VrsteClanaka>
    {
        public VrsteClanakaMap()
        {
            // Primary Key
            this.HasKey(t => t.VrstaID);

            // Properties
            this.Property(t => t.Naziv)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("VrsteClanaka");
            this.Property(t => t.VrstaID).HasColumnName("VrstaID");
            this.Property(t => t.Naziv).HasColumnName("Naziv");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");
        }
    }
}
