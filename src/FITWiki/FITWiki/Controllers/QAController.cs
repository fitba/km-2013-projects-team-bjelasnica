using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FITWiki.Models;

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
                ViewBag.ClanakID = id;
                Teme tema = db.Temes.Find(clanci.TemaID);
                ViewBag.TemaNaslov = vrsta.Naziv;
                ViewBag.TemaID = tema.TemaID;
                Korisnici korisnik = db.Korisnicis.Find(clanci.KorisnikID);
                ViewBag.Korisnik = korisnik.Ime + " " + korisnik.Prezime;





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
                Pitanja _Pitanje = new Pitanja();
                _Pitanje.Tekst = pitanje;
                _Pitanje.KorisnikID = 2;//korisnik.KorisnikID uzeti iz sesije;
                _Pitanje.ClanakID = TemaID;
                _Pitanje.TemaID = ClanakID;
                _Pitanje.DatumIzmjene = DateTime.Now;
                _Pitanje.DatumKreiranja = DateTime.Now;
                _Pitanje.Negativni = 0;
                _Pitanje.Pozitivni = 0;
                _Pitanje.Status = true;
                db.Pitanjas.Add(_Pitanje);
                db.SaveChanges();
            }
            else
                return RedirectToAction("Clanci", "Users");//ako su prazni temaID i clanakID
            return RedirectToAction("Clanci", "Users");
        }











    }
}
