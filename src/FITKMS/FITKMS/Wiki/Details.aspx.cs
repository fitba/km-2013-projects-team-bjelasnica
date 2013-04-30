using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FITKMS_business.Data;

namespace FITKMS.Wiki
{
    public partial class Details : System.Web.UI.Page
    {
        private fsp_Clanci_SelectById_Result article;
        protected List<fsp_ClanciKomentari_Select_Result> comments;

        protected Int32 articleId
        {
            get { return (Int32)ViewState["articleId"]; }
            set { ViewState["articleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindArticle();
                BindComments(false);

                if (User.Identity.Name == "")
                {
                    commentMessagePanel.Visible = true;
                }
                else
                    commentPanel.Visible = true;
            }
        }

        private void BindComments(bool newPage)
        {
            if (ViewState["articleId"] != null)
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
                    if (item.ItemIndex % 2 != 0)
                        commentBlock.Attributes.Add("class", "out");

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

                tagsRepeater.DataSource = DAClanci.SelectTags(articleId);
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
            }
        }

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
            commentPanel.Visible = false;

            bool newPage = false;

            if (commentsGrid.VirtualItemCount % commentsGrid.PageSize != 0)
                commentsGrid.CurrentPageIndex = commentsGrid.PageCount - 1;
            else if (commentsGrid.VirtualItemCount != 0)
                newPage = true;

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

        private void BindGrade()
        {
            article = DAClanci.SelectById(articleId);
            avgGradeLabel.Text = Math.Round((decimal)article.ProsjecnaOcjena, 2).ToString();
        }
    }
}