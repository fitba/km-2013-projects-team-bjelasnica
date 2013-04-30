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

        public static void Update(Clanci article, List<Tagovi> tags)
        {
            Connection.dm.fsp_Clanci_Update(article.ClanakID, article.Naslov, article.Autori, article.Tekst, article.KljucneRijeci,
                                            article.VrstaID, article.TemaID, article.Dokument, article.DokumentType, article.DokumentPath);

            Connection.dm.fsp_Clanci_DeleteTags(article.ClanakID);

            foreach (Tagovi t in tags)
            {
                Connection.dm.fsp_ClanciTagovi_Insert(article.ClanakID, t.TagID);
            }
        }

        public static Clanci Select(int clanakId)
        {
           return Connection.dm.Clanci.Find(clanakId);
        }

        public static List<fsp_Clanci_SelectByTypeTitle_Result> SearchByTypeTitle(int typeId, string searchText, int maxRows, int offset)
        { 
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Clanci_SelectByTypeTitle_Result> articles = Connection.dm.fsp_Clanci_SelectByTypeTitle
                                                                 (typeId, searchText, offset, maxRows, total).ToList();
            totalRows = Convert.ToInt32(total.Value);

            return articles;
        }

        public static fsp_Clanci_SelectById_Result SelectById(int articleId)
        {
            return Connection.dm.fsp_Clanci_SelectById(articleId).First();
        }

        #region Tags

        public static List<Tagovi> SelectTags(int clanakId)
        {
            return Connection.dm.fsp_Clanci_SelectTags(clanakId).ToList();
        }

        public static List<fsp_Clanci_SelectByTag_Result> SelectByTagId(int tagId, int maxRows, int offset)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Clanci_SelectByTag_Result> articles = Connection.dm.fsp_Clanci_SelectByTag(tagId, offset, maxRows, total).ToList();
            totalRows = Convert.ToInt32(total.Value);

            return articles;
        }

        #endregion

        #region Types

        public static List<VrsteClanaka> SelectTypes(bool status)
        {
            List<VrsteClanaka> types = Connection.dm.fsp_VrsteClanaka_SelectByStatus(status).ToList();
            VrsteClanaka empty = new VrsteClanaka();
            empty.Naziv = "Odaberite vrstu";
            empty.VrstaID = 0;
            types.Insert(0, empty);

            return types;

        }

        #endregion

        #region Comments

        public static List<fsp_ClanciKomentari_Select_Result> SelectComments(int articleId, int maxRows, int offset)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);

            List<fsp_ClanciKomentari_Select_Result> comments = Connection.dm.fsp_ClanciKomentari_Select(articleId, offset, maxRows, total).ToList();
            totalRows = Convert.ToInt32(total.Value);

            return comments;
        }

        public static void AddComment(ClanciKomentari comment)
        {
            Connection.dm.ClanciKomentari.Add(comment);
            Connection.dm.SaveChanges();
        }

        public static void GradeArticle(ClanciOcjene grade)
        {
            Connection.dm.ClanciOcjene.Add(grade);
            Connection.dm.SaveChanges();
        }

        public static ClanciOcjene GetGradeForUser(int articleId, int userId)
        {
            return Connection.dm.fsp_ClanciOcjene_SelectByUser(articleId, userId).FirstOrDefault();
        }

        #endregion

    }
}
