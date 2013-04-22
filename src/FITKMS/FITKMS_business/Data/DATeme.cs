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
    }
}
