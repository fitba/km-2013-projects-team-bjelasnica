using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            usernameInput.Focus();
        }

        protected void loginSubmit_Click(object sender, EventArgs e)
        {
            error_label.Visible = false;
            Korisnici user = DAKorisnici.GetByUsername(usernameInput.Text);

            if (user != null)
            {
                if (PasswordHash.EncodePassword(passwordInput.Text, user.LozinkaSalt) == user.LozinkaHash)
                {
                    if (!user.Aktivan)
                    {
                        error_label.Visible = true;
                        errorLabel.Text = "Vaš korisnički račun nije aktivan!";
                        return;
                    }

                    FormsAuthenticationTicket tick = new FormsAuthenticationTicket(user.KorisnikID.ToString(), false, 30);
                    string encTick = FormsAuthentication.Encrypt(tick);

                    Session["LastLogin"] = user.PosljednjaPrijava;
                    DAKorisnici.UpdateLastLogin(user.KorisnikID, DateTime.Now);

                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTick));
                    Response.Redirect(FormsAuthentication.GetRedirectUrl(user.KorisnikID.ToString(), false));
                }
                else
                {
                    error_label.Visible = true;
                    errorLabel.Text = "Neispravni korisnički podaci!";
                    ClearFields();
                }
            }
            else
            {
                error_label.Visible = true;
                errorLabel.Text = "Neispravni korisnički podaci!";
                ClearFields();
            }
        }

        private void ClearFields()
        {
            usernameInput.Text = "";
            passwordInput.Text = "";
        }
    }
}