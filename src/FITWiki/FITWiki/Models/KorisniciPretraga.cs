using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FITWiki.Models
{
    public class KorisniciPretraga
    {
        private string _mail;
        private string _imePrezime;

        public KorisniciPretraga()
        {
            ImePrezime = "";
            Mail = "";
            PageSize = 2;
        }

        public string Mail
        {
            get
            {
                return string.IsNullOrEmpty(_mail) ? "" : _mail;
            }

            set { _mail = value; }
        }

        public string ImePrezime
        {
            get
            {
                return string.IsNullOrEmpty(_imePrezime) ? "" : _imePrezime;
            }

            set { _imePrezime = value; }
        }
        public List<Korisnici> Rezultat { get; set; }
        public int RowCount { get; set; }
        public int PageSize { get; set; }
       
      
    }
}