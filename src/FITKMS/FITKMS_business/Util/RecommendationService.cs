using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FITKMS_business.Data;

namespace FITKMS_business.Util
{
    public class RecommendationService
    {
        #region Item-based Collaborative Recommmendation (similarity calculated with Pearson Correlation)

        //http://www.codeproject.com/Articles/221865/Collaborative-Filters-How-to-build-a-recommendatio
        Dictionary<int, List<ClanciOcjene>> articleRecommendation = new Dictionary<int, List<ClanciOcjene>>();
        Dictionary<int, List<PitanjaOcjene>> questionRecommendation = new Dictionary<int, List<PitanjaOcjene>>();

        //Ako je korisnik prijavljen potrebno je izbaciti iz preporuke članke koje je već pregledao, ocijenio...
        public List<Pitanja> GetTopQuestionsMatches(int questionId, int userId)
        {
            GetRatingsForQuestions(questionId, userId);
            var sortedList = questionRecommendation.Where(x => x.Key != questionId);

            List<Pitanja> recommendations = new List<Pitanja>();

            foreach (var entry in sortedList)
            {
                double pearson = CalculatePearsonCorrelationForQuestions(questionId, entry.Key);
                //Postaviti minimalnu vrijednost koeficijenta
                if (pearson >= 0.2)
                    recommendations.Add(QAService.getPitanjeByID(entry.Key));
            }

            return recommendations;
            
        }
        public List<fsp_Clanci_SelectById_Result> GetTopArticleMatches(int articleId, int userId)
        {
            //Inicijalno ukloniti članke sa lošom prosječnom ocjenom (<2 ako ima više od 5 glasova)
            //Inverse User Frequency fj = log(n/nj) - 0 ako su svi korisnici ocijenili item j
            //Ovakve članke ukloniti iz preporuke?

            GetRatingsForArticles(articleId, userId);
            var sortedList = articleRecommendation.Where(x => x.Key != articleId);
            List<fsp_Clanci_SelectById_Result> recommendations = new List<fsp_Clanci_SelectById_Result>();

            foreach (var entry in sortedList)
            {
                double pearson = CalculatePearsonCorrelationForArticles(articleId, entry.Key);
                //Postaviti minimalnu vrijednost koeficijenta
                if (pearson >= 0.2)
                    recommendations.Add(DAClanci.SelectById(entry.Key));
            }
            

            return recommendations;
            
        }

        private void GetRatingsForArticles(int articleId, int userId)
        {
            List<fsp_Clanci_SelectActive_Result> articles = Connection.dm.fsp_Clanci_SelectActive(userId, articleId).ToList();
           
            foreach (fsp_Clanci_SelectActive_Result a in articles)
            {
                List<ClanciOcjene> ratings = Connection.dm.fsp_ClanciOcjene_SelectByArticle(a.ClanakID).ToList();
                articleRecommendation.Add(a.ClanakID, ratings);
            }

        }

        private double CalculatePearsonCorrelationForArticles(int article1, int article2)
        {
            List<ClanciOcjene> sharedArticles = new List<ClanciOcjene>();

            //Pronaći ocjene korisnika koji su ocijenili oba članka
            foreach (var item in articleRecommendation[article1])
            {
                if (articleRecommendation[article2].Where(x => x.KorisnikID == item.KorisnikID).Count() != 0)
                    sharedArticles.Add(item);
                
            }

            if (sharedArticles.Count == 0)
                return 0;

            //Razmisliti da li izbaciti one zapise gdje je autor dao ocjenu

            //Sumirati sve ocjene za članke
            double article1RatingSum = 0.00f;
            double article2RatingSum = 0.00f;

            foreach (ClanciOcjene item in sharedArticles)
            {
                article1RatingSum += articleRecommendation[article1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;
                article2RatingSum += articleRecommendation[article2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;

            }

            double article1SQ = 0f;
            double article2SQ = 0f;
            foreach (ClanciOcjene item in sharedArticles)
            {
                article1SQ += Math.Pow(articleRecommendation[article1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena, 2);
                article2SQ += Math.Pow(articleRecommendation[article2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena, 2);
            }

            double sum = 0f;
            foreach (ClanciOcjene item in sharedArticles)
            {
                sum += articleRecommendation[article1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena *
                                articleRecommendation[article2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;

            }

            double num = sum - (article1RatingSum * article2RatingSum / sharedArticles.Count);

            double density = (double)Math.Sqrt((article1SQ - Math.Pow(article1RatingSum, 2) / sharedArticles.Count) *
                             ((article2SQ - Math.Pow(article2RatingSum, 2) / sharedArticles.Count)));

            if (density == 0)
                return 0;

            return num / density;
        }

        private double CalculatePearsonCorrelationForQuestions(int question1, int question2)
        {
            List<PitanjaOcjene> sharedQuestions = new List<PitanjaOcjene>();

            //Pronaći ocjene korisnika koji su ocijenili i jedno i drugo pitanje
            foreach (var item in questionRecommendation[question1])
            {
                if (questionRecommendation[question2].Where(x => x.KorisnikID == item.KorisnikID).Count() != 0)
                    sharedQuestions.Add(item);

            }

            if (sharedQuestions.Count == 0)
                return 0;

            //Sumirati sve ocjene za pitanja
            double question1RatingSum = 0.00f;
            double question2RatingSum = 0.00f;

            foreach (PitanjaOcjene item in sharedQuestions)
            {
                question1RatingSum += questionRecommendation[question1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;
                question2RatingSum += questionRecommendation[question2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;

            }

            double question1SQ = 0f;
            double question2SQ = 0f;
            foreach (PitanjaOcjene item in sharedQuestions)
            {
                question1SQ += Math.Pow(questionRecommendation[question1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena, 2);
                question2SQ += Math.Pow(questionRecommendation[question2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena, 2);
            }

            double sum = 0f;
            foreach (PitanjaOcjene item in sharedQuestions)
            {
                sum += questionRecommendation[question1].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena *
                                questionRecommendation[question2].Where(x => x.KorisnikID == item.KorisnikID).FirstOrDefault().Ocjena;

            }

            double num = sum - (question1RatingSum * question2RatingSum / sharedQuestions.Count);

            double density = (double)Math.Sqrt((question1SQ - Math.Pow(question1RatingSum, 2) / sharedQuestions.Count) *
                             ((question2SQ - Math.Pow(question2RatingSum, 2) / sharedQuestions.Count)));

            if (density == 0)
                return 0;

            return num / density;
        }

        private void GetRatingsForQuestions(int questionId, int userId)
        {
            List<Pitanja> questions = Connection.dm.fsp_Pitanja_SelectActive(userId, questionId).ToList();

            foreach (Pitanja q in questions)
            {
                List<PitanjaOcjene> ratings = Connection.dm.fsp_PitanjaOcjene_SelectByQuestion(q.PitanjeID).ToList();
                questionRecommendation.Add(q.PitanjeID, ratings);
            }

        }

        #endregion

        #region Collaborative and Content-based Recommendation

        public List<fsp_Clanci_SelectById_Result> GetTopArticles(int userId)
        {
            List<Korisnici> usersInClaster = GenerateUserCluster(userId);
            //Dodati aktivnog korisnika u klaster
            usersInClaster.Add(DAKorisnici.GetByID(userId));
            //Učitati kolekciju pregledanih članaka sličnih korisnika koje aktivni korisnik nije već ocijenio, pregledao

            List<ItemProfile> articles = new List<ItemProfile>();
            List<ItemProfile> activeUserArticles = new List<ItemProfile>();

            foreach (Korisnici item in usersInClaster)
            {
                List<fsp_Clanci_SelectUserCollection_Result> initArticles = Connection.dm.fsp_Clanci_SelectUserCollection(item.KorisnikID, 3).ToList();

                foreach (fsp_Clanci_SelectUserCollection_Result a in initArticles)
                {
                    ItemProfile ip = new ItemProfile(a.ClanakID, DAClanci.SelectTags(a.ClanakID));
                    articles.Add(ip);
                    if (item.KorisnikID == userId)
                        activeUserArticles.Add(ip);
                }

            }

            //articles = articles.Distinct().ToList(); 
            //Filtrirati tagove i proračunati "tag weight" za preostale
            CompleteItemProfile(articles);

            //Ukloniti pregledane članke aktivnog korisnika i kopirati "tag weight"
            for (int i = 0; i < articles.Count; i++)
            {
                if (activeUserArticles.Contains(articles[i]))
                {
                    articles.RemoveAt(i);
                    i--;
                }
            }
           
            //Učitati kolekciju članaka aktivnog korisnika i ponoviti postupak za određivanja "tag weight"

            //Formirati matricu sličnih članaka i primijeniti treshold 0.3
            List<fsp_Clanci_SelectById_Result> recommendations = new List<fsp_Clanci_SelectById_Result>();
            foreach (ItemProfile activeItem in activeUserArticles)
            {
                foreach (ItemProfile otherItem in articles)
                {
                    double sim = CalculateCosineSimilarity(activeItem.ItemTags, otherItem.ItemTags);
                    if (sim > 0.3)
                    {
                        //Provjeriti da li je članak već preporučen (sličan nekom od prethodnih)
                        bool exist = false;
                        foreach (fsp_Clanci_SelectById_Result item in recommendations)
                        {
                            if (item.ClanakID == otherItem.ID)
                            {
                                exist = true;
                                break;

                            }
                        }
                        if (!exist)
                            recommendations.Add(DAClanci.SelectById(otherItem.ID));
                    }

                }
            }

            return recommendations.OrderByDescending(x=>x.ProsjecnaOcjena).ToList();
        }

        private void CompleteItemProfile(List<ItemProfile> articles)
        {
            Dictionary<Tagovi, int> tagsFreq = new Dictionary<Tagovi,int>();
            //Broj članaka u klasteru
            int articleNum = articles.Count;

            foreach (ItemProfile item in articles)
            {
                tagsFreq.Clear();
                foreach (Tagovi tag in item.ItemTags.Keys)
                {
                    tagsFreq.Add(tag, (int)Connection.dm.fsp_Tagovi_CountWithinArticles(tag.TagID).FirstOrDefault());
                }

                //Pronaći tag koji se najviše puta ponavlja
                int maxFreq = tagsFreq.Values.Max();

                double tagTF = 0;
                foreach (var tag in tagsFreq)
                {
                    tagTF = (double)tag.Value/ maxFreq;
                    if (tagTF < 0.2) //Treshold iznosi 0.2
                    {
                        tagsFreq.Remove(tag.Key);
                        item.ItemTags.Remove(tag.Key);
                    }
                    else
                    {
                        int articleNumByTag = 0;
                        foreach (var a in articles)
                        {
                            if (a.TagExists(tag.Key))
                                articleNumByTag++;
                        }

                        if (articleNumByTag != 0) //Dodati u ItemProfile
                            item.ItemTags[tag.Key] = tag.Value * Math.Log10((double)articles.Count / articleNumByTag);

                    }
                }
                 
            }
        }

        private List<Korisnici> GenerateUserCluster(int userId)
        {
            List<Korisnici> users = Connection.dm.Korisnici.Where(x=>x.KorisnikID != userId).ToList();
            List<Korisnici> usersInCluster = new List<Korisnici>();
            Dictionary<Tagovi, double> targetUserVector = GenerateUserProfile(userId);
            foreach (Korisnici item in users)
            {
                double sim = CalculateCosineSimilarity(targetUserVector, GenerateUserProfile(item.KorisnikID));
                if(sim > 0.2)
                    usersInCluster.Add(item);
            }

            return usersInCluster;
        }

        private double CalculateCosineSimilarity(Dictionary<Tagovi, double> vector1, Dictionary<Tagovi, double> vector2)
        {
            //Pronaći zajedničke tagove
            List<Tagovi> sharedTags = new List<Tagovi>();

            foreach (var item1 in vector1)
            {
                foreach (var item2 in vector2)
                {
                    if (item1.Key.TagID == item2.Key.TagID)
                    {
                        sharedTags.Add(item1.Key);
                        break;
                    }
                }

            }

            double numerator = 0;
            double vectorInt1 = 0;
            double vectorInt2 = 0;

            foreach (Tagovi tag in sharedTags)
            {
                numerator += vector1[tag] * vector2[tag];
                vectorInt1 += vector1[tag] * vector1[tag];
                vectorInt2 += vector2[tag] * vector2[tag];
            }

            if (vectorInt1 == 0 || vectorInt2 == 0)
                return 0;

            return numerator/(Math.Sqrt(vectorInt1) * Math.Sqrt(vectorInt2));

        }

        private Dictionary<Tagovi, double> GenerateUserProfile(int userId)
        {
            //Tag frequency–inverse user frequency (TF-IUF) 
            Dictionary<Tagovi, double> userTags = new Dictionary<Tagovi, double>();

            foreach (Tagovi item in Connection.dm.fsp_Tagovi_SelectByUser(userId))
            {
                userTags.Add(item, GetLocalWeight(userId, item.TagID)*GetGlobalWeight(item.TagID));
            }

            return userTags;
        }

        private double GetGlobalWeight(int tagId)
        {
            //Ukupan broj korisnika
            int usrNum = Connection.dm.Korisnici.Count();
            //Broj korisnika sa određenim tag-om
            int usrNumTag = (int)Connection.dm.fsp_Korisnici_CountByTag(tagId).First();

            if (usrNumTag == 0)
                return 0;

            return Math.Log10(usrNum / usrNumTag);
        }

        private double GetLocalWeight(int userId, int tagId)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("BrojTagova", 0);
            int tagNum = DATagovi.CountByUser(total, userId, tagId);
            int maxTagNum = Convert.ToInt32(total.Value);

            if (maxTagNum == 0)
                return 0;

            return (double)tagNum / maxTagNum;
        }

        #endregion

        #region User-based Recommendation

        public List<Pitanja> ColaborativeFiltering(int ID)
        {
            //sva pitanja za korisnika
            List<PitanjaOcjene> OcijenjenaPitanja = Connection.dm.PitanjaOcjene.Where(x => x.KorisnikID == ID).OrderBy(x => x.PitanjeID).ToList();
            //sva pitanja koja su ocijenjena
            List<int> SvaPitanja = Connection.dm.PitanjaOcjene.OrderBy(x => x.PitanjeID).Select(x => x.PitanjeID).Distinct().ToList();
            //sva pitanja koja korisnik nije ocijenio
            List<int> SvaPitanjaKojaKorisnikNijeOcijenio = new List<int>();
            List<Pitanja> ListaPreporucenihPitanja = new List<Pitanja>();


            foreach (var i in SvaPitanja) // uzimam sva pitanja koje korisnik nije ocijenio 
            {
                int brojac = 0;

                foreach (var j in OcijenjenaPitanja)
                {
                    if (i == j.PitanjeID)
                        brojac++;
                }

                if (brojac == 0)
                {
                    SvaPitanjaKojaKorisnikNijeOcijenio.Add(i); // lista tih neocijenjenih pitanja
                }

            }

            List<int> SviKorisniciKojiSuOcijenili = new List<int>(); // lista korisnika koji su ocijenili pitanja koja logovani korisnik nije

            foreach (var i in SvaPitanjaKojaKorisnikNijeOcijenio)
            {
                SviKorisniciKojiSuOcijenili.AddRange(Connection.dm.PitanjaOcjene.Where(x => x.PitanjeID == i).Select(x => x.KorisnikID).ToList());
            }

            foreach (var i in SviKorisniciKojiSuOcijenili) // kroz sve korisnike radi se provjera
            {
                List<PitanjaOcjene> ListaSamoIstihPitanja = new List<PitanjaOcjene>();
                foreach (var item in OcijenjenaPitanja) // ubacujem u listu samo ona pitanja koja logovani korisnik ima radi preporuke
                {
                    PitanjaOcjene p = Connection.dm.PitanjaOcjene.Where(x => x.PitanjeID == item.PitanjeID && x.KorisnikID == i).SingleOrDefault();
                    if (p != null)
                    {
                        ListaSamoIstihPitanja.Add(p);
                    }

                }

                if (ListaSamoIstihPitanja.Count == OcijenjenaPitanja.Count) // poredim samo ista pitanja od korisnika
                {
                    int sumaOcjenaLogovanog = 0;
                    int sumaOcjenaDrugogKorisnika = 0;

                    foreach (var LS in OcijenjenaPitanja) //LS logovani kor, samo da se izračuna suma ocjena
                    {
                        int a = LS.PitanjeID;
                        sumaOcjenaLogovanog += LS.Ocjena;
                    }

                    foreach (var LD in ListaSamoIstihPitanja) //lista drugi korisnik
                    {
                        int a = LD.PitanjeID;
                        sumaOcjenaDrugogKorisnika += LD.Ocjena;
                    }

                    double Ra = (double)sumaOcjenaLogovanog / (double)OcijenjenaPitanja.Count; // prosjek ocjena za logovanog

                    double Rb = (double)sumaOcjenaDrugogKorisnika / (double)ListaSamoIstihPitanja.Count; // prosjek ocjena za logovanog

                    double Brojnik = 0;
                    double Nazivnik = 0;

                    for (int e = 0; e < OcijenjenaPitanja.Count; e++) //// prolazim kroz sve zajednicke elemente i racunam Pearson Correlation
                    {
                        double A = (OcijenjenaPitanja[e].Ocjena - Ra);
                        double B = (ListaSamoIstihPitanja[e].Ocjena - Rb);

                        Brojnik += A * B;

                        double C = Math.Pow(A, 2);
                        double D = Math.Pow(B, 2);

                        Nazivnik += Math.Sqrt(C + D);

                    }

                    double similar = Brojnik / Nazivnik; // ovo je slicnost 

                    if (similar > 0.2) /// threshold
                    {
                        //preporucuje korisnika koji je slican

                        List<PitanjaOcjene> SvaPitanjaKorisnikaPoSlicnosti = Connection.dm.PitanjaOcjene.Where(x => x.KorisnikID == i).ToList();

                        foreach (var item in SvaPitanjaKorisnikaPoSlicnosti) // uzimam sva pitanja koje korisnik nije ocijenio 
                        {

                            int brojac = 0;

                            foreach (var j in OcijenjenaPitanja)
                            {
                                if (item.PitanjeID == j.PitanjeID)
                                    brojac++;
                            }

                            if (brojac == 0)
                            {
                                Pitanja p = new Pitanja();
                                p = Connection.dm.Pitanja.Where(x => x.PitanjeID == item.PitanjeID).SingleOrDefault();
                                ListaPreporucenihPitanja.Add(p);
                            }

                        }
                    }
                }
            }

            return ListaPreporucenihPitanja;
        }

        #endregion
    }
}
