using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.Wiki
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<VrsteClanaka> types;
        protected List<fsp_Clanci_SelectSearch_Result> articles;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        protected void searchArticlesSubmit_Click(object sender, EventArgs e)
        {
            articlesGrid.CurrentPageIndex = 0;
            BindGrid();
        }

        private void BindGrid()
        {
            int offset = articlesGrid.CurrentPageIndex * articlesGrid.PageSize;

            articles = DAClanci.Search(searchInput.Text.Trim(), articlesGrid.PageSize, offset);
            articlesGrid.VirtualItemCount = DAClanci.totalRows;
            articlesGrid.DataBind();

        }

        protected void articlesGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            articlesGrid.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
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
    }
}