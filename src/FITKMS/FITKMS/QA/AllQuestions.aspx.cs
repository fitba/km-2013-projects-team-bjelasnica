using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.QA
{
    public partial class AllQuestions : System.Web.UI.Page
    {
        protected List<fsp_Pitanja_SelectAll_Result> pitanja;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {
                BindGrid();
                pitanjaGrid.CurrentPageIndex = 0;
            }

        }

        private void BindGrid()
        {

            int offset = pitanjaGrid.CurrentPageIndex * pitanjaGrid.PageSize;
            pitanja = QAService.Pitanja_Get_All(pitanjaGrid.PageSize, offset);
            pitanjaGrid.VirtualItemCount = QAService.totalRows;
            pitanjaGrid.DataBind();

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
    }
}