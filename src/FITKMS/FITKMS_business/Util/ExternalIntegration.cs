using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace FITKMS
{
    public class ExternalIntegration
    {
        #region Integracija sa stackoverflov
        /// <summary>
        /// http://stackoverflow.com/questions/6031003/stackoverflow-search-api
        /// </summary>
        /// <param name="a"></param>
        public List<Question> SearchStackOverflow(string a)
        {
            try
            {
                //var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://api.stackoverflow.com/1.1/search?intitle=" + Uri.EscapeDataString(y));
                string url = "http://api.stackoverflow.com/1.1/search?intitle=" + HttpUtility.UrlEncode(a) + "&pagesize=5&sort=votes";
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = request.GetResponse();
                string json = ExtractJsonResponse(response);
                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic d = js.Deserialize<dynamic>(json);

                dynamic[] questions = d["questions"];

                List<Question> question = new List<Question>();
                for (int i = 0; i < questions.Length; i++)
                {
                    Question q = new Question();
                    q.question_timeline_url = questions[i]["question_timeline_url"];
                    q.title = questions[i]["title"];
                    q.answer_count = questions[i]["answer_count"];
                    question.Add(q);
                }
                return question;
            }
            catch (System.Exception)
            {
                return new List<Question>();
            }
        }
        private string ExtractJsonResponse(WebResponse response)
        {
            string json;
            using (var outStream = new MemoryStream())
            using (var zipStream = new GZipStream(response.GetResponseStream(),
                CompressionMode.Decompress))
            {
                zipStream.CopyTo(outStream);
                outStream.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(outStream, Encoding.UTF8))
                {
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }
        #endregion

        #region Integracija sa wikipediom
        public List<WikiP> SearchWikipedia(string a)
        {
            List<WikiP> ls = new List<WikiP>();
            try
            {
                HttpWebRequest request
                    = WebRequest.Create("http://en.wikipedia.org/w/api.php?action=opensearch&search=" + HttpUtility.UrlEncode(a) + "&limit=10&namespace=0&format=xml") as HttpWebRequest;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";

                string odg;
                using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    odg = reader.ReadToEnd();
                    ls.AddRange(GetWikiByXml(odg));
                }
            }
            catch (System.Exception)
            {

            }
            return ls;
        }
        IEnumerable<WikiP> GetWikiByXml(string a)
        {
            using (StringReader textReader = new StringReader(a))
            {
                using (XmlReader xmlReader = XmlReader.Create(textReader))
                {
                    while (xmlReader.Read())
                    {
                        WikiP wiki = new WikiP();

                        if (xmlReader.Name == "Image")
                            wiki.Image = xmlReader.ReadInnerXml();
                        if (xmlReader.Name == "Text")
                            wiki.Name = xmlReader.ReadInnerXml();
                        if (xmlReader.Name == "Description")
                            wiki.Description = xmlReader.ReadInnerXml();
                        if (xmlReader.Name == "Url")
                            wiki.Url = xmlReader.ReadInnerXml();
                        if (wiki.Name == null || wiki.Url == null)
                            continue;
                        yield return wiki;
                    }
                }
                yield break;
            }
        }
        #endregion
    }
    public class Question
    {
        public string title { get; set; }
        public string question_timeline_url { get; set; }
        public int answer_count { get; set; }

        //Potrebno preklopiti zbog korištenja metode Distinct
        public override bool Equals(object obj)
        {
            Question dest = (Question)obj;
            if (dest == null)
                return false;
            return this.question_timeline_url == dest.question_timeline_url;
        }

        public override int GetHashCode()
        {
            return this.question_timeline_url.GetHashCode();
        }
    }
    public class WikiP
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }


        //Potrebno preklopiti zbog korištenja metode Distinct
        public override bool Equals(object obj)
        {
            WikiP dest = (WikiP)obj;
            if (dest == null)
                return false;
            return this.Url == dest.Url;
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode();
        }
    }






    
}