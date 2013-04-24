using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAKorisnici
    {
        public static void Insert(Korisnici user)
        {
            Connection.dm.fsp_Korisnici_Registration(user.Ime, user.Prezime, user.Mail, user.Spol, user.DatumRodjenja, user.KorisnickoIme, user.LozinkaHash, user.LozinkaSalt);
            Connection.dm.SaveChanges();
        }

        public static Korisnici GetByUsername(string username)
        {
            return Connection.dm.fsp_Korisnici_SelectByUsername(username).SingleOrDefault();
        }

        public static void Update(Korisnici user)
        {
            Connection.dm.fsp_Korisnici_Update(user.KorisnikID, user.Ime, user.Prezime, user.Mail, user.Spol, user.DatumRodjenja, user.Slika, user.SlikaType);
            Connection.dm.SaveChanges();
        }

        public static Korisnici GetByID(int korisnikID)
        {
            return Connection.dm.fsp_Korisnici_SelectByID(korisnikID).SingleOrDefault();
        }
    }
}
