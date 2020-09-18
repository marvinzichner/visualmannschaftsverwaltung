using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class Spiele : CustomPage
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
            previewHider.Visible = false;
            this.disableAdminFeatures();

            reloadContext();
        }
        #endregion

        #region Woker
        private void disableAdminFeatures()
        {
            if (GetUserFromSession().isUser())
            {
                addNewTurnier.Visible = false;
                generateTurniere.Visible = false;
                randomResults.Visible = false;
            }
        }

        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        private HtmlTableCell createCellTextboxes(int spielfk, int a, int b, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            TextBox tb1 = new TextBox();
            TextBox tb2 = new TextBox();

            tb1.ID = $"TBX-{spielfk.ToString()}-A";
            tb2.ID = $"TBX-{spielfk.ToString()}-B";
            tb1.Attributes.Add("internal-name", $"TBX-{spielfk.ToString()}-A");
            tb1.Attributes.Add("internal-id", $"{spielfk.ToString()}");
            tb2.Attributes.Add("internal-name", $"TBX-{spielfk.ToString()}-B");
            tb2.Attributes.Add("internal-id", $"{spielfk.ToString()}");
            tb1.Text = a.ToString();
            tb2.Text = b.ToString();

            tc.Controls.Add(tb1);
            tc.Controls.Add(tb2);
            tc.Attributes.Add("class", classes);
            return tc;
        }

        public void reloadContext()
        {
            loadDropdownContext();
            loadMannschaftenContext();
            loadAllData();
            loadAllRanks();

            editButton.Text = "Einträge bearbeiten";
            if (this.Session["isEditMode"] == "true")
                editButton.Text = "Änderungen speichern";
        }

        public void loadMannschaftenContext()
        {
            this.mannschaftenList = ApplicationController.getMannschaften(GetUserFromSession().getSessionId());
        }

        public Mannschaft getMannschaftByKey(int id)
        {
            return this.mannschaftenList.Find(mannschaft => mannschaft.ID.Equals(id));
        }

        public void loadDropdownContext()
        {
            turniereDropdown.Items.Clear();
            dropdownTeamA.Items.Clear();
            dropdownTeamb.Items.Clear();

            ApplicationController.getTurniere(GetUserFromSession().getSessionId()).ForEach(turnier => {
                ListItem listItem = new ListItem();
                listItem.Text = turnier.getName();
                listItem.Value = $"method=dataTurnier#turnier={turnier.getId()}";

                turniereDropdown.Items.Add(listItem);
            });

            if (this.Session["SelectedTurnier"] != null)
                ApplicationController.getTurniere(GetUserFromSession().getSessionId())
                    .Find(t => t.getId().ToString().Equals((string)this.Session["SelectedTurnier"]))
                    .getMannschaften()
                    .ForEach(mannschaft =>
                        {
                            ListItem listItem = new ListItem();
                            listItem.Text = mannschaft.Name;
                            listItem.Value = $"method=createNewTurnier#mannschaft={mannschaft.ID}";

                            dropdownTeamA.Items.Add(listItem);
                            dropdownTeamb.Items.Add(listItem);
                        });
        }

        public void createNewTurnier(Object sender, EventArgs e)
        {
            KeyValueList kv1 = new KeyValueList();
            KeyValueList kv2 = new KeyValueList();

            kv1.extractDataFromCombinedString(dropdownTeamA.SelectedItem.Value);
            kv2.extractDataFromCombinedString(dropdownTeamb.SelectedItem.Value);

            ApplicationController.createNewSpielOfTurnier(
                "human.created",
                Convert.ToInt32(kv1.getValueFromKeyValueList("mannschaft")),
                Convert.ToInt32(kv2.getValueFromKeyValueList("mannschaft")),
                ApplicationContext.disarmHijacking(spieltag.Text), 
                Convert.ToInt32((string)this.Session["SelectedTurnier"]),
                GetUserFromSession().getSessionId());

            createNewGame.Visible = false;
            reloadContext();
        }

        public void generateRandomResults(Object sender, EventArgs e)
        {
            ApplicationController.generateRandomResults(
                (string)this.Session["SelectedTurnier"],
                GetUserFromSession().getSessionId());
            reloadContext();
        }

        public void generateKnockoutSpiele(Object sender, EventArgs e)
        {
            int day = 1;
            string id = this.selectedTurnierId;
            List<Mannschaft> turnierMannschaften =
                ApplicationController.getTurniere(GetUserFromSession().getSessionId())
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
                            day.ToString(),
                            Utils.convertToInteger32((string)this.Session["SelectedTurnier"]),
                            GetUserFromSession().getSessionId());
                        day++;
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
                previewHider.Visible = true;
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
                ApplicationController.getSpiele(GetUserFromSession().getSessionId())
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

        public void showCreationMode(Object sender, EventArgs e)
        {
            createNewGame.Visible = true;
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
                spielTitle.InnerText = ApplicationController.getTurniere(GetUserFromSession().getSessionId())
                    .Find(turnier => turnier.getId().Equals((string)this.Session["SelectedTurnier"])).getName();
            }
            catch (Exception e)
            {
                spielTitle.InnerText = "Bitte wählen Sie ein Turnier aus";
            }

            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Globale Id", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Spieltag", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mannschaft A", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Ergebnis", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mannschaft B", "tablecell cellHead"));
            presenterTable.Rows.Add(trHead);

            if (selectedContext)
                ApplicationController.getSpiele(GetUserFromSession().getSessionId())
                    .FindAll(search => search.getTurnierId().ToString().Equals(ID))
                    .ForEach(spiel =>
                {
                    Mannschaft TeamA = getMannschaftByKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_A));
                    Mannschaft TeamB = getMannschaftByKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_B));

                    if (TeamA != null && TeamB != null)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        tr.Cells.Add(createCell($"{spiel.getId()}", "tablecell cellReadOnly"));
                        tr.Cells.Add(createCell($"{spiel.getSpieltag()}", "tablecell cellReadOnly"));
                        tr.Cells.Add(createCell($"{TeamA.Name}", "tablecell cellReadOnly"));

                        if (this.Session["isEditMode"] == null 
                            || this.Session["isEditMode"] == "false")
                            tr.Cells.Add(
                                createCell(
                                    $"{spiel.getResult(Spiel.TeamUnit.TEAM_A)}:{spiel.getResult(Spiel.TeamUnit.TEAM_B)}",
                                    "tablecell cellReadOnly"));
                        if (this.Session["isEditMode"] == "true")
                            tr.Cells.Add(
                                createCellTextboxes(
                                    spiel.getId(),
                                    spiel.getResult(Spiel.TeamUnit.TEAM_A),
                                    spiel.getResult(Spiel.TeamUnit.TEAM_B),
                                    "tablecell cellReadOnly"));

                        tr.Cells.Add(createCell($"{TeamB.Name}", "tablecell cellReadOnly"));
                        presenterTable.Rows.Add(tr);
                    }

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

        public void storeChanges()
        {
            List<Spiel> spiele = 
                ApplicationController.getSpiele(GetUserFromSession().getSessionId())
                .FindAll(s => s.getTurnierId() ==
                    Convert.ToInt32(this.Session["SelectedTurnier"]));

            spiele.ForEach(spiel =>
            {
                int A = 0;
                int B = 0;

                try { 
                    A = Convert.ToInt32(Request[$"ctl00$MainContent$TBX-{spiel.getId()}-A"]);
                    B = Convert.ToInt32(Request[$"ctl00$MainContent$TBX-{spiel.getId()}-B"]);

                    spiel.setResult(Spiel.TeamUnit.TEAM_A, A);
                    spiel.setResult(Spiel.TeamUnit.TEAM_B, B);
                }
                catch (Exception e)
                {
                    string deleteA = Request[$"ctl00$MainContent$TBX-{spiel.getId()}-A"];
                    string deleteB = Request[$"ctl00$MainContent$TBX-{spiel.getId()}-B"];

                    if (deleteA == "x" || deleteA == "X" || deleteB == "x" || deleteB == "X")
                        spiel.markDeleteFlag();
                }
            });

            ApplicationController.updateSpieleResult(spiele);
            this.Session["isEditMode"] = "false";
            reloadContext();
        }

        public void editList(Object sender, System.EventArgs e)
        {
            if (this.Session["isEditMode"] == null || this.Session["isEditMode"] == "false")
            { 
                this.Session["isEditMode"] = "true";
            }
            else
            {
                storeChanges();
            }
            reloadContext();
        }
        #endregion
    }
}