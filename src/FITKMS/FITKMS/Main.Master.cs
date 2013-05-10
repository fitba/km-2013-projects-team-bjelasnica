using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Check Login
                if (HttpContext.Current.User.Identity.Name != "")
                {
                    profile.Visible = true;
                    notification.Visible = true;
                    login.Visible = false;
                    if(DAKorisnici.IsAdmin(HttpContext.Current.User.Identity.Name))
                        adminlink.Visible = true;
                }

                //Notification
                BindNotification();

                //User image
                LoadUserImage();

                //Count all wiki, questions, users, tags
                BindCountAll();

            }
        }

        private void BindCountAll()
        {
            int totalWiki = DAClanci.Count();
            labelWiki.Text = totalWiki.ToString();

            int totalQuestions = DAPitanja.Count();
            labelQuestions.Text = totalQuestions.ToString();

            int totalTags = DATagovi.Count();
            labelTags.Text = totalTags.ToString();

            int totalUsers = DAKorisnici.Count();
            labelUsers.Text = totalUsers.ToString();

            labelThemes.Text = DATeme.Count().ToString();
        }

        private void LoadUserImage()
        {
            if (HttpContext.Current.User.Identity.Name != "")
            {
                Korisnici user = DAKorisnici.GetByID(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                if (user.SlikaType != null && user.SlikaType != "")
                    userImage.ImageUrl = "/Users/ImageHandler.ashx?userId=" + user.KorisnikID;
                else
                    userImage.ImageUrl = "/Content/img/profile.png";
            }
        }

        private void BindNotification()
        {
            if (HttpContext.Current.User.Identity.Name != "")
            {
                DateTime lastLogin = Convert.ToDateTime(Session["LastLogin"]);
                if (lastLogin == Convert.ToDateTime("01.01.0001 00:00"))
                    lastLogin = Convert.ToDateTime("01.01.2013 00:00");
                int newArticles = DAKorisnici.CountNewArticles(lastLogin);
                int newQuestions = DAKorisnici.CountNewQuestions(lastLogin);

                labelWikiNotification.Text = newArticles.ToString();
                labelQuestionsNotification.Text = newQuestions.ToString();
            }
        }
    }
}