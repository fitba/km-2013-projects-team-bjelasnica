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

                chkTagovi.DataSource = QAService.getAllTagovi();
                chkTagovi.DataTextField = "Naziv";
                chkTagovi.DataValueField = "TagID";
                chkTagovi.DataBind();
            }
        }

        protected void ddOblast_SelectedIndexChanged(object sender, EventArgs e)
        {
            int OblastID = int.Parse(ddOblast.SelectedValue);
            if (OblastID != 0)
                FillTeme(OblastID);
        }

        public void FillTeme(int id)
        {
            List<Teme> teme = QAService.getAllTemeByID(id);
            Teme empty = new Teme();
            empty.Naziv = "Odaberite temu";
            empty.TemaID = 0;
            teme.Insert(0, empty);
            ddTema.DataSource = teme;
            ddTema.DataTextField = "Naziv";
            ddTema.DataValueField = "TemaID";
            ddTema.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Tagovi> ListaOznacenihTagova = new List<Tagovi>();
            Tagovi tag;

            foreach (ListItem i in chkTagovi.Items)
            {
                if (i.Selected)
                {
                    int id = Int32.Parse(i.Value);
                    tag = QAService.getTagByID(id);
                    ListaOznacenihTagova.Add(tag);
                }
            }
            Session.Add("ListaOznacenihTagova", ListaOznacenihTagova);
        }

        protected void Save_Click(object sender, EventArgs e)
        {
            try
            {
                Pitanja pitanje = new Pitanja();
                pitanje.Naslov = txtNaslovPitanja.Text;
                pitanje.Tekst = wysiwyg.Text;
                pitanje.KorisnikID = 4;//////////////uzeti ovo iz sesije
                pitanje.TemaID = int.Parse(ddTema.SelectedValue);
                pitanje.Pozitivni = 0;
                pitanje.Negativni = 0;
                pitanje.BrojPregleda = 0;
                pitanje.Status = true;
                pitanje.DatumIzmjene = DateTime.Now;
                pitanje.DatumKreiranja = DateTime.Now;

                List<Tagovi> ListaTagova = (List<Tagovi>)Session["ListaOznacenihTagova"];
                QAService.savePitanje(pitanje, ListaTagova);

                Session.Remove("ListaOznacenihTagova");
                txtNaslovPitanja.Text = "";
                wysiwyg.Text = "";
                success_label.Visible = true;
                successLabel.Text = "Uspješno ste postavili pitanje.";
            }
            catch 
            {
                error_label.Visible = true;
                errorLabel.Text = "Greška prilikom pohrane podataka!";
                return;
            
            }
        }
    }
}