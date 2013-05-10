using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FITKMS_business.Data
{
    public class QAService
    {
        public static int totalRows;

        // get pitanje po ID-u
        public static Pitanja getPitanjeByID(int id)
        {
            return Connection.dm.Pitanja.Where(x => x.PitanjeID == id).SingleOrDefault();
        }
        //set broj pregleda ++
        public static void SetBrojPregleda(Pitanja pitanje)
        {
            pitanje.BrojPregleda++;
            Connection.dm.SaveChanges();
        }

        // lista oblasti za postavi pitanje
        public static List<Oblasti> getAllOblasti()
        {
            return Connection.dm.Oblasti.ToList();

        }
        // lista tema za postavi pitanje
        public static List<Teme> getAllTemeByID(int id)
        {
            //return Connection.dm.fsp_getAllTemeByID(id).ToList();
            return Connection.dm.Teme.Where(x => x.OblastID == id).ToList();
        }

        public static List<Tagovi> getAllTagovi()
        {
            return Connection.dm.Tagovi.ToList();
        }

        public static Tagovi getTagByID(int id)
        {
            return Connection.dm.Tagovi.Where(x => x.TagID == id).SingleOrDefault();

        }
        // unos novog pitanja
        public static void savePitanje(Pitanja pitanje, List<string> tagovi)
        {
            Connection.dm.Pitanja.Add(pitanje);
            Connection.dm.SaveChanges();

            foreach (var t in tagovi)
            {
                try
                {
                    Connection.dm.fsp_PitanjaTagovi_Insert(pitanje.PitanjeID, t);
                }
                catch
                {

                }
            }
        }

        // get korisnika po ID-u
        public static Korisnici GetKorisnikByID(int id)
        {
            return Connection.dm.Korisnici.Where(x => x.KorisnikID == id).SingleOrDefault();

        }

        // vraca listu tagova koji su koristeni u pitanju
        public static List<Tagovi> getListaTagovaUpitanju(int Pid)
        {
            return DAPitanja.SelectTags(Pid).ToList();
        }

        // lista svih odgovora za postavljeno pitanje
        public static List<fsp_Odgovori_SelectByPitanjeId_Result> getAllOdgovoriZaPitanje(int id)
        {
            return Connection.dm.fsp_Odgovori_SelectByPitanjeId(id).ToList();

        }
        // unos novog odgovora
        public static void saveOdgovor(Odgovori odg)
        {

            Odgovori odgovor = new Odgovori();
            odgovor.DatumIzmjene = odg.DatumIzmjene;
            odgovor.DatumKreiranja = odg.DatumKreiranja;
            odgovor.KorisnikID = odg.KorisnikID;
            odgovor.Negativni = 0;
            odgovor.Pozitivni = 0;
            odgovor.Status = true;
            odgovor.Tekst = odg.Tekst;
            odgovor.PitanjeID = odg.PitanjeID;

            Connection.dm.Odgovori.Add(odgovor);
            Connection.dm.SaveChanges();


        }
        // get odogovor by id
        public static Odgovori getOdgovorByID(int id)
        {
            return Connection.dm.Odgovori.Where(x => x.OdgovorID == id).SingleOrDefault();

        }

        // provjera da li je korisnik glasao vec za odgovor
        public static bool Je_LiGlasao(int KorisnikID, int odgID)
        {
            List<OdgovoriGlasovi> listaGlasova = Connection.dm.OdgovoriGlasovi.Where(x => x.OdgovorID == odgID).ToList();

            int ima = 0;
            foreach (OdgovoriGlasovi i in listaGlasova)
            {
                if (i.KorisnikID == KorisnikID)
                {
                    ima++;
                    break;
                }

            }
            if (ima == 0)
                return false; // ako nije glasao
            else
                return true; // ako je glasao

        }

        // update kad glasa pozitino za neki odgovor
        public static void UpdateOdgovorP(Odgovori odg, int kID)
        {
            odg.Pozitivni++;
            Connection.dm.SaveChanges();

            OdgovoriGlasovi odgG = new OdgovoriGlasovi();

            odgG.KorisnikID = kID;
            odgG.OdgovorID = odg.OdgovorID;
            odgG.Datum = DateTime.Now;
            odgG.Pozitivni = true;
            Connection.dm.OdgovoriGlasovi.Add(odgG);
            Connection.dm.SaveChanges();

        }
        //update negativno kad glasa za neki odgvor
        public static void UpdateOdgovorN(Odgovori odg, int kID)
        {
            odg.Negativni++;
            Connection.dm.SaveChanges();

            OdgovoriGlasovi odgG = new OdgovoriGlasovi();

            odgG.KorisnikID = kID;
            odgG.OdgovorID = odg.OdgovorID;
            odgG.Datum = DateTime.Now;
            odgG.Pozitivni = false;
            Connection.dm.OdgovoriGlasovi.Add(odgG);
            Connection.dm.SaveChanges();

        }


        public static bool Je_LiGlasaoZaPitanje(int korisnikID, int pitanjeID)
        {

            List<PitanjaGlasovi> listaGlasovaZaPitanje = Connection.dm.PitanjaGlasovi.Where(x => x.PitanjeID == pitanjeID).ToList();

            int ima = 0;

            foreach (PitanjaGlasovi i in listaGlasovaZaPitanje)
            {
                if (i.KorisnikID == korisnikID)
                {
                    ima++;
                    break;
                }

            }

            if (ima == 0)
                return false; // ako nije glasao

            else
                return true; // ako je glasao


        }

        public static void UpdatePitanjeLike(Pitanja pitanje, int korisnikID)
        {
            pitanje.Pozitivni++;

            Connection.dm.SaveChanges();

            PitanjaGlasovi pitanjaG = new PitanjaGlasovi();

            pitanjaG.KorisnikID = korisnikID;
            pitanjaG.Pozitivni = true;
            pitanjaG.Datum = DateTime.Now;
            pitanjaG.PitanjeID = pitanje.PitanjeID;

            Connection.dm.PitanjaGlasovi.Add(pitanjaG);
            Connection.dm.SaveChanges();
        }

        public static void UpdatePitanjeDislike(Pitanja pitanje, int korisnikID)
        {
            pitanje.Negativni++;
            Connection.dm.SaveChanges();

            PitanjaGlasovi pitanjaG = new PitanjaGlasovi();

            pitanjaG.KorisnikID = korisnikID;
            pitanjaG.Pozitivni = false;
            pitanjaG.Datum = DateTime.Now;
            pitanjaG.PitanjeID = pitanje.PitanjeID;

            Connection.dm.PitanjaGlasovi.Add(pitanjaG);
            Connection.dm.SaveChanges();
        }

        public static List<Pitanja> getAllPitanjaTEST()
        {
            return Connection.dm.Pitanja.ToList();

        }

        public static List<fsp_Pitanja_SelectSearch_Result> SelectSearch(string search, int maxRows, int offset)
        {
            System.Data.Objects.ObjectParameter total = new System.Data.Objects.ObjectParameter("TotalRows", 0);
            List<fsp_Pitanja_SelectSearch_Result> pitanja = Connection.dm.fsp_Pitanja_SelectSearch(search, offset, maxRows, total).ToList();

            totalRows = Convert.ToInt32(total.Value);

            return pitanja;
        }




        public static void UpdateQuestionLikeStatus(Pitanja pitanje, int userId, bool status)
        {
            if (Connection.dm.PitanjaGlasovi.First(p => p.PitanjeID == pitanje.PitanjeID && p.KorisnikID == userId).Pozitivni != status)
            {
                Connection.dm.PitanjaGlasovi.First(p => p.PitanjeID == pitanje.PitanjeID && p.KorisnikID == userId).Pozitivni = status;
                Connection.dm.SaveChanges();
                if (status)
                {
                    pitanje.Pozitivni++;
                    pitanje.Negativni--;
                    Connection.dm.SaveChanges();
                }
                else
                {
                    pitanje.Pozitivni--;
                    pitanje.Negativni++;
                    Connection.dm.SaveChanges();
                }
            }
        }

        public static void UpdateAnswerLikeStatus(Odgovori odgovor, int userId, bool status)
        {
            if (Connection.dm.OdgovoriGlasovi.First(o => o.OdgovorID == odgovor.OdgovorID && o.KorisnikID == userId).Pozitivni != status)
            {
                Connection.dm.OdgovoriGlasovi.First(o => o.OdgovorID == odgovor.OdgovorID && o.KorisnikID == userId).Pozitivni = status;
                Connection.dm.SaveChanges();

                if (status)
                {
                    odgovor.Pozitivni++;
                    odgovor.Negativni--;
                    Connection.dm.SaveChanges();
                }
                else
                {
                    odgovor.Pozitivni--;
                    odgovor.Negativni++;
                    Connection.dm.SaveChanges();
                }
            }
        }

    }
}
