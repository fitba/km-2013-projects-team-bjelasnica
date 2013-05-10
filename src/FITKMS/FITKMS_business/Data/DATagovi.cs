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
            return Connection.dm.Tagovi.OrderBy(x => x.Naziv).Where(x => x.Status == true).ToList();
        }

        public static List<fsp_Tagovi_SelectPagination_Result> SelectAllPagination(int pageNo, int recordPerPage, string search)
        {
            List<fsp_Tagovi_SelectPagination_Result> tags = new List<fsp_Tagovi_SelectPagination_Result>();
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

        public static void Save(Tagovi tag)
        {
            Connection.dm.Tagovi.Add(tag);
            Connection.dm.SaveChanges();
        }

        public static void UpdateStatus(int tagID)
        {
            Connection.dm.Tagovi.First(t => t.TagID == tagID).Status = false;
            Connection.dm.SaveChanges();
        }

        public static int Count()
        {
           return Connection.dm.Tagovi.Count();
        }

        //Koliko puta se određeni tag ponavlja za željenog korisnika i ukupan broj tagova tog korisnika (sa ponavljanjem?)
        public static int CountByUser(System.Data.Objects.ObjectParameter totalTags, int userId, int tagId)
        {
           return (int)Connection.dm.fsp_Tagovi_CountByUser(userId, tagId, totalTags).FirstOrDefault();
        }
    }
}
