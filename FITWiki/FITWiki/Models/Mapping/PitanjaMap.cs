using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class PitanjaMap : EntityTypeConfiguration<Pitanja>
    {
        public PitanjaMap()
        {
            // Primary Key
            this.HasKey(t => t.PitanjeID);

            // Properties
            this.Property(t => t.Tekst)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Pitanja");
            this.Property(t => t.PitanjeID).HasColumnName("PitanjeID");
            this.Property(t => t.Tekst).HasColumnName("Tekst");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.ClanakID).HasColumnName("ClanakID");
            this.Property(t => t.TemaID).HasColumnName("TemaID");
            this.Property(t => t.Pozitivni).HasColumnName("Pozitivni");
            this.Property(t => t.Negativni).HasColumnName("Negativni");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasOptional(t => t.Clanci)
                .WithMany(t => t.Pitanjas)
                .HasForeignKey(d => d.ClanakID);
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.Pitanjas)
                .HasForeignKey(d => d.KorisnikID);
            this.HasOptional(t => t.Teme)
                .WithMany(t => t.Pitanjas)
                .HasForeignKey(d => d.TemaID);

        }
    }
}
