using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FITKMS_business.Data;
namespace FITKMS.QA
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsCallback && !IsPostBack)
            {
                if (Request.QueryString != null)
                {
                    if (Request.QueryString["id"] != null)
                    {

                        int id = Int32.Parse(Request.QueryString["id"].ToString());

                        Pitanja pitanje = QAService.getPitanjeByID(id);

                        if (pitanje != null)
                        {
                            Session.Add("PitanjeID", pitanje.PitanjeID);
                            lblNaslovPitanja.Text = pitanje.Naslov;
                            lblTextPitanja.Text = pitanje.Tekst;
                            lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                            lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();
                            lblBrojPregleda.Text = pitanje.BrojPregleda.ToString();
                            Korisnici korinik = QAService.GetKorisnikByID(pitanje.KorisnikID);
                            lblKorisnik.Text = "by " + korinik.Ime + " " + korinik.Prezime;
                            lblDatum.Text = pitanje.DatumKreiranja.ToShortDateString();

                            dlListaTagova.DataSource = QAService.getListaTagovaUpitanju(pitanje.PitanjeID);
                            dlListaTagova.DataBind();

                            dtOdgovori.DataSource = QAService.getAllOdgovoriZaPitanje(pitanje.PitanjeID);
                            dtOdgovori.DataBind();

                            //// broje pregleda na stranici
                            QAService.SetBrojPregleda(pitanje);

                        }
                        else
                    Response.Redirect("AllQuestions.aspx");

                    }
                    else
                        Response.Redirect("AllQuestions.aspx");




                }
                
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            if ( wysiwyg.Text !="")
            {

                int id = Int32.Parse(Request.QueryString["id"].ToString());

                int pitanjeID = (int)Session["PitanjeID"];

                    Odgovori odg = new Odgovori();
                    odg.PitanjeID = pitanjeID;
                    odg.Tekst = wysiwyg.Text;
                    odg.KorisnikID = 1; ///// uzeti iz sesije, ovo ne valja
                    odg.Pozitivni = 0;
                    odg.Negativni = 0;
                    odg.DatumKreiranja = DateTime.Now;
                    odg.DatumIzmjene = DateTime.Now;

                    QAService.saveOdgovor(odg);

                    wysiwyg.Text = "";

                    dtOdgovori.DataSource = null;
                    dtOdgovori.DataSource = QAService.getAllOdgovoriZaPitanje(pitanjeID);
                    dtOdgovori.DataBind();
            }
             }

        protected void likeButton_Click(object sender, EventArgs e)
        {
            int pitanjeID = (int)Session["PitanjeID"];
                
            LinkButton btn = (LinkButton)(sender);

           ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn); // aktivira postback za linkbutton
            
            
            int IdOdgovora = int.Parse(btn.CommandArgument);



                int korisnikID = 1; //uzeti iz sesije korisnika

                bool glasao = QAService.Je_LiGlasao(korisnikID, IdOdgovora);

                if (glasao == false)
                {

                    Odgovori odg = QAService.getOdgovorByID(IdOdgovora);
                    QAService.UpdateOdgovorP(odg, korisnikID);

                    dtOdgovori.DataSource = null;
                    dtOdgovori.DataSource = QAService.getAllOdgovoriZaPitanje(pitanjeID);
                    dtOdgovori.DataBind();



                }          
        }

        protected void dislikeButton_Click(object sender, EventArgs e)
        {
            int pitanjeID = (int)Session["PitanjeID"];

            LinkButton btn = (LinkButton)(sender);
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

            int IdOdgovora = int.Parse(btn.CommandArgument);

            int korisnikID = 1; //uzeti iz sesije korisnika

            bool glasao = QAService.Je_LiGlasao(korisnikID, IdOdgovora);

            if (glasao == false)
            {

                Odgovori odg = QAService.getOdgovorByID(IdOdgovora);
                QAService.UpdateOdgovorN(odg, korisnikID);

                dtOdgovori.DataSource = null;
                dtOdgovori.DataSource = QAService.getAllOdgovoriZaPitanje(pitanjeID);
                dtOdgovori.DataBind();
            }          


        }

        protected void likePitanje_Click(object sender, EventArgs e)
        {
            int pitanjeID = (int) Session["PitanjeID"];
            LinkButton btn = (LinkButton)(sender);
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

            int korisnikID = 1; //uzeti iz sesije korisnika

            bool glasao = QAService.Je_LiGlasaoZaPitanje(korisnikID, pitanjeID);

            if (glasao == false)
            {

                Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);

                QAService.UpdatePitanjeLike(pitanje, korisnikID);

                lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();

            
            }



        }

        protected void dislikePitanje_Click(object sender, EventArgs e)
        {
            int pitanjeID = (int)Session["PitanjeID"];
            LinkButton btn = (LinkButton)(sender);
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(btn);

            int korisnikID = 1; //uzeti iz sesije korisnika

            bool glasao = QAService.Je_LiGlasaoZaPitanje(korisnikID, pitanjeID);

            if (glasao == false)
            {

                Pitanja pitanje = QAService.getPitanjeByID(pitanjeID);

                QAService.UpdatePitanjeDislike(pitanje, korisnikID);

                lblBrojNegativnih.Text = pitanje.Negativni.ToString();
                lblBrojPozitnivh.Text = pitanje.Pozitivni.ToString();

            }


        }

   




            










        

    }
}
