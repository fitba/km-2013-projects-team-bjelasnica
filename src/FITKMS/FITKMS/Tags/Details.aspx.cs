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
        protected List<fsp_Clanci_SelectByTypeTitle_Result> articles;
        protected List<fsp_Pitanja_SelectByTag_Result> questions;

        protected void Page_Load(object sender, EventArgs e)
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

                BindArticles();
                BindQuestions();
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
    }
}