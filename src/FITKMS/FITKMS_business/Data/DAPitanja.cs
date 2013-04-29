using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAPitanja
    {
        public static int totalRows;

        public static List<Tagovi> SelectTags(int pitanjeId)
        {
            return Connection.dm.fsp_Pitanja_SelectTags(pitanjeId).ToList();
        }

        public static List<fsp_Pitanja_SelectByTag_Result> SelectByTagId(int tagId, int maxRows, int offset)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Pitanja_SelectByTag_Result> questions = Connection.dm.fsp_Pitanja_SelectByTag(tagId, offset, maxRows, total).ToList();
            totalRows = Convert.ToInt32(total.Value);

            return questions;
        }
    }
}
