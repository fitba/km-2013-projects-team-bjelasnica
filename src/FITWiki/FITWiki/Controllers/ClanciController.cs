using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FITWiki.Models;

namespace FITWiki.Controllers
{
    public class ClanciController : Controller
    {
        private FITWikiContext db = new FITWikiContext();

        //
        // GET: /Clanci/

        public ActionResult Index(int VrstaID = 0, string naslov = "", string kRijeci = "")
        {

            using (var context = new FITWikiContext())
            {
                context.Database.Connection.Open();
                DbCommand cmd = context.Database.Connection.CreateCommand();
                cmd.CommandText = "sp_Clanci_SelectByVrstaNazivKRijeci";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@VrstaID", VrstaID));
                cmd.Parameters.Add(new SqlParameter("@Naslov", naslov));
                cmd.Parameters.Add(new SqlParameter("@KRijeci", kRijeci));
                cmd.Parameters.Add(new SqlParameter("@Offset", 0));
                cmd.Parameters.Add(new SqlParameter("@Maxrows", 10));
                var totalCount = new SqlParameter("@TotalRows", 0) { Direction = ParameterDirection.Output };
                cmd.Parameters.Add(totalCount);

                using (var reader = cmd.ExecuteReader())
                {
                    IEnumerable<Clanci> clanci = DataReaderExtensions.MapToList<Clanci>(reader);
                    return View(clanci.ToList());
                }
                
                
            }
        }

        //
        // GET: /Clanci/Details/5

        public ActionResult Details(int id = 0)
        {
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            return View(clanci);
        }

        //
        // GET: /Clanci/Create

       
        public ActionResult Create()
        {
            ViewBag.TemaID = new SelectList(db.Temes, "TemaID", "Naziv");
            ViewBag.VrstaID = new SelectList(db.VrsteClanakas, "VrstaID", "Naziv");
            return View();
        }

        //
        // POST: /Clanci/Create

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Clanci clanci)
        {
            if (ModelState.IsValid)
            {
                clanci.KorisnikID = 1;
                clanci.DatumKreiranja = DateTime.Now;
                clanci.DatumIzmjene = DateTime.Now;
                db.Clancis.Add(clanci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TemaID = new SelectList(db.Temes, "TemaID", "Naziv", clanci.TemaID);
            ViewBag.VrstaID = new SelectList(db.VrsteClanakas, "VrstaID", "Naziv", clanci.VrstaID);
            return View(clanci);
        }

        //
        // GET: /Clanci/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            ViewBag.KorisnikID = new SelectList(db.Korisnicis, "KorisnikID", "Ime", clanci.KorisnikID);
            ViewBag.TemaID = new SelectList(db.Temes, "TemaID", "Naziv", clanci.TemaID);
            ViewBag.VrstaID = new SelectList(db.VrsteClanakas, "VrstaID", "Naziv", clanci.VrstaID);
            return View(clanci);
        }

        //
        // POST: /Clanci/Edit/5

        [HttpPost]
        public ActionResult Edit(Clanci clanci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clanci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KorisnikID = new SelectList(db.Korisnicis, "KorisnikID", "Ime", clanci.KorisnikID);
            ViewBag.TemaID = new SelectList(db.Temes, "TemaID", "Naziv", clanci.TemaID);
            ViewBag.VrstaID = new SelectList(db.VrsteClanakas, "VrstaID", "Naziv", clanci.VrstaID);
            return View(clanci);
        }

        //
        // GET: /Clanci/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Clanci clanci = db.Clancis.Find(id);
            if (clanci == null)
            {
                return HttpNotFound();
            }
            return View(clanci);
        }

        //
        // POST: /Clanci/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Clanci clanci = db.Clancis.Find(id);
            db.Clancis.Remove(clanci);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}