using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Wiki
{
    public partial class Add : System.Web.UI.Page
    {
        protected List<Tagovi> tags;
        protected List<Teme> themes;
        protected List<VrsteClanaka> types;

        protected List<Tagovi> selectedTags 
        {
            get { return (List<Tagovi>)Session["selectedTags"]; }
            set { Session["selectedTags"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTags();
                BindTypes();
                BindThemes();
                selectedTags = new List<Tagovi>();
            }
        }

        private void BindThemes()
        {
            themes= DATeme.Select(true);
            themeList.DataBind();

        }

        private void BindTags()
        {
            tags = DATagovi.SelectAll();
            tagsList.DataBind();
     
        }

        private void BindTypes()
        {
            types = DAClanci.SelectTypes(true);
            typesList.DataBind();
        }

        protected void saveArticleSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Clanci article = new Clanci();
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

                string extension = "";
                if (documentFile.PostedFile != null && documentFile.PostedFile.FileName != null
                          && documentFile.PostedFile.FileName != "")
                {
                    extension = System.IO.Path.GetExtension(documentFile.PostedFile.FileName);
                    if (extension == ".pdf")
                    {
                        article.DokumentType = documentFile.PostedFile.ContentType;
                        article.Dokument = new byte[documentFile.PostedFile.ContentLength];
                        documentFile.PostedFile.InputStream.Read(article.Dokument, 0, documentFile.PostedFile.ContentLength);
                    }
                    else
                    {
                        errorLabel.Text = "Format dokumenta nije podržan.";
                        error_label.Visible = true;
                        return;
                    }
                }

                DAClanci.Insert(article, selectedTags);

                if (article.Dokument != null)
                {
                    string filename = article.ClanakID + extension;
                    System.IO.File.WriteAllBytes(Server.MapPath("/Content/articles/") + filename, article.Dokument);
                    article.DokumentPath = "~/Content/articles/" + filename;
                }

                successLabel.Text = "Uspješno ste dodali članak.";
                error_label.Visible = false;
                success_label.Visible = true;
                clearFields();
            }
            catch (Exception ex)
            {
                errorLabel.Text = ex.Message;
                error_label.Visible = true;
            }
        }

        private void clearFields()
        {
            typesList.SelectedIndex = 0;
            themeList.SelectedIndex = 0;
            titleInput.Text = "";
            authorsInput.Text = "";
            keyWordsInput.Text = "";
            wysiwyg.InnerText = "";
            tagsInput.Text = "";
            selectedTags.Clear();
            tagsList.ClearSelection();
        }

        protected void saveTagsSubmit_Click(object sender, EventArgs e)
        {
            selectedTags.Clear();
            foreach (ListItem item in tagsList.Items)
            {
                if (item.Selected)
                {
                    Tagovi tag = new Tagovi();
                    tag.Naziv = item.Text;
                    tag.TagID = Convert.ToInt32(item.Value);
                    selectedTags.Add(tag);
                    tagsInput.Text += item.Text + " ";
                }
            }
        }

        protected void loadTagsSubmit_Click(object sender, EventArgs e)
        {
            foreach (ListItem item in tagsList.Items)
            {
                item.Selected = false;
                foreach (Tagovi tag in selectedTags)
                {
                    if (item.Value == tag.TagID.ToString())
                    {
                        item.Selected = true;
                        break;
                    }
                }
            }
        }
    }
}