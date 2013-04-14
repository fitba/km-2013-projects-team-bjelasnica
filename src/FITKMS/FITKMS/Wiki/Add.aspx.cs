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
        protected List<VrsteClanaka> types;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindTags();
                BindTypes();
            }
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
            Clanci article = new Clanci();
            if (typesList.SelectedIndex != 0)
                article.VrstaID = Convert.ToInt32(typesList.SelectedValue);
            article.Naslov = titleInput.Text.Trim();
            article.Autori = authorsInput.Text.Trim();
            article.KljucneRijeci = keyWordsInput.Text.Trim();

            List<Int32> selectedTags = new List<Int32>();
            foreach (ListItem item in tagsList.Items)
            {
                if (item.Selected)
                    selectedTags.Add(Convert.ToInt32(item.Value));
                
            }

            article.Tekst = wysiwyg.InnerText;
            DAClanci.Insert(article, selectedTags);
        }
    }
}