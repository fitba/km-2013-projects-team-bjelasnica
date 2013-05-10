using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DATeme
    {
        public static List<Teme> Select(bool status)
        {
            List<Teme> themes = Connection.dm.fsp_Teme_SelectByStatus(status).ToList();
            Teme empty = new Teme();
            empty.Naziv = "Odaberite temu";
            empty.TemaID = 0;
            themes.Insert(0, empty);

            return themes;
        }

        public static List<Teme> SelectByArea(int oblastID)
        {
            return Connection.dm.Teme.Where(t => t.OblastID == oblastID).Where(t => t.Status == true).ToList();
        }

        public static void Save(Teme theme)
        {
            Connection.dm.Teme.Add(theme);
            Connection.dm.SaveChanges();
        }

        public static void UpdateStatus(int themeID)
        {
            Connection.dm.Teme.First(t => t.TemaID == themeID).Status = false;
            Connection.dm.SaveChanges();
        }

        public static Teme SelectTemaByID(int TemaID)
        {
            return Connection.dm.Teme.Where(t => t.TemaID == TemaID).SingleOrDefault();
        }

        public static int Count()
        {
            return Connection.dm.Teme.Where(x => x.Status == true).Count();
        }

        public static List<Teme> SelectByName(string name)
        {
            return Connection.dm.fsp_Teme_SelectByName(name).ToList();
        }

        public static List<fsp_Teme_SelectMostUsed_Result> SelectMostUsed()
        {
            return Connection.dm.fsp_Teme_SelectMostUsed().Where(x=>x.BrojPonavljanja>5).ToList();
        }

    }
}
