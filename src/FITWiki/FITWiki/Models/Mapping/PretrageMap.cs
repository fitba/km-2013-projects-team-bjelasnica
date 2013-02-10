using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class PretrageMap : EntityTypeConfiguration<Pretrage>
    {
        public PretrageMap()
        {
            // Primary Key
            this.HasKey(t => t.PretragaID);

            // Properties
            this.Property(t => t.TekstPretrage)
                .HasMaxLength(500);

            // Table & Column Mappings
            this.ToTable("Pretrage");
            this.Property(t => t.PretragaID).HasColumnName("PretragaID");
            this.Property(t => t.Datum).HasColumnName("Datum");
            this.Property(t => t.AktivnostID).HasColumnName("AktivnostID");
            this.Property(t => t.TekstPretrage).HasColumnName("TekstPretrage");
            this.Property(t => t.ClanakID).HasColumnName("ClanakID");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
        }
    }
}
