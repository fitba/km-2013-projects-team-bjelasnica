using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.Wiki
{
    public partial class Details : System.Web.UI.Page
    {
        private fsp_Clanci_SelectById_Result article;
        protected List<fsp_ClanciKomentari_Select_Result> comments;
        protected List<fsp_ClanciIzmjene_Select_Result> history;

        protected Int32 articleId
        {
            get { return (Int32)ViewState["articleId"]; }
            set { ViewState["articleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["comments"] != null)
                    activateCommentTab();

                BindArticle();
                BindComments(false);
                BindHistory();

                if (User.Identity.Name == "")
                {
                    commentMessagePanel.Visible = true;
                    commentPanel.Visible = false;
                }
                else
                    saveView();
            }

        }

        private void saveView()
        {
            try
            {
                Pretrage search = new Pretrage();
                search.AktivnostID = DAAktivnosti.Select("Pregled članka").AktivnostID;
                search.KorisnikID = Convert.ToInt32(User.Identity.Name);
                search.ClanakID = articleId;
                search.Datum = DateTime.Now;
                DAAktivnosti.Insert(search);

            }
            catch
            {

            }
        }

        #region Bindings

        private void BindComments(bool newPage)
        {
            if (Request["articleId"] != null)
            {
                int offset;
                if (newPage)
                    offset = commentsGrid.PageCount * commentsGrid.PageSize;
                else
                    offset = commentsGrid.CurrentPageIndex * commentsGrid.PageSize;

                comments = DAClanci.SelectComments(articleId, commentsGrid.PageSize, offset);

                commentsGrid.VirtualItemCount = DAClanci.totalRows;
                commentsGrid.DataBind();

                foreach (DataGridItem item in commentsGrid.Items)
                {
                    HtmlControl commentBlock = (HtmlControl)item.FindControl("commentBlock");
                    HtmlControl deleteLinkBlock = (HtmlControl)item.FindControl("deleteLinkBlock");

                    if (item.ItemIndex % 2 != 0)
                    {
                        commentBlock.Attributes.Add("class", "out");
                        deleteLinkBlock.Attributes.Add("class", "tools pull-left");
                    }

                    if (User.Identity.Name != "")
                    {
                        if (comments[item.ItemIndex].KorisnikID == Convert.ToInt32(User.Identity.Name))
                        {
                            LinkButton deleteLink = (LinkButton)item.FindControl("deleteLink");
                            deleteLink.Visible = true;
                        }
                    }

                }

                if (newPage)//Ne promijeni se u dizajnu
                    commentsGrid.CurrentPageIndex = commentsGrid.PageCount - 1;
            }
        }

        private void BindArticle()
        {
            if (Request["articleId"] != null)
            {
                articleId = Convert.ToInt32(Request["articleId"]);
                editLink.PostBackUrl = "Edit.aspx?articleId=" + articleId;
                article = DAClanci.SelectById(articleId);
                titleLabel.Text = article.Naslov;
                typeLable.Text = article.Vrsta;
                themeLabel.Text = article.Tema;
                authorsLabel.Text = article.Autori;
                dateCreatedLabel.Text = string.Format("{0:dd.MM.yyyy}", article.DatumKreiranja);
                dateChangedLabel.Text = string.Format("{0:dd.MM.yyyy}", article.DatumIzmjene);

                if (article.ProsjecnaOcjena != null)
                    avgGradeLabel.Text = Math.Round((decimal)article.ProsjecnaOcjena, 2).ToString();
                else
                    avgGradeLabel.Text = "0.00";

                textLiteral.Text = article.Tekst;

                List<Tagovi> tags = DAClanci.SelectTags(articleId);
                tagsRepeater.DataSource = tags;
                tagsRepeater.DataBind();

                if (User.Identity.Name != "")
                {
                    ClanciOcjene userGrade = DAClanci.GetGradeForUser(articleId, Convert.ToInt32(User.Identity.Name));
                    if (userGrade != null)
                    {
                        articleRating.CurrentRating = userGrade.Ocjena;
                        articleRating.ReadOnly = true;
                        ratingLabel.Text = "Vaša ocjena: " + userGrade.Ocjena.ToString();
                        dateRatedLabel.Text = string.Format("{0:dd.MM.yyyy}", userGrade.DatumKreiranja);
                        rating_block.Visible = true;
                    }
                }

                if (article.Dokument != null)
                {
                    pdfIcon.Visible = true;
                    pdfDownloadLink.HRef = "Download.aspx?articleId=" + articleId;
                }

                //Preporuka Wikipedia
                WikiRecommendation(tags, article.Naslov);

                //Item-based preporuka
                ItemBasedRecommendation();

                HtmlControl div = (HtmlControl)this.Master.FindControl("questionRecommend");
                div.Visible = false;

            }
        }

        private void ItemBasedRecommendation()
        {
            DataList articlesList = (DataList)this.Master.FindControl("articlesList");
            RecommendationService recommendation = new RecommendationService();

            //Ukoliko je korisnik prijavljen ukloniti pregledane članke iz preporuke
            int userId = 0;
            if (User.Identity.Name != "")
                userId = Convert.ToInt32(User.Identity.Name);
            articlesList.DataSource = recommendation.GetTopArticleMatches(articleId, userId);
            articlesList.DataBind();
        }

        private void WikiRecommendation(List<Tagovi> tags, string title)
        {
            DataList wikiList = (DataList)this.Master.FindControl("wikiList");

            List<string> words = new List<string>();

            foreach (Tagovi t in tags)
            {
                if (t.Naziv.Length >= 4)
                    words.Add(t.Naziv);
            }

            words.Add(title);
            words = words.Distinct().ToList();

            ExternalIntegration integration = new ExternalIntegration();
            List<WikiP> articlesWiki = new List<WikiP>();
            List<WikiP> articlesWikiRecommend = new List<WikiP>();

            foreach (string w in words)
            {
                articlesWiki.Clear();
                articlesWiki.AddRange(integration.SearchWikipedia(w));
                articlesWikiRecommend.AddRange(articlesWiki.Take(3).ToList());
            }

            articlesWikiRecommend = articlesWikiRecommend.Distinct().ToList();
            wikiList.DataSource = articlesWikiRecommend;
            wikiList.DataBind();

            HtmlControl div = (HtmlControl)this.Master.FindControl("wikiRecommend");
            div.Visible = true;
        }

        private void BindGrade()
        {
            article = DAClanci.SelectById(articleId);
            avgGradeLabel.Text = Math.Round((decimal)article.ProsjecnaOcjena, 2).ToString();
        }

        private void BindHistory()
        {
            if (Request["articleId"] != null)
            {
                int offset = historyGrid.CurrentPageIndex * historyGrid.PageSize;
                history = DAClanci.SelectHistory(articleId, historyGrid.PageSize, offset);

                historyGrid.VirtualItemCount = DAClanci.totalRows;
                historyGrid.DataBind();
            }
        }

        #endregion

        protected void commentsGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            if (commentTab.Attributes["class"] == "")
                activateCommentTab();
            commentsGrid.CurrentPageIndex = e.NewPageIndex;
            BindComments(false);
        }

        private void activateCommentTab()
        {
            defaultTab.Attributes.Add("class", "");
            commentTab.Attributes.Add("class", "active");
            articleComments.Attributes.Add("class", "tab-pane fade active in");
            home.Attributes.Add("class", "tab-pane fade");
        }

        protected void addCommentSubmit_Click(object sender, EventArgs e)
        {
            ClanciKomentari comment = new ClanciKomentari();
            comment.Komentar = wysiwyg.InnerText;
            comment.ClanakID = articleId;
            comment.KorisnikID = Convert.ToInt32(User.Identity.Name);
            comment.DatumKreiranja = DateTime.Now;
            comment.DatumIzmjene = DateTime.Now;
            comment.Status = true;

            DAClanci.AddComment(comment);

            wysiwyg.InnerText = "";

            bool newPage = false;

            if (commentsGrid.VirtualItemCount % commentsGrid.PageSize != 0)
                commentsGrid.CurrentPageIndex = commentsGrid.PageCount - 1;
            else if (commentsGrid.VirtualItemCount != 0)
                newPage = true;

            activateCommentTab();
            BindComments(newPage);

        }

        protected void articleRating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
        {
            if (User.Identity.Name == "")
            {
                warning.Text = "Članak mogu ocijeniti samo prijavljeni korisnici.";
                warning_block.Visible = true;
            }
            else
            {
                ClanciOcjene grade = new ClanciOcjene();
                grade.ClanakID = articleId;
                grade.KorisnikID = Convert.ToInt32(User.Identity.Name);
                grade.Ocjena = articleRating.CurrentRating;
                grade.DatumKreiranja = DateTime.Now;
                grade.DatumIzmjene = DateTime.Now;
                DAClanci.GradeArticle(grade);
                articleRating.ReadOnly = true;

                ratingLabel.Text = "Vaša ocjena: " + grade.Ocjena.ToString();
                dateRatedLabel.Text = string.Format("{0:dd.MM.yyyy}", grade.DatumKreiranja);
                rating_block.Visible = true;

                BindGrade();
            }
        }

        protected void commentsGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "deleteCommand")
            {
                int articleCommentId = Convert.ToInt32(e.CommandArgument);
                DAClanci.UpdateCommentStatus(articleCommentId, false);
                activateCommentTab();
                BindComments(false);
            }
        }

        protected void historyGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            historyGrid.CurrentPageIndex = e.NewPageIndex;
            BindHistory();
        }
    }
}