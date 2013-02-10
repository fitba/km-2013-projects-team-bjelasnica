using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class ClanciMap : EntityTypeConfiguration<Clanci>
    {
        public ClanciMap()
        {
            // Primary Key
            this.HasKey(t => t.ClanakID);

            // Properties
            this.Property(t => t.Naslov)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Autori)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Sazetak)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Tekst)
                .IsRequired();

            this.Property(t => t.KljucneRijeci)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Slike)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Clanci");
            this.Property(t => t.ClanakID).HasColumnName("ClanakID");
            this.Property(t => t.Naslov).HasColumnName("Naslov");
            this.Property(t => t.Autori).HasColumnName("Autori");
            this.Property(t => t.Sazetak).HasColumnName("Sazetak");
            this.Property(t => t.Tekst).HasColumnName("Tekst");
            this.Property(t => t.KljucneRijeci).HasColumnName("KljucneRijeci");
            this.Property(t => t.VrstaID).HasColumnName("VrstaID");
            this.Property(t => t.TemaID).HasColumnName("TemaID");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.Slike).HasColumnName("Slike");
            this.Property(t => t.Dokument).HasColumnName("Dokument");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Izmjene).HasColumnName("Izmjene");
            this.Property(t => t.DatumKreiranja).HasColumnName("DatumKreiranja");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

            // Relationships
            this.HasRequired(t => t.Korisnici)
                .WithMany(t => t.Clancis)
                .HasForeignKey(d => d.KorisnikID);
            this.HasRequired(t => t.Teme)
                .WithMany(t => t.Clancis)
                .HasForeignKey(d => d.TemaID);
            this.HasRequired(t => t.VrsteClanaka)
                .WithMany(t => t.Clancis)
                .HasForeignKey(d => d.VrstaID);

        }
    }
}
