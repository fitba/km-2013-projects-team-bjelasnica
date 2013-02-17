using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FITWiki.Models;
using System.Globalization;
using System.Data.Metadata.Edm;
namespace FITWiki.Controllers
{
    public class QAController : Controller
    {
        private FITWikiContext db = new FITWikiContext();
       
        int IdClanka;

        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetVersions(int OS)
        {
            if (OS != -1)
            {
                string SP = string.Format("EXEC dbo.sp_GetAllTemeZaOblast '{0}'", OS);
                List<Teme> ListaTema = db.Database.SqlQuery<Teme>(SP).ToList();
                ViewBag.ListaTema = ListaTema;
            }
     

            return View();
        }


        public ActionResult CreateQuestion()
        {


            string SP = string.Format("EXEC dbo.sp_GetAllOblasti");
            List<Oblasti> ListaOblasti = db.Database.SqlQuery<Oblasti>(SP).ToList();

            ViewBag.ListaOblasti = ListaOblasti;



            return View();
        }


        [HttpPost]
        public ActionResult CreateQuestion(string txtPitanja,string temaID)
        {
            if (ModelState.IsValid)
            {
                Pitanja pitanja = new Pitanja();
                pitanja.TemaID = int.Parse(temaID);
                pitanja.Tekst = txtPitanja;
                pitanja.KorisnikID = 4; // iz sesije ili cokie-a pokupiti, samo radi testiranja
                pitanja.DatumIzmjene = DateTime.Now;
                pitanja.DatumKreiranja = DateTime.Now;
                pitanja.Status = true;
                pitanja.Pozitivni = 0;
                pitanja.Negativni = 0;
                db.Pitanjas.Add(pitanja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Clanci()
        {


            return View(db.Clancis.ToList());

        }

        //get
        public ActionResult CreateQuestionFor(int id = 0)
        {
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {

                return HttpNotFound();
            }
            else
            {
                IdClanka = id;

                VrsteClanaka vrsta = db.VrsteClanakas.Find(clanci.VrstaID);
                ViewBag.VrstaNaslov = vrsta.Naziv;
                Teme tema = db.Temes.Find(clanci.TemaID);
                ViewBag.TemaNaslov = tema.Naziv;
                Korisnici korisnik = db.Korisnicis.Find(clanci.KorisnikID);
                ViewBag.Korisnik = korisnik.Ime + " " + korisnik.Prezime;

                string SP = string.Format("EXEC sp_GetAllQuestionsForClanak '{0}'", id);
               List<PitanjaKorisnici> listaPitanja = db.Database.SqlQuery<PitanjaKorisnici>(SP).ToList();
                
          //  string SP = string.Format("EXEC dbo.sp_GetImeIprezime");
             //   List<suda> test = db.Database.SqlQuery<suda>(SP).ToList();

                ViewBag.ListaSvihPitanjaZaClanak = listaPitanja; 


                return View(clanci);
            }

        }
        //public ActionResult InsertQuestionFor(FormCollection c)
        //{
        //    c["UserID"].ToString()
        public ActionResult InsertQuestionFor(string pitanje, int? TemaID, int? ClanakID)
        {
            if (TemaID.HasValue && ClanakID.HasValue)
            {
                
                    string SP = string.Format("EXEC dbo.InsertPitanjeForQuestion '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'", pitanje, 4, ClanakID, TemaID, DateTime.Now.ToString("s"), DateTime.Now.ToString("s"), 0, 0);
                    db.Database.ExecuteSqlCommand(SP);

                
               

            }
            else
                return RedirectToAction("Clanci", "QA");//ako su prazni temaID i clanakID
            return RedirectToAction("Clanci", "QA");
        }











    }
}
