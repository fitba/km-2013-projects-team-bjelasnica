using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Wiki
{
    public partial class Download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["articleId"] != null)
                {
                    int articleId = Convert.ToInt32(Request["articleId"]);
                    fsp_Clanci_SelectById_Result article = DAClanci.SelectById(articleId);
                    if (article.Dokument != null)
                    {
                        Response.ContentType = article.DokumentType;
                        Response.AddHeader("Content-Disposition", "filename=" + article.Naslov);
                        Response.BinaryWrite(article.Dokument);
                        
                    }
                }
            }
        }
    }
}