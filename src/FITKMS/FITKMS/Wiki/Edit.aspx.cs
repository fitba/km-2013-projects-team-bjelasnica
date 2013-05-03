using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Wiki
{
    public partial class Edit : System.Web.UI.Page
    {
        protected List<Teme> themes;
        protected List<VrsteClanaka> types;

        private Clanci article;

        protected Int32 articleId
        {
            get { return (Int32)ViewState["articleId"]; }
            set { ViewState["articleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTypes();
                BindThemes();
                BindArticle();
            }
        }

        private void BindThemes()
        {
            themes = DATeme.Select(true);
            themeList.DataBind();

        }

        private void BindTypes()
        {
            types = DAClanci.SelectTypes(true);
            typesList.DataBind();
        }

        private void BindArticle()
        {
            if (Request["articleId"] != null)
            {
                articleId = Convert.ToInt32(Request["articleId"]);
                article = DAClanci.Select(articleId);
                List<Tagovi> tags = DAClanci.SelectTags(article.ClanakID);

                foreach (Tagovi tag in tags)
                    tagsInput.Text += tag.Naziv + ", ";

                typesList.SelectedValue = article.VrstaID.ToString();
                themeList.SelectedValue = article.TemaID.ToString();
                titleInput.Text = article.Naslov;
                authorsInput.Text = article.Autori;
                keyWordsInput.Text = article.KljucneRijeci;
                wysiwyg.InnerText = article.Tekst;
            }
        }

        protected void saveArticleSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (documentFile.PostedFile.ContentLength == 0)
                    article = DAClanci.Select(articleId);
                else if (documentFile.PostedFile != null && documentFile.PostedFile.FileName != null
                          && documentFile.PostedFile.FileName != "")
                {
                    string extension = "";
                    article = new Clanci();
                    article.ClanakID = articleId;

                    extension = System.IO.Path.GetExtension(documentFile.PostedFile.FileName);
                    if (extension == ".pdf")
                    {
                        article.DokumentType = documentFile.PostedFile.ContentType;
                        article.Dokument = new byte[documentFile.PostedFile.ContentLength];
                        documentFile.PostedFile.InputStream.Read(article.Dokument, 0, documentFile.PostedFile.ContentLength);
                        string filename = article.ClanakID + extension;
                        System.IO.File.WriteAllBytes(Server.MapPath("/Content/articles/") + filename, article.Dokument);
                        article.DokumentPath = "~/Content/articles/" + filename;
                    }
                    else
                    {
                        errorLabel.Text = "Podržani format dokumenta je pdf.";
                        error_label.Visible = true;
                        return;
                    }
                }

                if (typesList.SelectedIndex != 0)
                    article.VrstaID = Convert.ToInt32(typesList.SelectedValue);
                if (themeList.SelectedIndex != 0)
                    article.TemaID = Convert.ToInt32(themeList.SelectedValue);

                article.Naslov = titleInput.Text.Trim();
                article.Autori = authorsInput.Text.Trim();
                article.KljucneRijeci = keyWordsInput.Text.Trim();
                article.DatumKreiranja = DateTime.Now;
                article.DatumIzmjene = DateTime.Now;
                article.Status = true;
                article.KorisnikID = Convert.ToInt32(User.Identity.Name);
                article.Tekst = wysiwyg.InnerText;

                List<string> tags = new List<string>();

                foreach (string tag in tagsInput.Text.Split(','))
                {
                    if (tag.Trim() != "")
                        tags.Add(tag.Trim());
                }

                DAClanci.Update(article, tags);

                //Praćenje izmjena članka
                ClanciIzmjene articleChange = new ClanciIzmjene();
                articleChange.KorisnikID = Convert.ToInt32(User.Identity.Name);
                articleChange.ClanakID = articleId;
                articleChange.Datum = DateTime.Now;
                articleChange.Opis = editDescriptionInput.InnerText;

                DAClanci.TrackChange(articleChange);

                successLabel.Text = "Uspješno ste sačuvali izmjene.";
                error_label.Visible = false;
                success_label.Visible = true;
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                if (ex.InnerException != null)
                    errorLabel.Text += "\n" + ex.InnerException;
                error_label.Visible = true;
            }
        }

        protected void documentDeleteSubmit_Click(object sender, EventArgs e)
        {
            article = DAClanci.Select(articleId);

            if (article.DokumentPath != null && article.Dokument != null)
            {
                string[] filename = article.DokumentPath.Split('/');
                System.IO.File.Delete(Server.MapPath("/Content/articles/") + filename);

                DAClanci.DeleteDocument(articleId);

                successLabel.Text = "Dokument uspješno uklonjen.";
                error_label.Visible = false;
                success_label.Visible = true;
            }
            else
            {
                warningLabel.Text = "Ne postoji dokument.";
                warning_label.Visible = true;
            }
        }

        #region WebMethod
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethod()]
        public static string[] GetTagNames(string prefixText, int count)
        {
            List<Tagovi> tags = DATagovi.SelectByName(prefixText);
            List<string> tagNames = new List<string>();

            foreach (Tagovi tag in tags)
            {
                string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem
                    (tag.Naziv, tag.TagID.ToString());
                tagNames.Add(item);

            }

            return tagNames.ToArray();

        }
        #endregion
    }
}