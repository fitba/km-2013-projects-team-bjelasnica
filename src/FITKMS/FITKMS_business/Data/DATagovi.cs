using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DATagovi
    {
        public static int totalRows;

        public static List<Tagovi> SelectAll()
        {
            return Connection.dm.Tagovi.OrderBy(x => x.Naziv).ToList();
        }

        public static List<Tagovi> SelectAllPagination(int pageNo, int recordPerPage, string search)
        {
            List<Tagovi> tags = new List<Tagovi>();
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            tags = Connection.dm.fsp_Tagovi_SelectPagination(pageNo, recordPerPage, total, search).ToList();
            totalRows = Convert.ToInt32(total.Value);
            return tags;
        }

        public static Tagovi getTagByID(int id)
        {
            return Connection.dm.Tagovi.Where(x => x.TagID == id).SingleOrDefault();
        }

        public static List<Tagovi> SelectByName(string name)
        {
            return Connection.dm.fsp_Tagovi_SelectByName(name).ToList();
        }
    }
}
