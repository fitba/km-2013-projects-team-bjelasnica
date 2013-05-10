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

        public static void UpdateAnswerStatus(int IdOdgovora, bool status)
        {
            Connection.dm.fsp_Odgovori_UpdateStatus(IdOdgovora, status);
            Connection.dm.SaveChanges();
        }

        public static List<fsp_Pitanja_SelectLastAnswers_Result> SelectLastAnswers()
        {
            return Connection.dm.fsp_Pitanja_SelectLastAnswers().ToList();
        }

        public static List<fsp_Pitanja_SelectUnAnswered_Result> SelectUnAnswered()
        {
            return Connection.dm.fsp_Pitanja_SelectUnAnswered().ToList();
        }

        public static int Count()
        {
            return Connection.dm.Pitanja.Count();
        }

        public static List<fsp_Pitanja_SelectBestLiked_Result> SelectBestLiked()
        {
            return Connection.dm.fsp_Pitanja_SelectBestLiked().ToList();
        }

        public static void GradeQuestion(PitanjaOcjene grade)
        {
            Connection.dm.PitanjaOcjene.Add(grade);
            Connection.dm.SaveChanges();
        }

        public static PitanjaOcjene GetGradeForUser(int pitanjeID, int userID)
        {
            return Connection.dm.fsp_PitanjaOcjene_SelectByUser(pitanjeID, userID).FirstOrDefault();
        }

        public static void Update(Pitanja question, List<string> tags)
        {
            Connection.dm.fsp_Pitanja_Update(question.PitanjeID, question.Naslov, question.Tekst, question.TemaID, question.KorisnikID, question.DatumIzmjene);
            Connection.dm.SaveChanges();
            Connection.dm.fsp_Pitanja_DeleteTags(question.PitanjeID);
            Connection.dm.SaveChanges();
            foreach (string t in tags)
            {
                try
                {
                    Connection.dm.fsp_PitanjaTagovi_Insert(question.PitanjeID, t);
                    Connection.dm.SaveChanges();
                }
                catch
                {
                }
            }
        }

        public static List<fsp_Pitanja_SelectSearch_Result> SearchByTheme(string theme, int themeId,  int maxRows, int offset)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Pitanja_SelectSearch_Result> pitanja = Connection.dm.fsp_Pitanja_SelectByTheme(theme, themeId, offset, maxRows, total).ToList();

            totalRows = Convert.ToInt32(total.Value);

            return pitanja;
        }
    }
}
