using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Wiki
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<VrsteClanaka> types;
        protected List<fsp_Clanci_SelectByTypeTitle_Result> articles;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTypes();
                BindGrid();
            }
        }

        protected void searchArticlesSubmit_Click(object sender, EventArgs e)
        {
            articlesGrid.CurrentPageIndex = 0;
            BindGrid();
        }

        private void BindTypes()
        {
            types = DAClanci.SelectTypes(true);
            typesList.DataBind();
        }

        private void BindGrid()
        {
            int offset = articlesGrid.CurrentPageIndex * articlesGrid.PageSize;
            int typeId = 0;
            if (typesList.SelectedIndex != 0)
                typeId = Convert.ToInt32(typesList.SelectedValue);

            articles = DAClanci.SearchByTypeTitle(typeId, titleInput.Text.Trim(), articlesGrid.PageSize, offset);
            articlesGrid.VirtualItemCount = DAClanci.totalRows;
            articlesGrid.DataBind();

        }

        protected void articlesGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            articlesGrid.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }
    }
}