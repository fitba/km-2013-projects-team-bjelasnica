using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;
using FITKMS_business.Util;
using System.Web.UI.HtmlControls;

namespace FITKMS.Wiki
{
    public partial class Index : System.Web.UI.Page
    {
        protected List<VrsteClanaka> types;
        protected List<fsp_Clanci_SelectSearch_Result> articles;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();

                if (User.Identity.Name != "")
                    HybridRecommendation();
                else
                    RecommendBestRated();
                
                HtmlControl questionRecommend = (HtmlControl)this.Master.FindControl("questionRecommend");
                questionRecommend.Visible = false;
                WikiRecommendation();
            }
        }

        private void RecommendBestRated()
        {
            DataList articlesList = (DataList)this.Master.FindControl("articlesList");
            Label title = (Label)this.Master.FindControl("recTitleLabel");
            title.Text = "Preporučeni članci";
            articlesList.DataSource = DAClanci.SelectBestRated(); ;
            articlesList.DataBind();
        }

        protected void searchArticlesSubmit_Click(object sender, EventArgs e)
        {
            articlesGrid.CurrentPageIndex = 0;
            BindGrid();
            WikiRecommendation();
            saveSearch();
        }

        private void saveSearch()
        {
            try
            {
                //Pohrani pretragu korisnika
                if (articlesGrid.Items.Count > 0 && User.Identity.Name != "")
                {
                    Pretrage search = new Pretrage();
                    search.AktivnostID = DAAktivnosti.Select("Pretraga članaka").AktivnostID;
                    search.TekstPretrage = searchInput.Text;
                    search.KorisnikID = Convert.ToInt32(User.Identity.Name);
                    search.Datum = DateTime.Now;
                    DAAktivnosti.Insert(search);
                }
            }
            catch
            {

            }
        }

        private void BindGrid()
        {
            int offset = articlesGrid.CurrentPageIndex * articlesGrid.PageSize;
            string search = '"' + searchInput.Text.Trim() + '"';
            articles = DAClanci.Search(search, articlesGrid.PageSize, offset);
            articlesGrid.VirtualItemCount = DAClanci.totalRows;
            articlesGrid.DataBind();

        }

        private void HybridRecommendation()
        {
            DataList articlesList = (DataList)this.Master.FindControl("articlesList");
            RecommendationService recommendation = new RecommendationService();
            articlesList.DataSource = recommendation.GetTopArticles(Convert.ToInt32(User.Identity.Name));
            articlesList.DataBind();
        }

        private void WikiRecommendation()
        {
            DataList wikiList = (DataList)this.Master.FindControl("wikiList");

            List<string> words = new List<string>();

            if (searchInput.Text.Trim() != "")
                words.Add(searchInput.Text);
            else
            {
                foreach (DataGridItem item in articlesGrid.Items)
                {
                    LinkButton titleLink = (LinkButton)item.FindControl("titleLink");
                    words.Add(titleLink.Text);
                }
            }

            ExternalIntegration integration = new ExternalIntegration();
            List<WikiP> articlesWiki = new List<WikiP>();
            List<WikiP> articlesWikiRecommend = new List<WikiP>();

            foreach (string w in words)
            {
                articlesWiki.Clear();
                articlesWiki.AddRange(integration.SearchWikipedia(w));
                articlesWikiRecommend.AddRange(articlesWiki.Take(2).ToList());
            }

            articlesWikiRecommend = articlesWikiRecommend.Distinct().ToList();
            wikiList.DataSource = articlesWikiRecommend;
            wikiList.DataBind();

            HtmlControl div = (HtmlControl)this.Master.FindControl("wikiRecommend");
            div.Visible = true;
        }

        protected void articlesGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            articlesGrid.CurrentPageIndex = e.NewPageIndex;
            BindGrid();
        }

        private string getPart(string text)
        {
            if (text.IndexOf("<p>") == 0)
                text = text.Remove(0, 3);
            text = text.Replace("<p>", "\n");
            text = text.Replace("<P>", "\n");
            text = text.Replace("<BR>", "\n");
            text = text.Replace("<BR />", "\n");
            text = text.Replace("<br>", "\n");
            text = text.Replace("<br />", "\n");
            text = HtmlRemoval.StripTagsCharArray(text);

            if (text.Length > 150)
            {
                text = text.Substring(0, 150);
                for (int i = 99; i > 0; i--)
                {
                    if (text[i] != ' ')
                        text = text.Remove(i, 1);
                    else
                        break;
                }
                text = text + " ...";
            }

            text = text.Replace("\n", "<br />");
            return text;
        }

        protected void articlesGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex != -1)
            {
                Literal textLiteral = (Literal)e.Item.FindControl("textLiteral");
                textLiteral.Text = getPart(articles[e.Item.ItemIndex].Tekst);

                Repeater tagsRepeater = (Repeater)e.Item.FindControl("tagsRepeater");
                tagsRepeater.DataSource = DAClanci.SelectTags(articles[e.Item.ItemIndex].ClanakID);
                tagsRepeater.DataBind();
            }
        }
    }
}