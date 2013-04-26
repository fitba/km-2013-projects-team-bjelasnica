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
                ddOblast.DataSource = QAService.getAllOblasti();
                ddOblast.DataTextField = "Naziv";
                ddOblast.DataValueField = "OblastID";
                ddOblast.DataBind();

                FillTeme(2);//////////////// nije dobro

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
            ddTema.DataSource = QAService.getAllTemeByID(id);
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
            Pitanja pitanje = new Pitanja();

            pitanje.Naslov = txtNaslovPitanja.Text;
            pitanje.Tekst = wysiwyg.Text;
            pitanje.KorisnikID = 1;//////////////uzeti ovo iz sesije
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




        }
    }
}