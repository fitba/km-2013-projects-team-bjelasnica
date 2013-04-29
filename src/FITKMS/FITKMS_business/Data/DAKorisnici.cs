using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public static void ChangePassword(int userID, string passwordSalt, string passwordHash)
        {
            Connection.dm.fsp_Korisnici_ChangePassword(userID, passwordSalt, passwordHash);
            Connection.dm.SaveChanges();
        }

        public static KorisniciDataSet.korisniciRow SelectById(int userId)
        {
            SqlConnection con = new SqlConnection(Connection.dm.Database.Connection.ConnectionString);
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("[fsp_Korisnici_SelectByID]", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("@KorisnikID", System.Data.SqlDbType.Int).Value = userId;

                KorisniciDataSet ds = new KorisniciDataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds.korisnici);

                return ds.korisnici[0];

            }
            finally
            {
                con.Close();
            }
        }
    }
}
