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
            BindForm();
        }

        private void BindForm()
        {
            Korisnici user = DAKorisnici.GetByUsername(User.Identity.Name);

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
            byte[] image = new byte["".Length];
            string ImageType = "";
            if (imageFile.PostedFile != null && imageFile.PostedFile.FileName != "")
            {
                ImageType = imageFile.PostedFile.ContentType;
                byte[] content = new byte[imageFile.PostedFile.ContentLength + 1];
                imageFile.PostedFile.InputStream.Read(content, 0, imageFile.PostedFile.ContentLength);

                MemoryStream ms = new MemoryStream(content);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);

                MemoryStream newStream = new MemoryStream();

                resizeBitmap((Bitmap)img, 415, 300).Save(newStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                image = newStream.ToArray();
            }

            DAKorisnici.Update(User.Identity.Name,
                               fnameInput.Text,
                               lnameInput.Text,
                               mailInput.Text,
                               genderList.SelectedValue,
                               Convert.ToDateTime(dayList.Text + '.' + monthList.Text + '.' + yearList.Text),
                               image,
                               ImageType);

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