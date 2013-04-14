using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class DAClanci
    {
        public static void Insert(Clanci article, List<Int32> tagIds)
        {
            Connection.dm.Clanci.Add(article);
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
    }
}
