using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public ActionResult Index(KorisniciPretraga model)
        {
            if (model.ImePrezime == null)
                model.ImePrezime = "";
            if (model.Mail == null)
                model.Mail = "";

            int page = 0;
            if (Request["page"] != null)
            {
                int.TryParse(Request["page"], out page);
                page--; //Indeks umjesto rednog broja
            }

            using (var context = new FITWikiContext())
            {
                using (context.Database.Connection)
                {
                    context.Database.Connection.Open();
                    DbCommand cmd = context.Database.Connection.CreateCommand();
                    cmd.CommandText = "sp_Korisnici_SelectByNameMail";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ImePrezime", model.ImePrezime));
                    cmd.Parameters.Add(new SqlParameter("@Mail", model.Mail));
                    cmd.Parameters.Add(new SqlParameter("@Offset", page*model.PageSize));
                    cmd.Parameters.Add(new SqlParameter("@MaxRows", 2));
                    var totalCount = new SqlParameter("@TotalRows", 0) { Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(totalCount);

                    using (var reader = cmd.ExecuteReader())
                    {
                        model.Rezultat = DataReaderExtensions.MapToList<Korisnici>(reader);
                    }

                    model.RowCount = (totalCount.Value == null) ? 0 : Convert.ToInt32(totalCount.Value);
                    
                    return PartialView(model);
                }
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