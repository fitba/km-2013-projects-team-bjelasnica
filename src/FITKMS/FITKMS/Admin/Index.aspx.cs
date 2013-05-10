using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using FITKMS_business.Data;

namespace FITKMS.Admin
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (DAKorisnici.IsAdmin(User.Identity.Name))
                {
                    BindAreas();
                    BindListAreas();
                    BindThemes();
                    BindTags();
                }
                else
                    Response.Redirect("/Default.aspx");
            }
        }

        #region Oblasti
        private void BindAreas()
        {
            areasGrid.DataSource = DAOblasti.Select();
            areasGrid.DataBind();
            activateAreasTab();
        }

        private void activateAreasTab()
        {
            themesTab.Attributes.Add("class", "");
            tagsTab.Attributes.Add("class", "");
            areasTab.Attributes.Add("class", "active");
            areas.Attributes.Add("class", "tab-pane fade active in");
            themes.Attributes.Add("class", "tab-pane fade");
            tags.Attributes.Add("class", "tab-pane fade");
        }

        protected void areaSubmit_Click(object sender, EventArgs e)
        {
            Oblasti area = new Oblasti();
            if (areaInput.Text != "")
                area.Naziv = areaInput.Text.Trim();
            else
            {
                labelAreaInput.Visible = true;
                return;
            }
            area.Status = true;
            area.DatumKreiranja = DateTime.Now;
            area.DatumIzmjene = DateTime.Now;
            DAOblasti.Save(area);
            BindAreas();
            activateAreasTab();
            labelAreaInput.Visible = false;
            areaInput.Text = "";
        }

        protected void areasGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "deleteArea")
            {
                int areaID = Convert.ToInt32(e.CommandArgument);
                DAOblasti.UpdateStatus(areaID);
                BindAreas();
                activateAreasTab();
            }
        }
        #endregion

        #region Teme
        private void BindListAreas()
        {
            List<Oblasti> areas = DAOblasti.Select();
            Oblasti empty = new Oblasti();
            empty.OblastID = 0;
            empty.Naziv = "Odaberite oblast";
            areas.Insert(0, empty);
            listAreas.DataSource = areas;
            listAreas.DataTextField = "Naziv";
            listAreas.DataValueField = "OblastID";
            listAreas.DataBind();
        }

        private void BindThemes()
        {
            List<Teme> themes = new List<Teme>();
            if (listAreas.SelectedValue != "0")
                themes = DATeme.SelectByArea(Convert.ToInt32(listAreas.SelectedValue));
            else
            {
                themes = DATeme.Select(true);
                themes.RemoveAt(0);
            }

            themesGrid.DataSource = themes;
            themesGrid.DataBind();
        }

        private void activateThemesTab()
        {
            areasTab.Attributes.Add("class", "");
            tagsTab.Attributes.Add("class", "");
            themesTab.Attributes.Add("class", "active");
            themes.Attributes.Add("class", "tab-pane fade active in");
            areas.Attributes.Add("class", "tab-pane fade");
            tags.Attributes.Add("class", "tab-pane fade");
        }

        protected void listAreas_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindThemes();
            activateThemesTab();
        }

        protected void themeSubmit_Click(object sender, EventArgs e)
        {
            Teme theme = new Teme();
            if (listAreas.SelectedValue != "0")
                theme.OblastID = Convert.ToInt32(listAreas.SelectedValue);
            else
                theme.OblastID = null;
            if (themeInput.Text != "")
                theme.Naziv = themeInput.Text.Trim();
            else
            {
                labelThemeInput.Visible = true;
                activateThemesTab();
                return;
            }
            theme.Status = true;
            theme.DatumKreiranja = DateTime.Now;
            theme.DatumIzmjene = DateTime.Now;
            DATeme.Save(theme);
            BindThemes();
            activateThemesTab();
            labelThemeInput.Visible = false;
            themeInput.Text = "";
        }

        protected void themesGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "deleteTheme")
            {
                int themeID = Convert.ToInt32(e.CommandArgument);
                DATeme.UpdateStatus(themeID);
                BindThemes();
                activateThemesTab();
            }
        }

        #endregion

        #region Tagovi
        private void BindTags()
        {
            tagsGrid.DataSource = DATagovi.SelectAll();
            tagsGrid.DataBind();
        }

        private void activateTagsTab()
        {
            areasTab.Attributes.Add("class", "");
            themesTab.Attributes.Add("class", "");
            tagsTab.Attributes.Add("class", "active");
            tags.Attributes.Add("class", "tab-pane fade active in");
            areas.Attributes.Add("class", "tab-pane fade");
            themes.Attributes.Add("class", "tab-pane fade");
        }

        protected void tagSubmit_Click(object sender, EventArgs e)
        {
            Tagovi tag = new Tagovi();
            if (tagInput.Text != "")
                tag.Naziv = tagInput.Text.Trim();
            else
            {
                labelTagInput.Visible = true;
                activateTagsTab();
                return;
            }
            tag.Opis = tagDescInput.Text.Trim();
            tag.Status = true;

            DATagovi.Save(tag);
            BindTags();
            activateTagsTab();
            labelTagInput.Visible = false;
            tagInput.Text = "";
            tagDescInput.Text = "";
        }

        protected void tagsGrid_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "deleteTag")
            {
                int tagID = Convert.ToInt32(e.CommandArgument);
                DATagovi.UpdateStatus(tagID);
                BindTags();
                activateTagsTab();
            }
        }

        #endregion
        
    }
}