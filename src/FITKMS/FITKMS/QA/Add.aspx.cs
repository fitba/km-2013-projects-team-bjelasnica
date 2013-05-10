using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.QA
{
    public partial class Add : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
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

                List<string> temaEmpty = new List<string>();
                temaEmpty.Add("Odaberite temu");
                ddTema.DataSource = temaEmpty;
                ddTema.DataBind();
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

            Teme empty = new Teme();
            empty.Naziv = "Odaberite temu";
            empty.TemaID = 0;
            teme.Insert(0, empty);
            ddTema.DataSource = teme;
            ddTema.DataTextField = "Naziv";
            ddTema.DataValueField = "TemaID";
            ddTema.DataBind();
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                Pitanja pitanje = new Pitanja();
                pitanje.Naslov = txtNaslovPitanja.Text;
                pitanje.Tekst = wysiwyg.Text;
                pitanje.KorisnikID = Convert.ToInt32(User.Identity.Name);
                if (ddTema.SelectedValue == "Odaberite temu")
                {
                    error_label.Visible = true;
                    errorLabel.Text = "Obavezno odabrati temu!";
                    return;
                }
                else
                    pitanje.TemaID = int.Parse(ddTema.SelectedValue);
                pitanje.Pozitivni = 0;
                pitanje.Negativni = 0;
                pitanje.BrojPregleda = 0;
                pitanje.Status = true;
                pitanje.DatumIzmjene = DateTime.Now;
                pitanje.DatumKreiranja = DateTime.Now;

                List<string> tags = new List<string>();

                foreach (string tag in tagsInput.Text.Split(','))
                {
                    if (tag != "")
                        tags.Add(tag.Trim());
                }

                QAService.savePitanje(pitanje, tags);

                ClearFields();

                success_label.Visible = true;
                successLabel.Text = "Uspješno ste dodali pitanje.";
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