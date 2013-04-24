using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;

namespace FITKMS.Users
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindForm();
            }
        }

        private void BindForm()
        {
            Korisnici user = DAKorisnici.GetByID(Convert.ToInt32(User.Identity.Name));

            if (user.SlikaType != null && user.SlikaType != "")
                userImage.ImageUrl = "Image.aspx?username=" + user.KorisnickoIme;
            else
                userImage.ImageUrl = "../Content/img/profile.png";

            usernameInput.Text = user.KorisnickoIme;
            fnameInput.Text = user.Ime;
            lnameInput.Text = user.Prezime;
            mailInput.Text = user.Mail;
            genderList.SelectedValue = user.Spol;
            string day = user.DatumRodjenja.Day.ToString();
            string month = user.DatumRodjenja.Month.ToString();
            string year = user.DatumRodjenja.Year.ToString();
            dayList.SelectedValue = day;
            monthList.SelectedValue = month;
            yearList.SelectedValue = year;
        }

        protected void saveSubmit_Click(object sender, EventArgs e)
        {
            Korisnici user = new Korisnici();
            user = DAKorisnici.GetByID(Convert.ToInt32(User.Identity.Name));
            user.Ime = fnameInput.Text;
            user.Prezime = lnameInput.Text;
            user.Mail = mailInput.Text;
            user.Spol = genderList.SelectedValue;
            try
            {
                user.DatumRodjenja = Convert.ToDateTime(dayList.Text + '.' + monthList.Text + '.' + yearList.Text);
            }
            catch 
            {
                error_label.Visible = true;
                errorLabel.Text = "Obavezno odabrati datum rođenja!";
                return;
            }
            if (imageFile.PostedFile != null && imageFile.PostedFile.FileName != "")
            {
                user.SlikaType = imageFile.PostedFile.ContentType;
                byte[] content = new byte[imageFile.PostedFile.ContentLength + 1];
                imageFile.PostedFile.InputStream.Read(content, 0, imageFile.PostedFile.ContentLength);

                MemoryStream ms = new MemoryStream(content);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                MemoryStream newStream = new MemoryStream();

                resizeBitmap((Bitmap)img, 415, 300).Save(newStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                user.Slika = newStream.ToArray();
            }

            DAKorisnici.Update(user);
            success_label.Visible = true;
            successLabel.Text = "Uspješno ste sačuvali promjene.";

        }

        public static Bitmap resizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);
            g.DrawImage(b, 0, 0, nWidth, nHeight);

            return result;
        }
    }
}