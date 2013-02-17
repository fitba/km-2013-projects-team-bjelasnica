using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FITWiki.Models;

namespace FITWiki.Controllers
{
    public class HomeController : Controller
    {
        private FITWikiContext db = new FITWikiContext();
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

   


    }
}
