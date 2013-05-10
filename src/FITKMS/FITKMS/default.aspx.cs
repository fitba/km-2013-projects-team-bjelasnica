using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS
{
    public partial class Default : System.Web.UI.Page
    {
        protected List<fsp_Clanci_SelectSearch_Result> articles;
        protected List<fsp_Clanci_SelectLastComments_Result> commentsArticles;
        protected List<fsp_Pitanja_SelectSearch_Result> questions;
        protected List<fsp_Pitanja_SelectLastAnswers_Result> answerQuestions;
        protected List<fsp_Pitanja_SelectUnAnswered_Result> unansweredQuestions;
        protected List<fsp_Clanci_SelectBestRated_Result> articlesBestRated;
        protected List<fsp_Pitanja_SelectBestLiked_Result> questionsBestLiked;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindArticles();
                BindQuestions();
                BindCommentArticles();
                BindAnswerQuestions();
                BindUnAnsweredQuestions();
                RecommendBestRated();
            }
        }

        private void BindArticles()
        {
            string search = '"' + "" + '"';
            articles = DAClanci.Search(search, 10, 0);
            articlesGrid.DataBind();
        }

        private void BindQuestions()
        {
            string search = '"' + "" + '"';
            questions = QAService.SelectSearch(search, 10, 0);
            questionsGrid.DataBind();
        }

        private void BindCommentArticles()
        {
            commentsArticles = DAClanci.SelectLastComments();
            commentsArticleGrid.DataBind();
        }

        private void BindAnswerQuestions()
        {
            answerQuestions = DAPitanja.SelectLastAnswers();
            answersQuestionGrid.DataBind();
        }

        private void BindUnAnsweredQuestions()
        {
            unansweredQuestions = DAPitanja.SelectUnAnswered();
            unansweredGrid.DataBind();
        }

        private void RecommendBestRated()
        {
            articlesBestRated = DAClanci.SelectBestRated();
            DataList articlesList = (DataList)this.Master.FindControl("articlesList");
            Label title = (Label)this.Master.FindControl("recTitleLabel");
            title.Text = "Preporučeni članci";
            articlesList.DataSource = articlesBestRated;
            articlesList.DataBind();

            questionsBestLiked = DAPitanja.SelectBestLiked();
            DataList questionsList = (DataList)this.Master.FindControl("questionsList");
            Label titleQA = (Label)this.Master.FindControl("recQATitleLabel");
            titleQA.Text = "Preporučena pitanja";
            questionsList.DataSource = questionsBestLiked;
            questionsList.DataBind(); 

        }

        private string getPart(string text)
        {
            if (text.IndexOf("<p>") == 0)
                text = text.Remove(0, 3);
            text = text.Replace("<p>", "\n");
            text = text.Replace("<P>", "\n");
            text = text.Replace("<BR>", "\n");
            text = text.Replace("<BR />", "\n");
            text = text.Replace("<br>", "\n");
            text = text.Replace("<br />", "\n");
            text = HtmlRemoval.StripTagsCharArray(text);

            if (text.Length > 150)
            {
                text = text.Substring(0, 150);
                for (int i = 99; i > 0; i--)
                {
                    if (text[i] != ' ')
                        text = text.Remove(i, 1);
                    else
                        break;
                }
                text = text + " ...";
            }

            text = text.Replace("\n", "<br />");
            return text;
        }

        protected void articlesGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = getPart(articles[e.Item.ItemIndex].Tekst);
                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = DAClanci.SelectTags(articles[e.Item.ItemIndex].ClanakID);
                tagsRepeater.DataBind();
            }
        }

        protected void questionsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = questions[e.Item.ItemIndex].Tekst;//getPart(pitanja[e.Item.ItemIndex].Tekst);
                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = QAService.getListaTagovaUpitanju(questions[e.Item.ItemIndex].PitanjeID);
                tagsRepeater.DataBind();
            }
        }

        protected void commentsArticleGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = getPart(commentsArticles[e.Item.ItemIndex].Tekst);
                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = DAClanci.SelectTags(commentsArticles[e.Item.ItemIndex].ClanakID);
                tagsRepeater.DataBind();
            }
        }

        protected void answersQuestionGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = answerQuestions[e.Item.ItemIndex].Tekst;//getPart(pitanja[e.Item.ItemIndex].Tekst);
                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = QAService.getListaTagovaUpitanju(answerQuestions[e.Item.ItemIndex].PitanjeID);
                tagsRepeater.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> words = new List<string>();
            words.Add("Linux");
            words.Add("Unix");
            words.Add("Ubuntu");

            ExternalIntegration integration = new ExternalIntegration();
            List<WikiP> articlesWiki = new List<WikiP>();
            List<WikiP> articlesWikiRecommend = new List<WikiP>();

            foreach (string w in words)
            {
                articlesWiki.Clear();
                articlesWiki.AddRange(integration.SearchWikipedia(w));
                articlesWikiRecommend.AddRange(articlesWiki.Take(200).ToList());
            }

            articlesWikiRecommend = articlesWikiRecommend.Distinct().ToList();

            List<string> tags = new List<string>();

            tags.Add("Linux");
            tags.Add("Unix");
            tags.Add("Ubuntu");

            foreach (var item in articlesWikiRecommend)
            {
                Clanci clanak = new Clanci();
                clanak.Naslov = item.Name;
                clanak.Tekst = item.Description;
                clanak.Autori = "Wiki";
                clanak.KljucneRijeci = "linux, unix, ubuntu";
                clanak.VrstaID = 4;
                clanak.TemaID = 24;
                clanak.KorisnikID = 21;
                clanak.Status = true;
                clanak.DatumKreiranja = DateTime.Now;
                clanak.DatumIzmjene = DateTime.Now;

                DAClanci.Insert(clanak, tags);
            }
        }
    }
}