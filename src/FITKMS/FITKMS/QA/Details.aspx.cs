using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.QA
{
    public partial class Details : System.Web.UI.Page
    {
        List<fsp_Odgovori_SelectByPitanjeId_Result> answers;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (User.Identity.Name == "")
                {
                    answerMessagePanel.Visible = true;
                    answerPanel.Visible = false;
                }
                else
                {
                    answerMessagePanel.Visible = false;
                    answerPanel.Visible = true;
                }

                if (Request.QueryString != null)
                {
                    if (Request.QueryString["id"] != null)
                    {

                        int id = Int32.Parse(Request.QueryString["id"].ToString());
                        editLink.PostBackUrl = "Edit.aspx?id=" + id;

                        Pitanja pitanje = QAService.getPitanjeByID(id);

                        if (pitanje != null)
                        {

                            Session.Add("PitanjeID", pitanje.PitanjeID);
                            lblNaslovPitanja.Text = pitanje.Naslov;
                            lblTextPitanja.Text = pitanje.Tekst;
                            lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                            lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();
                            lblBrojPregleda.Text = pitanje.BrojPregleda.ToString();
                            Korisnici korinik = QAService.GetKorisnikByID(pitanje.KorisnikID);
                            lblKorisnik.Text = "by " + korinik.Ime + " " + korinik.Prezime;
                            lblDatum.Text = pitanje.DatumKreiranja.ToShortDateString();

                            List<Tagovi> tags = QAService.getListaTagovaUpitanju(pitanje.PitanjeID);;
                            tagsRepeater.DataSource = tags;
                            tagsRepeater.DataBind();

                            BindOdgovori(pitanje.PitanjeID);
                            BindGrade();
                            //// broje pregleda na stranici
                            QAService.SetBrojPregleda(pitanje);

                            //Preporuka StackOverflow
                            StackOverflowRecommendation(tags, pitanje.Naslov);

                            HtmlControl div = (HtmlControl)this.Master.FindControl("articleRecommend");
                            div.Visible = false;

                            ItemBasedRecommendation();
                        }
                        else
                            Response.Redirect("Index.aspx");

                    }
                    else
                        Response.Redirect("Index.aspx");

                }

            }

        }

        private void ItemBasedRecommendation()
        {
            DataList questionsList = (DataList)this.Master.FindControl("questionsList");
            RecommendationService recommendation = new RecommendationService();

            //Ukoliko je korisnik prijavljen ukloniti ocijenjena pitanja iz preporuke
            int userId = 0;
            if (User.Identity.Name != "")
                userId = Convert.ToInt32(User.Identity.Name);

            questionsList.DataSource = recommendation.GetTopQuestionsMatches(Int32.Parse(Request.QueryString["id"].ToString()), userId);
            questionsList.DataBind();
        }

        private void StackOverflowRecommendation(List<Tagovi> tags, string title)
        {
            DataList stackOverflowList = (DataList)this.Master.FindControl("stackOverflowList");

            List<string> words = new List<string>();

            foreach (Tagovi t in tags)
            {
                if (t.Naziv.Length >= 4)
                    words.Add(t.Naziv);
            }

            words.Add(title);
            words = words.Distinct().ToList();

            ExternalIntegration integration = new ExternalIntegration();
            List<Question> questionsStack = new List<Question>();
            List<Question> questionsStackRecommend = new List<Question>();


            foreach (string w in words)
            {
                questionsStack.Clear();
                questionsStack.AddRange(integration.SearchStackOverflow(w));
                questionsStackRecommend.AddRange(questionsStack.Take(3).ToList());
            }

            questionsStackRecommend = questionsStackRecommend.Distinct().ToList();
            stackOverflowList.DataSource = questionsStackRecommend;
            stackOverflowList.DataBind();

            HtmlControl div = (HtmlControl)this.Master.FindControl("stackOverflowRecommend");
            div.Visible = true;
        }


        private void BindGrade()
        {
            if (User.Identity.Name != "")
            {
                int pitanjeID = (int)Session["PitanjeID"];
                PitanjaOcjene userGrade = DAPitanja.GetGradeForUser(pitanjeID, Convert.ToInt32(User.Identity.Name));
                if (userGrade != null)
                {
                    articleRating.CurrentRating = userGrade.Ocjena;
                    articleRating.ReadOnly = true;
                    ratingLabel.Text = "Vaša ocjena: " + userGrade.Ocjena.ToString();
                    dateRatedLabel.Text = string.Format("{0:dd.MM.yyyy}", userGrade.DatumKreiranja);
                    rating_block.Visible = true;
                }
            }
        }

        private void BindOdgovori(int pitanjeID)
        {
            dtOdgovori.DataSource = null;
            answers = QAService.getAllOdgovoriZaPitanje(pitanjeID);
            dtOdgovori.DataSource = answers;
            dtOdgovori.DataBind();

            foreach (DataListItem item in dtOdgovori.Items)
            {
                if (User.Identity.Name != "")
                {
                    if (answers[item.ItemIndex].KorisnikID == Convert.ToInt32(User.Identity.Name))
                    {
                        LinkButton deleteLink = (LinkButton)item.FindControl("deleteLink");
                        deleteLink.Visible = true;
                    }
                }
            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            if (wysiwyg.Text != "")
            {

                int id = Int32.Parse(Request.QueryString["id"].ToString());

                int pitanjeID = (int)Session["PitanjeID"];

                Odgovori odg = new Odgovori();
                odg.PitanjeID = pitanjeID;
                odg.Tekst = wysiwyg.Text;
                odg.KorisnikID = Convert.ToInt32(User.Identity.Name);
                odg.Pozitivni = 0;
                odg.Negativni = 0;
                odg.DatumKreiranja = DateTime.Now;
                odg.DatumIzmjene = DateTime.Now;

                QAService.saveOdgovor(odg);
                BindOdgovori(pitanjeID);
                wysiwyg.Text = "";
            }
        }

        protected void likeButton_Click(object sender, EventArgs e)
        {
            if (User.Identity.Name != "")
            {
                int pitanjeID = (int)Session["PitanjeID"];

                LinkButton btn = (LinkButton)(sender);

                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn); // aktivira postback za linkbutton


                int IdOdgovora = int.Parse(btn.CommandArgument);

                int korisnikID = Convert.ToInt32(User.Identity.Name);

                bool glasao = QAService.Je_LiGlasao(korisnikID, IdOdgovora);

                if (glasao == false)
                {

                    Odgovori odg = QAService.getOdgovorByID(IdOdgovora);
                    QAService.UpdateOdgovorP(odg, korisnikID);

                    BindOdgovori(pitanjeID);
                }
                else
                {
                    Odgovori odgovor = QAService.getOdgovorByID(IdOdgovora);
                    QAService.UpdateAnswerLikeStatus(odgovor, Convert.ToInt32(User.Identity.Name), true);
                    BindOdgovori(pitanjeID);
                }
            }
        }

        protected void dislikeButton_Click(object sender, EventArgs e)
        {
            if (User.Identity.Name != "")
            {
                int pitanjeID = (int)Session["PitanjeID"];

                LinkButton btn = (LinkButton)(sender);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

                int IdOdgovora = int.Parse(btn.CommandArgument);

                int korisnikID = Convert.ToInt32(User.Identity.Name);

                bool glasao = QAService.Je_LiGlasao(korisnikID, IdOdgovora);

                if (glasao == false)
                {
                    Odgovori odg = QAService.getOdgovorByID(IdOdgovora);
                    QAService.UpdateOdgovorN(odg, korisnikID);

                    BindOdgovori(pitanjeID);
                }
                else
                {
                    Odgovori odgovor = QAService.getOdgovorByID(IdOdgovora);
                    QAService.UpdateAnswerLikeStatus(odgovor, Convert.ToInt32(User.Identity.Name), false);
                    BindOdgovori(pitanjeID);
                }
            }

        }

        protected void likePitanje_Click(object sender, EventArgs e)
        {
            if (User.Identity.Name != "")
            {
                int pitanjeID = (int)Session["PitanjeID"];
                LinkButton btn = (LinkButton)(sender);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

                int korisnikID = Convert.ToInt32(User.Identity.Name);

                bool glasao = QAService.Je_LiGlasaoZaPitanje(korisnikID, pitanjeID);

                if (glasao == false)
                {

                    Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);

                    QAService.UpdatePitanjeLike(pitanje, korisnikID);

                    lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                    lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();

                }
                else
                {
                    Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);
                    QAService.UpdateQuestionLikeStatus(pitanje, Convert.ToInt32(User.Identity.Name), true);
                    lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                    lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();
                }
            }
        }

        protected void dislikePitanje_Click(object sender, EventArgs e)
        {
            if (User.Identity.Name != "")
            {
                int pitanjeID = (int)Session["PitanjeID"];
                LinkButton btn = (LinkButton)(sender);
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

                int korisnikID = Convert.ToInt32(User.Identity.Name);

                bool glasao = QAService.Je_LiGlasaoZaPitanje(korisnikID, pitanjeID);

                if (glasao == false)
                {

                    Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);

                    QAService.UpdatePitanjeDislike(pitanje, korisnikID);

                    lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                    lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();

                }
                else
                {
                    Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);
                    QAService.UpdateQuestionLikeStatus(pitanje, Convert.ToInt32(User.Identity.Name), false);
                    lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                    lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();
                }
            }

        }

        protected void deleteLink_Click(object sender, EventArgs e)
        {
            int pitanjeID = (int)Session["PitanjeID"];
            LinkButton btn = (LinkButton)(sender);
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

            int IdOdgovora = int.Parse(btn.CommandArgument);
            DAPitanja.UpdateAnswerStatus(IdOdgovora, false);

            BindOdgovori(pitanjeID);
        }

        protected void articleRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            if (User.Identity.Name == "")
            {
                warning.Text = "Pitanje mogu ocijeniti samo prijavljeni korisnici!";
                warning_block.Visible = true;
            }
            else
            {
                int pitanjeID = (int)Session["PitanjeID"];
                PitanjaOcjene grade = new PitanjaOcjene();
                grade.PitanjeID = pitanjeID;
                grade.KorisnikID = Convert.ToInt32(User.Identity.Name);
                grade.Ocjena = articleRating.CurrentRating;
                grade.DatumKreiranja = DateTime.Now;
                grade.DatumIzmjene = DateTime.Now;
                DAPitanja.GradeQuestion(grade);
                articleRating.ReadOnly = true;

                ratingLabel.Text = "Vaša ocjena: " + grade.Ocjena.ToString();
                dateRatedLabel.Text = string.Format("{0:dd.MM.yyyy}", grade.DatumKreiranja);
                rating_block.Visible = true;

                //BindGrade();
            }
        }

    }
}
