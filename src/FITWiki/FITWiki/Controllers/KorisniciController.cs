using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using FITWiki.Models;

using PagedList;

namespace FITWiki.Controllers
{
    public class KorisniciController : Controller
    {
        private FITWikiContext db = new FITWikiContext();

        //
        // GET: /Korisnici/

        public ActionResult Index(int? page, string currentFilterIP = "", string currentFilterM = "",  
                                  string imePrezime="", string mail="", string sortOrder="")
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PrezimeSortParm = String.IsNullOrEmpty(sortOrder) ? "Prezime desc" : "";
            ViewBag.DatumSortParm = sortOrder == "Datum" ? "Datum desc" : "Datum";

            //if (Request.HttpMethod == "GET")
            //{
            //    imePrezime = currentFilterIP;
            //    mail = currentFilterM;
            //}
            //else
            //{
            //    page = 1;
            //}

            ViewBag.currentFilterIP = imePrezime;
            ViewBag.currentFilterM = mail;

            using (var context = new FITWikiContext())
            {
                IEnumerable<Korisnici> korisnici = context.Database.SqlQuery<Korisnici>("EXEC sp_Korisnici_SelectByNameMail @ImePrezime, @Mail",
                    new SqlParameter("@ImePrezime", imePrezime),
                    new SqlParameter("@Mail", mail)).ToList();

                switch (sortOrder)
                {
                    case "Prezime desc":
                        korisnici = korisnici.OrderByDescending(k => k.Prezime);
                        break;
                    case "Datum":
                        korisnici = korisnici.OrderBy(k => k.DatumRegistracije);
                        break;
                    case "Datum desc":
                        korisnici = korisnici.OrderByDescending(k => k.DatumRegistracije);
                        break;
                    default:
                        korisnici = korisnici.OrderBy(k => k.Prezime);
                        break;
                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(korisnici.ToPagedList(pageNumber, pageSize));
            }
        }

        //
        // GET: /Korisnici/Details/5

        public ActionResult Details(int id = 0)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        //
        // GET: /Korisnici/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Korisnici/Create

        [HttpPost]
        public ActionResult Create(Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                korisnici.LozinkaSalt = GenerateSaltValue();
                korisnici.LozinkaHash = HashPassword(korisnici.Lozinka, korisnici.LozinkaSalt);
                db.Korisnicis.Add(korisnici);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(korisnici);
        }

        //
        // GET: /Korisnici/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        //
        // POST: /Korisnici/Edit/5

        [HttpPost]
        public ActionResult Edit(Korisnici korisnici)
        {
            if (ModelState.IsValid)
            {
                db.Entry(korisnici).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(korisnici);
        }

        //
        // GET: /Korisnici/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        //
        // POST: /Korisnici/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            db.Korisnicis.Remove(korisnici);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Korisnici/ChangePassword/5

        public ActionResult ChangePassword(int id = 0)
        {
            Korisnici korisnici = db.Korisnicis.Find(id);
            if (korisnici == null)
            {
                return HttpNotFound();
            }
            return View(korisnici);
        }

        //
        // POST: /Korisnici/ChangePassword/5

        [HttpPost]
        public ActionResult ChangePassword(int id, string lozinka)
        {
            string salt = GenerateSaltValue();
            string hash = HashPassword(lozinka, salt);
            using (var context = new FITWikiContext())
            {
                SqlParameterCollection parametri = new SqlCommand().Parameters;
                parametri.Add(new SqlParameter("@KorisnikID", id));
                parametri.Add(new SqlParameter("@LozinkaSalt", salt));
                parametri.Add(new SqlParameter("@LozinkaHash", hash));

                context.Database.ExecuteSqlCommand("sp_Korisnici_ChangePassword @KorisnikID, @LozinkaSalt, @LozinkaHash", parametri);
            }

            return RedirectToAction("Index");  
        }
    
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        #region Password Generator

        private static string GenerateSaltValue()
        {
            byte[] buf = new byte[16];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }

        private static string HashPassword(string clearData, string saltValue)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(clearData);
            byte[] src = Convert.FromBase64String(saltValue);
            byte[] dst = new byte[src.Length + bytes.Length];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        #endregion

    }
}