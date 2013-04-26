using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;
namespace FITKMS.QA
{
    public partial class Tags : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {

                dtListaTagova.DataSource = QAService.getTagoviAll();
                dtListaTagova.DataBind();
            
            
            }



        }
    }
}