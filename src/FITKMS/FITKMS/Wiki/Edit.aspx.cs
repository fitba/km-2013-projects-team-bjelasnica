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
        protected List<Tagovi> tags;
        protected List<Teme> themes;
        protected List<VrsteClanaka> types;

        private Clanci article;

        protected List<Tagovi> selectedTags
        {
            get { return (List<Tagovi>)Session["selectedTags"]; }
            set { Session["selectedTags"] = value; }
        }

        protected Int32 articleId
        {
            get { return (Int32)ViewState["articleId"]; }
            set { ViewState["articleId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTags();
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

        private void BindTags()
        {
            tags = DATagovi.SelectAll();
            tagsList.DataBind();
        }

        private void BindArticle()
        {
            if (Request["articleId"] != null)
            {
                articleId = Convert.ToInt32(Request["articleId"]);
                article = DAClanci.Select(articleId);
                selectedTags = DAClanci.SelectTags(article.ClanakID);

                foreach (ListItem item in tagsList.Items)
                {
                    foreach (Tagovi tag in selectedTags)
                    {
                        if (item.Value == tag.TagID.ToString())
                        {
                            item.Selected = true;
                            tagsInput.Text += item.Text + " ";
                            break;
                        }
                    }
                }

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
                if (documentFile.PostedFile == null)
                    article = DAClanci.Select(articleId);
                else if (documentFile.PostedFile != null && documentFile.PostedFile.FileName != null
                          && documentFile.PostedFile.FileName != "")
                {
                    string extension = "";
                    article = new Clanci();
                    article.ClanakID = articleId;

                    extension = System.IO.Path.GetExtension(documentFile.PostedFile.FileName);
                    if (extension == ".pdf" || extension == ".doc" || extension == ".docx")
                    {
                        article.Dokument = new byte[documentFile.PostedFile.ContentLength];
                        article.Dokument = new byte[documentFile.PostedFile.ContentLength];
                        documentFile.PostedFile.InputStream.Read(article.Dokument, 0, documentFile.PostedFile.ContentLength);
                        string filename = article.ClanakID + extension;
                        System.IO.File.WriteAllBytes(Server.MapPath("/Content/articles/") + filename, article.Dokument);
                        article.DokumentPath = "~/Content/articles/" + filename;
                    }
                    else
                    {
                        errorLabel.Text = "Format dokumenta nije podržan.";
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

                DAClanci.Update(article, selectedTags);

                successLabel.Text = "Uspješno ste sačuvali izmjene.";
                error_label.Visible = false;
                success_label.Visible = true;
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                error_label.Visible = true;
            }
        }

        protected void documentDeleteSubmit_Click(object sender, EventArgs e)
        {
            article = DAClanci.Select(articleId);
          
            string[] filename = article.DokumentPath.Split('/');
            System.IO.File.Delete(Server.MapPath("/Content/articles/") + filename);

            article.Dokument = null;
            article.DokumentType = null;
            article.DokumentPath = null;
            DAClanci.Update(article, selectedTags);

            successLabel.Text = "Uspješno ste uklonili dokument.";
            error_label.Visible = false;
            success_label.Visible = true;
        }
    }
}