using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.QA
{
    public partial class Edit : System.Web.UI.Page
    {
        private Pitanja question;

        protected Int32 questionId
        {
            get { return (Int32)ViewState["questionId"]; }
            set { ViewState["questionId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Oblasti> oblasti = QAService.getAllOblasti();
                Oblasti empty = new Oblasti();
                empty.Naziv = "Odaberite oblast";
                empty.OblastID = 0;
                oblasti.Insert(0, empty);
                ddOblast.DataSource = oblasti;
                ddOblast.DataTextField = "Naziv";
                ddOblast.DataValueField = "OblastID";
                ddOblast.DataBind();

                ddOblast_SelectedIndexChanged(null, null);
                BindQuestion();
            }
        }

        private void BindQuestion()
        {
            if (Request["id"] != null)
            {
                questionId = Convert.ToInt32(Request["id"]);
                question = QAService.getPitanjeByID(questionId);
                List<Tagovi> tags = DAPitanja.SelectTags(questionId);

                foreach (Tagovi tag in tags)
                    tagsInput.Text += tag.Naziv + ", ";

                ddTema.SelectedValue = question.TemaID.ToString();
                Teme tema = new Teme();
                if(question.TemaID != null)
                    tema = DATeme.SelectTemaByID((int)question.TemaID);
                if (tema.OblastID != null)
                    ddOblast.SelectedValue = tema.OblastID.ToString();
                txtNaslovPitanja.Text = question.Naslov;
                wysiwyg.Text = question.Tekst;
            }
        }

        protected void ddOblast_SelectedIndexChanged(object sender, EventArgs e)
        {
            int OblastID = int.Parse(ddOblast.SelectedValue);
            if (OblastID != 0)
                FillTeme(OblastID);
            else
            {
                FillTeme(0);
            }
        }

        public void FillTeme(int id)
        {
            List<Teme> teme = new List<Teme>();

            if (id != 0)
                teme = QAService.getAllTemeByID(id);
            else
                teme = DATeme.Select(true);

            ddTema.DataSource = teme;
            ddTema.DataTextField = "Naziv";
            ddTema.DataValueField = "TemaID";
            ddTema.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                question = new Pitanja();
                question.PitanjeID = Convert.ToInt32(Request["id"]);
                question.Naslov = txtNaslovPitanja.Text;
                question.Tekst = wysiwyg.Text;
                question.KorisnikID = Convert.ToInt32(User.Identity.Name);
                question.DatumIzmjene = DateTime.Now;

                if (ddTema.SelectedIndex != 0)
                    question.TemaID = Convert.ToInt32(ddTema.SelectedValue);
                else
                {
                    error_label.Visible = true;
                    errorLabel.Text = "Obavezno odabrati temu!";
                    return;
                }

                List<string> tags = new List<string>();

                foreach (string tag in tagsInput.Text.Split(','))
                {
                    if (tag.Trim() != "")
                        tags.Add(tag.Trim());
                }

                DAPitanja.Update(question, tags);

                ClearFields();

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

        private void ClearFields()
        {
            ddOblast.SelectedIndex = 0;
            ddTema.SelectedIndex = 0;
            txtNaslovPitanja.Text = "";
            wysiwyg.Text = "";
            tagsInput.Text = "";
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