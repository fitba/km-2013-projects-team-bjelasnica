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

        public static void AddFavoriteTag(KorisniciTagovi favoriteTag)
        {
            KorisniciTagovi userTag = new KorisniciTagovi();
            userTag = Connection.dm.KorisniciTagovi.Where(k => k.KorisnikID == favoriteTag.KorisnikID && k.TagID == favoriteTag.TagID).SingleOrDefault();
            if (userTag == null)
            {
                Connection.dm.KorisniciTagovi.Add(favoriteTag);
                Connection.dm.SaveChanges();
            }
            else
            {
                Connection.dm.KorisniciTagovi.First(k => k.KorisnikID == favoriteTag.KorisnikID && k.TagID == favoriteTag.TagID).Status = true;
                Connection.dm.SaveChanges();
            }
        }

        public static bool CheckFavoriteTag(int userID, int tagID)
        {
            KorisniciTagovi userTag = new KorisniciTagovi();
            userTag = Connection.dm.KorisniciTagovi.Where(k => k.KorisnikID == userID && k.TagID == tagID).SingleOrDefault();
            if (userTag != null)
            {
                if (userTag.Status == true)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        public static void UpdateTagStatus(int userID, int tagID)
        {
            Connection.dm.KorisniciTagovi.First(k => k.KorisnikID == userID && k.TagID == tagID).Status = false;
            Connection.dm.SaveChanges();
        }

        public static void UpdateLastLogin(int userID, DateTime lastLogin)
        {
            Connection.dm.Korisnici.First(k => k.KorisnikID == userID).PosljednjaPrijava = lastLogin;
            Connection.dm.SaveChanges();
        }

        public static int CountNewArticles(DateTime lastLogin)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("BrojClanaka", 0);
            Connection.dm.fsp_Clanci_CountNewByUser(lastLogin, total);
            return Convert.ToInt32(total.Value);
        }

        public static int CountNewQuestions(DateTime lastLogin)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("BrojPitanja", 0);
            Connection.dm.fsp_Pitanja_CountNewByUser(lastLogin, total);
            return Convert.ToInt32(total.Value);
        }

        public static int Count()
        {
            return Connection.dm.Korisnici.Count();
        }

        public static bool IsAdmin(string user)
        {
            if (user != "")
            {
                int userId = Convert.ToInt32(user);
                List<KorisniciUloge> listaUloga = Connection.dm.KorisniciUloge.Where(x => x.KorisnikID == userId).ToList();

                foreach (KorisniciUloge i in listaUloga)
                {
                    if (Connection.dm.Uloge.Where(x => x.UlogaID == i.UlogaID).Single().Naziv.Contains("Admin"))
                        return true;
                }
            }

            return false;

        }
    }
}
