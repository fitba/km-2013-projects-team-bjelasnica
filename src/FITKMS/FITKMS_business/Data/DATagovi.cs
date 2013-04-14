using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DATagovi
    {
        public static List<Tagovi> SelectAll()
        {
            return Connection.dm.Tagovi.OrderBy(x=>x.Naziv).ToList();
        }
    }
}
