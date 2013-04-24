using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAClanci
    {
        public static int totalRows;
        public static void Insert(Clanci article, List<Tagovi> tags)
        {
            Connection.dm.Clanci.Add(article);
            Connection.dm.SaveChanges();

            foreach (Tagovi t in tags)
            {
                Connection.dm.fsp_ClanciTagovi_Insert(article.ClanakID, t.TagID);
            }
        }

        public static List<VrsteClanaka> SelectTypes(bool status)
        {
            List<VrsteClanaka> types = Connection.dm.fsp_VrsteClanaka_SelectByStatus(status).ToList();
            VrsteClanaka empty = new VrsteClanaka();
            empty.Naziv = "Odaberite vrstu";
            empty.VrstaID = 0;
            types.Insert(0, empty);

            return types;
        }

        public static List<fsp_Clanci_SelectByTypeTitle_Result> SearchByTypeTitle(int typeId, string title, int maxRows, int offset)
        { 
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Clanci_SelectByTypeTitle_Result> articles = Connection.dm.fsp_Clanci_SelectByTypeTitle
                                                                 (typeId, title, offset, maxRows, total).ToList();
            totalRows = Convert.ToInt32(total.Value);

            return articles;
        }

        public static fsp_Clanci_SelectById_Result SelectById(int articleId)
        {
            return Connection.dm.fsp_Clanci_SelectById(articleId).First();
        }
    }
}
