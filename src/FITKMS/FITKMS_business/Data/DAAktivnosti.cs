using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAAktivnosti
    {
        public static Aktivnosti Select(string name)
        {
            return Connection.dm.fsp_Aktivnosti_SelectByName(name).First();
        }

        public static void Insert(Pretrage search)
        {
            try
            {
                Connection.dm.Pretrage.Add(search);
                Connection.dm.SaveChanges();
            }
            catch (Exception)
            {
                Connection.dm.Pretrage.Remove(search);
            }
        }
    }
}
