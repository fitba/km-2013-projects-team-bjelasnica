using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;
using FITKMS_business.Util;
using System.Web.UI.HtmlControls;

namespace FITKMS.QA
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<fsp_Pitanja_SelectSearch_Result> pitanja;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {
                BindGrid();
                pitanjaGrid.CurrentPageIndex = 0;

                if (User.Identity.Name != "")
                    UserQuestionRecommendation();
                else
                    RecommendBestRated();
                   

                StackOverflowRecommendation();
                HtmlControl articleRecommend = (HtmlControl)this.Master.FindControl("articleRecommend");
                articleRecommend.Visible = false;
            }
        }

        private void BindGrid()
        {
            int offset = pitanjaGrid.CurrentPageIndex * pitanjaGrid.PageSize;
            string search = '"' + searchInput.Text.Trim() + '"';
            pitanja = QAService.SelectSearch(search, pitanjaGrid.PageSize, offset);
            pitanjaGrid.VirtualItemCount = QAService.totalRows;
            pitanjaGrid.DataBind();
        }

        private void RecommendBestRated()
        {
            DataList questionsList = (DataList)this.Master.FindControl("questionsList");
            Label titleQA = (Label)this.Master.FindControl("recQATitleLabel");
            titleQA.Text = "Preporučena pitanja";
            questionsList.DataSource = DAPitanja.SelectBestLiked();;
            questionsList.DataBind();

        }

        private void UserQuestionRecommendation()
        {
            DataList questionsList = (DataList)this.Master.FindControl("questionsList");
            RecommendationService recommendation = new RecommendationService();
            questionsList.DataSource = recommendation.ColaborativeFiltering(Convert.ToInt32(User.Identity.Name));
            questionsList.DataBind();
        }

        private void StackOverflowRecommendation()
        {
            DataList stackOverflowList = (DataList)this.Master.FindControl("stackOverflowList");

            List<string> words = new List<string>();

            if (searchInput.Text.Trim() != "")
                words.Add(searchInput.Text);
            else
            {
                foreach (DataGridItem item in pitanjaGrid.Items)
                {
                    LinkButton titleLink = (LinkButton)item.FindControl("titleLink");
                    words.Add(titleLink.Text);
                }
            }

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

        protected void pitanjaGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            pitanjaGrid.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void pitanjaGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = pitanja[e.Item.ItemIndex].Tekst;//getPart(pitanja[e.Item.ItemIndex].Tekst);
                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = QAService.getListaTagovaUpitanju(pitanja[e.Item.ItemIndex].PitanjeID);
                tagsRepeater.DataBind();
            }
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

            if (text.Length > 200)
            {
                text = text.Substring(0, 500);
                for (int i = 499; i > 0; i--)
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

        protected void searchArticlesSubmit_Click(object sender, EventArgs e)
        {
            pitanjaGrid.CurrentPageIndex = 0;
            BindGrid();
            StackOverflowRecommendation();
            saveSearch();
        }

        private void saveSearch()
        {
            try
            {
                //Pohrani pretragu korisnika
                if (pitanjaGrid.Items.Count > 0 && User.Identity.Name != "")
                {
                    Pretrage search = new Pretrage();
                    search.AktivnostID = DAAktivnosti.Select("Pretraga pitanja").AktivnostID;
                    search.TekstPretrage = searchInput.Text;
                    search.KorisnikID = Convert.ToInt32(User.Identity.Name);
                    search.Datum = DateTime.Now;
                    DAAktivnosti.Insert(search);
                }
            }
            catch
            {

            }
        }
    }
}