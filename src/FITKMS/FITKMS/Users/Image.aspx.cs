using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;

namespace FITKMS.Users
{
    public partial class Image : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpContext context = HttpContext.Current;
            string username = "";
            Korisnici user = new Korisnici();
            if (context.Request["username"] != null)
            {
                username = context.Request["username"];
                user = DAKorisnici.GetByUsername(username);

                context.Response.BinaryWrite(user.Slika);
                context.Response.ContentType = user.SlikaType;
            }

        }
    }
}