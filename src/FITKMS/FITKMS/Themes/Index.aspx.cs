using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;
using System.Web.UI.HtmlControls;

namespace FITKMS.Themes
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<fsp_Pitanja_SelectSearch_Result> questions;
        protected List<fsp_Clanci_SelectSearch_Result> articlesList;
        protected List<fsp_Clanci_SelectBestRated_Result> articlesBestRated;
        protected List<fsp_Pitanja_SelectBestLiked_Result> questionsBestLiked;
        protected List<fsp_Teme_SelectMostUsed_Result> themesMostUsed;

        protected Int32 themeId
        {
            get { return (Int32)ViewState["themeId"]; }
            set { ViewState["themeId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RecommendThemes();

                themeId = 0;
                if (Request["id"] != null)
                {
                    themeId = Convert.ToInt32(Request["id"]);
                }
                else
                    themeId = themesMostUsed[0].TemaID;

                BindArticles();
                BindQuestions();
                RecommendBestRated();
            }
        }

        private void RecommendThemes()
        {
            DataList themesList = (DataList)this.Master.FindControl("themesList");
            themesMostUsed = DATeme.SelectMostUsed();
            themesList.DataSource = themesMostUsed;
            themesList.DataBind();

            HtmlControl themesRecommend = (HtmlControl)this.Master.FindControl("themesRecommend");
            themesRecommend.Visible = true;
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

        private void BindArticles()
        {
            int offset = articlesGrid.CurrentPageIndex * articlesGrid.PageSize;
            string search = '"' + searchInput.Text.Trim() + '"';
            articlesList = DAClanci.SearchByTheme(search, themeId, articlesGrid.PageSize, offset);
            articlesGrid.VirtualItemCount = DAClanci.totalRows;
            articlesGrid.DataBind();

        }

        private void BindQuestions()
        {
            int offset = questionsGrid.CurrentPageIndex * questionsGrid.PageSize;
            string search = '"' + searchInput.Text.Trim() + '"';
            questions = DAPitanja.SearchByTheme(search, themeId, questionsGrid.PageSize, offset);
            questionsGrid.VirtualItemCount = QAService.totalRows;
            questionsGrid.DataBind();  
        }

        protected void articlesGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            articlesGrid.CurrentPageIndex = e.NewPageIndex;
            BindArticles();
        }

        protected void articlesGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = Helper.GetStringPart(articlesList[e.Item.ItemIndex].Tekst, 150);

                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = DAClanci.SelectTags(articlesList[e.Item.ItemIndex].ClanakID);
                tagsRepeater.DataBind();
            }
        }

        protected void pitanjaGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
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

        protected void pitanjaGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            questionsGrid.CurrentPageIndex = e.NewPageIndex;
            BindQuestions();
        }

        protected void searchSubmit_Click(object sender, EventArgs e)
        {
            articlesGrid.CurrentPageIndex = 0;
            questionsGrid.CurrentPageIndex = 0;
            BindArticles();
            BindQuestions();
        }

        #region WebMethod
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethod()]
        public static string[] GetThemeNames(string prefixText, int count)
        {
            List<Teme> themes = DATeme.SelectByName(prefixText);
            List<string> themeNames = new List<string>();

            foreach (Teme theme in themes)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                    (theme.Naziv, theme.TemaID.ToString());
                themeNames.Add(item);

            }

            return themeNames.ToArray();

        }
        #endregion
    }
}