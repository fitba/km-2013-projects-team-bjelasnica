using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class ClanciOcjeneMap : EntityTypeConfiguration<ClanciOcjene>
    {
        public ClanciOcjeneMap()
        {
            // Primary Key
            this.HasKey(t => t.ClanakOcjenaID);

            // Properties
            // Table & Column Mappings
            this.ToTable("ClanciOcjene");
            this.Property(t => t.ClanakOcjenaID).HasColumnName("ClanakOcjenaID");
            this.Property(t => t.Ocjena).HasColumnName("Ocjena");
            this.Property(t => t.ClanakID).HasColumnName("ClanakID");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasRequired(t => t.Clanci)
                .WithMany(t => t.ClanciOcjenes)
                .HasForeignKey(d => d.ClanakID);
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.ClanciOcjenes)
                .HasForeignKey(d => d.KorisnikID);

        }
    }
}
