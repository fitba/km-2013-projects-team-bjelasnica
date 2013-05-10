using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.Tags
{
    public partial class Details : System.Web.UI.Page
    {
        protected List<fsp_Clanci_SelectByTag_Result> articles;
        protected List<fsp_Pitanja_SelectByTag_Result> questions;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Int32.Parse(Request.QueryString["id"].ToString());
                    ViewState["tagId"] = id.ToString();

                    Tagovi tag = DATagovi.getTagByID(id);

                    if (tag != null)
                    {
                        labelTag.Text = tag.Naziv;
                        labelDescription.Text = tag.Opis;
                    }

                    ShowFavorite(id);

                    BindArticles();
                    BindQuestions();
                }
            }
        }

        private void ShowFavorite(int tagID)
        {
            if (User.Identity.Name != "")
            {
                favoriteTag.Visible = true;

                if (DAKorisnici.CheckFavoriteTag(Convert.ToInt32(User.Identity.Name), tagID))
                {
                    favoriteTag.Visible = false;
                    noFavoriteTag.Visible = true;
                }
                else
                {
                    favoriteTag.Visible = true;
                    noFavoriteTag.Visible = false;
                }
            }
        }

        private void BindArticles()
        {
            int offset = articlesGrid.CurrentPageIndex * articlesGrid.PageSize;
            int tagId = Convert.ToInt32(ViewState["tagId"]);

            articles = DAClanci.SelectByTagId(tagId, articlesGrid.PageSize, offset);
            articlesGrid.VirtualItemCount = DAClanci.totalRows;
            articlesGrid.DataBind();
        }

        private void BindQuestions()
        {
            int offset = questionsGrid.CurrentPageIndex * questionsGrid.PageSize;
            int tagId = Convert.ToInt32(ViewState["tagId"]);

            questions = DAPitanja.SelectByTagId(tagId, questionsGrid.PageSize, offset);
            questionsGrid.VirtualItemCount = DAPitanja.totalRows;
            questionsGrid.DataBind();
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

        protected void articlesGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            articlesGrid.CurrentPageIndex = e.NewPageIndex;
            BindArticles();
        }

        protected void questionsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = questions[e.Item.ItemIndex].Tekst;//getPart(questions[e.Item.ItemIndex].Tekst);

                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = DAPitanja.SelectTags(questions[e.Item.ItemIndex].PitanjeID);
                tagsRepeater.DataBind();
            }
        }

        protected void questionsGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            questionsGrid.CurrentPageIndex = e.NewPageIndex;
            BindQuestions();
        }

        protected void favoriteTag_Click(object sender, EventArgs e)
        {
            KorisniciTagovi favoriteTag = new KorisniciTagovi();
            favoriteTag.KorisnikID = Convert.ToInt32(User.Identity.Name);
            favoriteTag.TagID = Convert.ToInt32(ViewState["tagId"]);
            favoriteTag.Datum = DateTime.Now;
            favoriteTag.Status = true;
            DAKorisnici.AddFavoriteTag(favoriteTag);
            ShowFavorite(Convert.ToInt32(ViewState["tagId"]));
        }

        protected void noFavoriteTag_Click(object sender, EventArgs e)
        {
            DAKorisnici.UpdateTagStatus(Convert.ToInt32(User.Identity.Name), Convert.ToInt32(ViewState["tagId"]));
            ShowFavorite(Convert.ToInt32(ViewState["tagId"]));
        }
    }
}