using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;

namespace FITKMS.Users
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void createSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Korisnici user = new Korisnici();
                user.Ime = fnameInput.Text.Trim();
                user.Prezime = lnameInput.Text.Trim();
                user.Mail = mailInput.Text.Trim();
                user.Spol = genderList.Text;
                try
                {
                    string birthDate = dayList.Text + '.' + monthList.Text + '.' + yearList.Text;
                    user.DatumRodjenja = Convert.ToDateTime(birthDate);
                }
                catch
                {
                    error_label.Visible = true;
                    errorLabel.Text = "Obavezno odabrati datum rođenja!";
                    return;
                }

                if (password1Input.Text != password2Input.Text)
                {
                    error_label.Visible = true;
                    errorLabel.Text = "Neispravna potvrda lozinke!";
                    return;
                }

                user.KorisnickoIme = usernameInput.Text;
                user.LozinkaSalt = PasswordHash.GenerateSalt();
                user.LozinkaHash = PasswordHash.EncodePassword(password1Input.Text, user.LozinkaSalt);

                DAKorisnici.Insert(user);
                ClearFields();
                success_label.Visible = true;
                successLabel.Text = "Uspješno ste izvršili proces registracije.";
            }
            catch 
            {
                error_label.Visible = true;
                errorLabel.Text = "Greška prilikom pohrane podataka!";
                return;
            }
        }

        private void ClearFields()
        {
            fnameInput.Text = "";
            lnameInput.Text = "";
            mailInput.Text = "";
            genderList.SelectedIndex = 0;
            dayList.SelectedIndex = 0;
            monthList.SelectedIndex = 0;
            yearList.SelectedIndex = 0;
            usernameInput.Text = "";
            password1Input.Text = "";
            password2Input.Text = "";
        }

        protected void cancelSubmit_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Default.aspx");
        }
    }
}