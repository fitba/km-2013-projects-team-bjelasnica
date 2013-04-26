using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;

namespace FITKMS.QA
{
    public partial class TagDetaljno : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {

                if (Request.QueryString != null)
                {
                    if (Request.QueryString["id"] != null)
                    {
                        int id = Int32.Parse(Request.QueryString["id"].ToString());

                        Tagovi tag = QAService.getTagByID(id);

                        if (tag != null)
                        {
                            lblNazivTaga.Text = tag.Naziv;
                            lblOpisTaga.Text = tag.Opis;
                        
                        } 
                        
                        else
                {
                    Response.Redirect("Tags.aspx");
                }
                    }

                    else
                    {
                        Response.Redirect("Tags.aspx");

                    }

                }

               




            }
            


        }
    }
}