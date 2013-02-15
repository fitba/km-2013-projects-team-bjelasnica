using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FITWiki.Models;
using System.Globalization;
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

        public ActionResult CreateQuestion()
        {
            List<Teme> ListaTema = db.Temes.ToList();

            ViewBag.ListaTema = ListaTema;

            return View();
        }


        [HttpPost]
        public ActionResult CreateQuestion(Pitanja pitanja)
        {
            if (ModelState.IsValid)
            {

                pitanja.KorisnikID = 2; // iz sesije ili cokie-a pokupiti, samo radi testiranja
                pitanja.DatumIzmjene = DateTime.Now;
                pitanja.DatumKreiranja = DateTime.Now;
                pitanja.Status = true;
                pitanja.Pozitivni = 0;
                pitanja.Negativni = 0;
                db.Pitanjas.Add(pitanja);
                db.SaveChanges();
                return RedirectToAction("Index");
            }



            ViewBag.ClanakID = new SelectList(db.Clancis, "ClanakID", "Naslov", pitanja.ClanakID);
            ViewBag.KorisnikID = new SelectList(db.Korisnicis, "KorisnikID", "Ime", pitanja.KorisnikID);
            ViewBag.TemaID = new SelectList(db.Temes, "TemaID", "Naziv", pitanja.TemaID);
            return View(pitanja);
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
                ViewBag.TemaNaslov = vrsta.Naziv;
                Korisnici korisnik = db.Korisnicis.Find(clanci.KorisnikID);
                ViewBag.Korisnik = korisnik.Ime + " " + korisnik.Prezime;

                string SP = string.Format("EXEC sp_GetAllQuestionsForClanak '{0}'", id);
                  List<Pitanja> listaPitanja = db.Database.SqlQuery<Pitanja>(SP).ToList();


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
