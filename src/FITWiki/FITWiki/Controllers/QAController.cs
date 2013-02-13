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
            //      using (var context = new FITWikiContext())
            //{
            //     SqlParameterCollection parametri = new SqlCommand().Parameters;
            //     parametri.Add(new SqlParameter("@TextPitanja", pitanje));
            //     parametri.Add(new SqlParameter("@KorisnikID", 2));//dodati iz sesije
            //     parametri.Add(new SqlParameter("@ClanakID", ClanakID));
            //     parametri.Add(new SqlParameter("@TemaID", TemaID));
            //     parametri.Add(new SqlParameter("@DatumKreiranja", DateTime.Now));
            //     parametri.Add(new SqlParameter("@DatumIzmjene", DateTime.Now));
            //     parametri.Add(new SqlParameter("@Pozitivni", 0));
            //     parametri.Add(new SqlParameter("@Negativni", 0));

            //     context.Database.ExecuteSqlCommand("InsertPitanjeForQuestion @TextPitanja, @KorisnikID, @ClanakID,@TemaID,@DatumKreiranja,@DatumIzmjene,@Pozitivni,@Negativni", parametri);
            // }


                using (var context = new FITWikiContext())
                {
                    SqlParameterCollection parametri = new SqlCommand().Parameters;
                    parametri.Add(new SqlParameter("@Naziv", "Cistacica"));
                    parametri.Add(new SqlParameter("@DatumKreiranja", DateTime.Now));
                    parametri.Add(new SqlParameter("@DatumIzmjene", DateTime.Now));

                    context.Database.ExecuteSqlCommand("InsertUloga @Naziv, @DatumKreiranja, @DatumIzmjene", parametri);
                }




            }
            else
                return RedirectToAction("Clanci", "QA");//ako su prazni temaID i clanakID
            return RedirectToAction("Clanci", "QA");
        }











    }
}
