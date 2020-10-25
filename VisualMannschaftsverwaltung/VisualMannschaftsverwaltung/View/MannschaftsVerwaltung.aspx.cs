using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class MannschaftsVerwaltung : CustomPage
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        private string SESSION_MANNSCHAFT = "MVW_MANNSCHAFT";
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        #endregion

        #region Konstruktor
        #endregion

        #region Worker
        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;

            this.prepareData();
            this.loadMembers();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.disableAdminFeatures();
        }

        private void disableAdminFeatures()
        {
            if (GetUserFromSession().isUser())
            {
                teamsDelete.Visible = false;
                teamsEdit.Visible = false;
                personList.Visible = false;
                personListButton.Visible = false;
                personListDelete.Visible = false;
                personListDeleteButton.Visible = false;
                showCreationPanelButton.Visible = false;
            }
        }

        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        protected void prepareData()
        {
            mannschaftenTabelle.Rows.Clear();

            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Globale Id", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Name", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Typ", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Mitglieder", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Aktionen", "tablecell cellHead"));
            mannschaftenTabelle.Rows.Add(trHead);

            //Dropdown
            teamsList.Items.Clear();
            ApplicationController.getMannschaften(
               GetUserFromSession().getSessionId()).ForEach(m =>
            {
                ListItem li = new ListItem();
                li.Text = $"{m.Name} ({m.SportArt.ToString()})";
                li.Value = m.Name;
                teamsList.Items.Add(li);

                HtmlTableRow tr = new HtmlTableRow();
                tr.Attributes.Add("class", "cellBodyHover");
                tr.Cells.Add(createCell($"{m.ID.ToString()}", "tablecell cellReadOnly"));
                tr.Cells.Add(createCell($"{m.Name}", "tablecell cellBody"));
                tr.Cells.Add(createCell($"{m.SportArt.ToString()}", "tablecell cellReadOnly"));

                string cellPersonen = "";
                m.Personen.ForEach(person =>
                {
                    cellPersonen += $"{person.Nachname.ToString().ToUpper()}, {person.Name} <br>";
                });
                tr.Cells.Add(createCell($"{cellPersonen}", "tablecell cellReadOnly"));

                HtmlTableCell cell = new HtmlTableCell();
                Button select = new Button();
                select.Click += new EventHandler(tableButtonSelected);
                select.Text = "Auswählen";
                select.Attributes.Add("data", $"mannschaft={m.ID.ToString()}#data=attribute");
                cell.Controls.Add(select);
                tr.Cells.Add(cell);

                mannschaftenTabelle.Rows.Add(tr);
            });

            if (ApplicationController.getMannschaften(
                GetUserFromSession().getSessionId()).Count == 0)
            {
                selectTeamAlternative.Visible = true;
                selectTeam.Visible = false;
                if(GetUserFromSession().isAdmin())
                    creationPanel.Visible = true;
            }
            else
            {
                creationPanel.Visible = false;
                selectTeamAlternative.Visible = false;
                selectTeam.Visible = true;
            }
        }

        public void tableButtonSelected(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            KeyValueList kv = new KeyValueList();
            kv.extractDataFromCombinedString(selectedButton.Attributes["data"]);
            string mannschaft = kv.getValueFromKeyValueList("mannschaft");

            this.Session[SESSION_MANNSCHAFT] = mannschaft;
            this.loadMembers();
        }

        protected void loadMembers(Mannschaft.OrderBy ob = Mannschaft.OrderBy.UNSORTED) {
            //Team Members
            membersListContainer.Controls.Clear();
            personListDelete.Items.Clear();
            personList.Items.Clear();

            int mannschaftId = -1;
            if (this.Session[SESSION_MANNSCHAFT] != null)
            {
                mannschaftId = Convert.ToInt32(this.Session[SESSION_MANNSCHAFT].ToString());
                List<Person> personen = new List<Person>();

                try { 

                    displayMannschaftName.InnerHtml = 
                        ApplicationController
                        .getMannschaften(GetUserFromSession().getSessionId())
                        .Find(mannschaft => mannschaft.ID == mannschaftId).Name;

                    ApplicationController.getMannschaften(GetUserFromSession().getSessionId())
                        .Find(mannschaft => mannschaft.ID == mannschaftId)
                        .Personen
                        .ForEach(person =>
                        {
                            createPersonEntry(person, "");

                            ListItem li = new ListItem();
                            li.Text = $"{person.Nachname.ToUpper()}, {person.Name} ({person.Birthdate})";
                            li.Value = $"{person.ID}";
                            personListDelete.Items.Add(li);

                            personen.Add(person);
                        });

                    personList.Items.Clear();
                    SportArt sa = ApplicationController.getMannschaften(GetUserFromSession().getSessionId())
                        .Find(mannschaft => mannschaft.ID == mannschaftId).SportArt;
                    ApplicationController.getUnsortedPersonen(GetUserFromSession().getSessionId())
                        .ForEach(person =>
                        {
                            if (personen.Find(p => p.ID == person.ID) == null)
                            {
                                ListItem li = new ListItem();
                                li.Text = $"{person.Nachname.ToUpper()}, {person.Name} ({person.Birthdate})";
                                li.Value = $"{person.ID}";

                                if (sa == person.SportArt || 
                                       person.isTrainer() ||
                                       person.isPhysiotherapeut())
                                    personList.Items.Add(li);
                            }
                        });

                } 
                catch (Exception ex)
                {
                    displayMannschaftName.InnerHtml = "Bitte wählen Sie eine Mannschaft";
                }
            }
        }

        protected void appendFilter(object sender, EventArgs e)
        {
            Mannschaft.OrderBy ob = 
                (Mannschaft.OrderBy)Enum.Parse(typeof(Mannschaft.OrderBy), this.dropDownSorting.SelectedValue);
            
            this.prepareData();
            this.loadMembers(ob);
        }

        private void createPersonEntry(Person p, string borderclasses)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl div =
                   new System.Web.UI.HtmlControls.HtmlGenericControl();
            Label roleType = new Label();
            Label personName = new Label();
            Label skills = new Label();

            div.Attributes.Add("class", $"member clear {borderclasses}");
            roleType.CssClass = "rolename";
            personName.CssClass = "personname";
            skills.CssClass = "rolename";

            roleType.Text = $"{Utils.basicClassName(p)} <br />";
            personName.Text = $"{p.Nachname}, {p.Name} <br />";
            skills.Text = $"{p.saySkills()}";

            div.Controls.Add(roleType);
            div.Controls.Add(personName);
            div.Controls.Add(skills);
            this.membersListContainer.Controls.Add(div);
        }

        protected void removeTeam(object sender, EventArgs e)
        {
            string team = teamsList.SelectedValue;
            ApplicationController.removeMannschaft(
                 ApplicationController.Mannschaften.Find(
                    m => m.Name == team),
                 GetUserFromSession().getSessionId());
            
            ApplicationController.TempMannschaft = new Mannschaft("_GENERATED");
            this.contentContainer.Visible = false;
            this.prepareData();
            this.loadMembers();
        }

        protected void changeTeam(object sender, EventArgs e)
        {
            int mannschaftId = Convert.ToInt32(this.Session[SESSION_MANNSCHAFT].ToString());
            Mannschaft mannschaft = ApplicationController
                .getMannschaften(GetUserFromSession().getSessionId())
                .Find(m => m.ID == mannschaftId);

            newTeamnameBox.Text = mannschaft.Name;
            newTeamtype.Text = mannschaft.SportArt.ToString();
            newTeamtype.SelectedValue = mannschaft.SportArt.ToString();

            ApplicationController.EditMode = true;
            newTeamBtn.Text = "Änderung anwenden";
            creationPanel.Visible = true;
        }

        protected void addPersonToMannschaft(object sender, EventArgs e)
        {
            personListButton.Enabled = false;

            string searchPattern = this.personList.SelectedValue;
            if (searchPattern != null || searchPattern != "")
            {
                try {  
                    int personId = Convert.ToInt32(searchPattern);
                    int selectedMannschaft = Convert.ToInt32(this.Session[SESSION_MANNSCHAFT]);

                    Person person = ApplicationController
                            .getUnsortedPersonen(GetUserFromSession().getSessionId())
                            .Find(p => personId == p.ID);
                    Mannschaft mannschaft = ApplicationController.
                        getMannschaften(GetUserFromSession().getSessionId())
                        .Find(m => m.ID == selectedMannschaft);

                    ApplicationController.addPersonToMannschaft(
                        person, mannschaft, GetUserFromSession().getSessionId());
                }
                catch (Exception exception)
                {
                    // nothing
                }
                finally { };
            }

            this.prepareData();
            this.loadMembers();
            personListButton.Enabled = true;
        }

        protected void removePersonFromMannschaft(object sender, EventArgs e)
        {
            personListDeleteButton.Enabled = false;
            DataRepository repo = new DataRepository();

            if (this.personListDelete.SelectedValue != null 
                || this.personListDelete.SelectedValue != "") {
                try
                {
                    int selectedId = Convert.ToInt32(this.personListDelete.SelectedValue);
                    int selectedMannschaft = Convert.ToInt32(this.Session[SESSION_MANNSCHAFT]);
                    Person person = ApplicationController
                        .getUnsortedPersonen(GetUserFromSession().getSessionId())
                        .Find(p => selectedId == p.ID);
                    Mannschaft mannschaft = ApplicationController.
                        getMannschaften(GetUserFromSession().getSessionId())
                        .Find(m => m.ID == selectedMannschaft);

                    repo.removePersonFromMannschaft(person, mannschaft);

                    this.prepareData();
                    this.loadMembers();
                }
                catch (Exception exception)
                {
                    // nothing
                }
            }

            personListDeleteButton.Enabled = true;
        }

        protected void createTeam(object sender, EventArgs e)
        {
            string teamname = this.newTeamnameBox.Text;
            string type = this.newTeamtype.SelectedValue;
            SportArt sa = (SportArt)Enum.Parse(typeof(SportArt), type);

            if (ApplicationController.EditMode)
            {
                int mannschaftId = Convert.ToInt32(
                    this.Session[SESSION_MANNSCHAFT].ToString());
                Mannschaft mannschaft = ApplicationController
                    .getMannschaften(GetUserFromSession().getSessionId())
                    .Find(m => m.ID == mannschaftId);

                DataRepository repo = new DataRepository();
                repo.enableSessionbasedQueries()
                    .setSession(GetUserFromSession().getSessionId())
                    .updateMannschaftSettings(
                        mannschaft.ID.ToString(), teamname, sa.ToString());
            }
            else
            {
                Mannschaft mannschaft = new Mannschaft();
                mannschaft
                    .name(teamname)
                    .sportArt(sa);

                ApplicationController
                    .addMannschaftIfNotExists(
                    mannschaft, GetUserFromSession().getSessionId());
            }

            ApplicationController.EditMode = false;
            newTeamBtn.Text = "Anlegen";

            this.prepareData();
            this.loadMembers();
        }

        protected void showCreationPanel(object sender, EventArgs e)
        {
            creationPanel.Visible = true;
            contentContainer.Visible = false;
        }

        protected void teamSelected(object sender, EventArgs e)
        {
            string teamNameSel = this.teamsList.SelectedValue;
            ApplicationController.getMannschaften(
                GetUserFromSession().getSessionId()).ForEach(m =>
            { 
                if(m.Name == teamNameSel)
                {
                    Mannschaft copy = m;
                    DataRepository repo = new DataRepository();
                    
                    copy.personen(repo.loadPersonen(m.ID.ToString()));
                    ApplicationController.TempMannschaft = copy;
                }
            });

            this.loadMembers();
            contentContainer.Visible = true;
            creationPanel.Visible = false;
        }

        protected void generateXML(object sender, EventArgs e)
        {
            ApplicationController.generateMannschaftenXML(
                GetUserFromSession().getSessionId());
        }
        #endregion

    }
}