using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class Spiele : System.Web.UI.Page
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        private bool selectedContext;
        private string selectedTurnierId;
        private List<Mannschaft> mannschaftenList;
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        public bool SelectedContext { get => selectedContext; set => selectedContext = value; }
        public string SelectedTurnierId { get => selectedTurnierId; set => selectedTurnierId = value; }
        public List<Mannschaft> MannschaftenList { get => mannschaftenList; set => mannschaftenList = value; }
        #endregion

        #region Konstruktor
        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;

            reloadContext();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region Woker
        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        public void reloadContext()
        {
            loadDropdownContext();
            loadMannschaftenContext();
            loadAllData();
            loadAllRanks();
        }

        public void loadMannschaftenContext()
        {
            this.mannschaftenList = ApplicationController.getMannschaften(getOrCreateSession());
        }

        public Mannschaft getMannschaftByKey(int id)
        {
            return this.mannschaftenList.Find(mannschaft => mannschaft.ID.Equals(id));
        }

        public void loadDropdownContext()
        {
            turniereDropdown.Items.Clear();
            ApplicationController.getTurniere(getOrCreateSession()).ForEach(turnier => {
                ListItem listItem = new ListItem();
                listItem.Text = turnier.getName();
                listItem.Value = $"method=dataTurnier#turnier={turnier.getId()}";

                turniereDropdown.Items.Add(listItem);
            });
        }

        public void generateRandomResults(Object sender, EventArgs e)
        {
            ApplicationController.generateRandomResults(
                (string)this.Session["SelectedTurnier"],
                getOrCreateSession());
            reloadContext();
        }

        // SELECT *, SUM(RESULT_A) as CALCULATED_A FROM `mvw_spiel` where TURNIER_FK=7 group by `MANNSCHAFT_A_FK` order by CALCULATED_A desc
        public void generateKnockoutSpiele(Object sender, EventArgs e)
        {
            string id = this.selectedTurnierId;
            List<Mannschaft> turnierMannschaften =
                ApplicationController.getTurniere(getOrCreateSession())
                .Find(t => t.getId().ToString().Equals((string)this.Session["SelectedTurnier"]))
                .getMannschaften();

            turnierMannschaften.ForEach(mannschaft =>
            {
                turnierMannschaften.ForEach(mannschaft2 =>
                {
                    if (mannschaft.ID != mannschaft2.ID)
                    {
                        ApplicationController.createNewSpielOfTurnier(
                            "generated.fwd",
                            mannschaft.ID,
                            mannschaft2.ID,
                            DateTime.Now.ToShortDateString(),
                            Utils.convertToInteger32((string)this.Session["SelectedTurnier"]),
                            getOrCreateSession());
                    }
                });
            });

            reloadContext();
        }

        public void loadAllRanks()
        {
            presenterRank.Rows.Clear();
            string ID = "";
            if ((string)this.Session["SelectedTurnier"] != null)
            {
                ID = (string)this.Session["SelectedTurnier"];
                selectedContext = true;
            }

            KeyValueList kv = new KeyValueList();
            int relativeCounter = 1;
            int lastResult = -1;

            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Position", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mannschaft", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Absolutes Ergebnis", "tablecell cellHead"));
            presenterRank.Rows.Add(trHead);

            if (selectedContext)
                ApplicationController.getSpiele(getOrCreateSession())
                    .FindAll(search => search.getTurnierId().ToString().Equals(ID))
                    .ForEach(spiel =>
                    {
                        kv.increaseOrUpdateKeyByInt32(
                            spiel.getMannschaft(Spiel.TeamUnit.TEAM_A).ToString(),
                            spiel.getResult(Spiel.TeamUnit.TEAM_A));

                        kv.increaseOrUpdateKeyByInt32(
                            spiel.getMannschaft(Spiel.TeamUnit.TEAM_B).ToString(),
                            spiel.getResult(Spiel.TeamUnit.TEAM_B));
                    });

            kv.sortByNumericValue().ForEach(team =>
            {
                int mannschaftKey = Convert.ToInt32(team.Key);
                int mannschaftValue = Convert.ToInt32(team.Value);
                HtmlTableRow tr = new HtmlTableRow();

                tr.Cells.Add(createCell($"{relativeCounter}", "tablecell cellReadOnly"));
                tr.Cells.Add(createCell($"{getMannschaftByKey(mannschaftKey).Name}", "tablecell cellReadOnly"));
                tr.Cells.Add(createCell($"{team.Value}", "tablecell cellReadOnly"));

                presenterRank.Rows.Add(tr);

                if (!lastResult.Equals(mannschaftValue))
                    relativeCounter++;
                //lastResult = mannschaftValue;
            });
        }


        public void loadAllData()
        {
            presenterTable.Rows.Clear();
            string ID = "";
            if ((string)this.Session["SelectedTurnier"] != null) { 
                ID = (string)this.Session["SelectedTurnier"];
                selectedContext = true;
            }

            try { 
                spielTitle.InnerText = ApplicationController.getTurniere(getOrCreateSession())
                    .Find(turnier => turnier.getId().Equals((string)this.Session["SelectedTurnier"])).getName();
            }
            catch (Exception e)
            {
                spielTitle.InnerText = "Bitte wählen Sie ein Turnier aus";
            }

            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Spieltag", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mannschaft A", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Ergebnis", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mannschaft B", "tablecell cellHead"));
            presenterTable.Rows.Add(trHead);

            if (selectedContext)
                ApplicationController.getSpiele(getOrCreateSession())
                    .FindAll(search => search.getTurnierId().ToString().Equals(ID))
                    .ForEach(spiel =>
                {
                    Mannschaft TeamA = getMannschaftByKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_A));
                    Mannschaft TeamB = getMannschaftByKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_B));

                    HtmlTableRow tr = new HtmlTableRow();
                    tr.Cells.Add(createCell($"{spiel.getSpieltag()}", "tablecell cellReadOnly"));
                    tr.Cells.Add(createCell($"{TeamA.Name}", "tablecell cellReadOnly"));
                    tr.Cells.Add(
                        createCell(
                            $"{spiel.getResult(Spiel.TeamUnit.TEAM_A)}:{spiel.getResult(Spiel.TeamUnit.TEAM_B)}",
                            "tablecell cellReadOnly"));
                    tr.Cells.Add(createCell($"{TeamB.Name}", "tablecell cellReadOnly"));
                    presenterTable.Rows.Add(tr);
                });
        }

        public void selectTurnier(Object sender, System.EventArgs e)
        {
            KeyValueList kv = new KeyValueList();
            kv.extractDataFromCombinedString(
                turniereDropdown.SelectedItem.Value.ToString());
            this.selectedTurnierId = kv.getValueFromKeyValueList("turnier");
            this.Session["SelectedTurnier"] = kv.getValueFromKeyValueList("turnier");
            this.selectedContext = true;
            reloadContext();
        }

        public string getOrCreateSession()
        {
            string session = "undefined";
            if (this.Session["User"] != null)
            {
                session = (string)this.Session["User"];
            }
            return session;
        }
        #endregion
    }
}