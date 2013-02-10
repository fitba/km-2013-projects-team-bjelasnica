using System;
using System.Collections.Generic;

namespace FITWiki.Models
{
    public partial class Pretrage
    {
        public int PretragaID { get; set; }
        public System.DateTime Datum { get; set; }
        public int AktivnostID { get; set; }
        public string TekstPretrage { get; set; }
        public Nullable<int> ClanakID { get; set; }
        public int KorisnikID { get; set; }
    }
}
