using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FITWiki.Models.Mapping
{
    public class KorisniciMap : EntityTypeConfiguration<Korisnici>
    {
        public KorisniciMap()
        {
            // Primary Key
            this.HasKey(t => t.KorisnikID);

            // Properties
            this.Property(t => t.Ime)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Prezime)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Mail)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.Spol)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.KorisnickoIme)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LozinkaHash)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.LozinkaSalt)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Korisnici");
            this.Property(t => t.KorisnikID).HasColumnName("KorisnikID");
            this.Property(t => t.Ime).HasColumnName("Ime");
            this.Property(t => t.Prezime).HasColumnName("Prezime");
            this.Property(t => t.Mail).HasColumnName("Mail");
            this.Property(t => t.Spol).HasColumnName("Spol");
            this.Property(t => t.DatumRodjenja).HasColumnName("DatumRodjenja");
            this.Property(t => t.DatumRegistracije).HasColumnName("DatumRegistracije");
            this.Property(t => t.PosljednjaPrijava).HasColumnName("PosljednjaPrijava");
            this.Property(t => t.KorisnickoIme).HasColumnName("KorisnickoIme");
            this.Property(t => t.LozinkaHash).HasColumnName("LozinkaHash");
            this.Property(t => t.LozinkaSalt).HasColumnName("LozinkaSalt");
            this.Property(t => t.Aktivan).HasColumnName("Aktivan");
            this.Property(t => t.DatumIzmjene).HasColumnName("DatumIzmjene");

        }
    }
}
