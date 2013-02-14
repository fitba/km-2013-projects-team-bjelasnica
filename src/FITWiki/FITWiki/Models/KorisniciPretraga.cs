using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FITWiki.Models
{
    public class KorisniciPretraga
    {
        public KorisniciPretraga()
        {
            ImePrezime = "";
            Mail = "";
        }

        public string ImePrezime { get; set; }
        public string Mail { get; set; }
        public List<Korisnici> Rezultat { get; set; }
        public int RowCount { get; set; }
    }
}