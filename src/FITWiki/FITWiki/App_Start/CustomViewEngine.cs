using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FITWiki.App_Start
{
    public class CustomViewEngine: RazorViewEngine
    {
        public CustomViewEngine()
        {
            var viewLocations = new[] {  
            "~/Views/Admin/{1}/{0}.cshtml"
        };

            this.PartialViewLocationFormats = viewLocations;
            this.ViewLocationFormats = viewLocations;
        }
    }
}