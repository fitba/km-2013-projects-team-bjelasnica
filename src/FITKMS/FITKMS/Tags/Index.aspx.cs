using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Tags
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["pageNo"] = 1;
                BindTags();
            }
        }

        protected void searchTagsSubmit_Click(object sender, EventArgs e)
        {
            ViewState["pageNo"] = 1;
            BindTags();
        }

        private void BindTags()
        {
            int recordPerPage = 12;
            int pageNo = Convert.ToInt32(ViewState["pageNo"]);

            if (pageNo < 1)
                pageNo = 1;
            
            string search = searchInput.Text;
            if (search == "")
                search = null;

            listTags.DataSource = DATagovi.SelectAllPagination(pageNo, recordPerPage, search);
            listTags.DataBind();

            if (DATagovi.totalRows == 0)
            {
                panelPager.Visible = false;
                warning_label.Visible = true;
            }
            else
            {
                panelPager.Visible = true;
                warning_label.Visible = false;
            }
            PopulatePager(DATagovi.totalRows, pageNo, recordPerPage);

        }

        private void PopulatePager(int totalRows, int pageNo, int recordPerPage)
        {
            double dblPageCount = (double)((decimal)totalRows / recordPerPage);
            int pageCount = (int) Math.Ceiling(dblPageCount);

            labelCurrent.Text = "Stranica " + pageNo.ToString() + " od " + pageCount.ToString();
            ViewState["pageCount"] = pageCount.ToString();
        }

        protected void linkPrevious_Click(object sender, EventArgs e)
        {
            ViewState["pageNo"] = Convert.ToInt32(ViewState["pageNo"]) - 1;

            if (Convert.ToInt32(ViewState["pageNo"]) < 1)
                ViewState["pageNo"] = 1;

            BindTags();
        }

        protected void linkNext_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(ViewState["pageCount"]) != Convert.ToInt32(ViewState["pageNo"]))
            ViewState["pageNo"] = Convert.ToInt32(ViewState["pageNo"]) + 1;
            
            BindTags();
        }
    }
}