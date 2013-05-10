using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAOblasti
    {
        public static List<Oblasti> Select()
        {
            return Connection.dm.Oblasti.Where(o => o.Status == true).ToList();
        }

        public static void Save(Oblasti area)
        {
            Connection.dm.Oblasti.Add(area);
            Connection.dm.SaveChanges();
        }

        public static void UpdateStatus(int areaID)
        {
            Connection.dm.Oblasti.First(o => o.OblastID == areaID).Status = false;
            Connection.dm.SaveChanges();
        }
    }
}
