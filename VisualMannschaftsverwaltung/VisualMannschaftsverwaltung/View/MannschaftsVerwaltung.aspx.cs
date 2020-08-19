using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class MannschaftsVerwaltung : CustomPage
    {
        #region Eigenschaften
        private ApplicationController applicationController;
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

            this.checkAlerts();
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
                createNewTeam.Visible = false;
                teamsEdit.Visible = false;
                personList.Visible = false;
                personListButton.Visible = false;
                personListDelete.Visible = false;
                personListDeleteButton.Visible = false;
            }
        }

        protected void checkAlerts()
        {
            if (!ApplicationController.DatabaseOk)
            {
                configMismatch.Visible = true;
            }
        }

        protected void prepareData()
        {
            //Dropdown
            teamsList.Items.Clear();
            ApplicationController.getMannschaften(
               GetUserFromSession().getSessionId()).ForEach(m =>
            {
                ListItem li = new ListItem();
                li.Text = $"{m.Name} ({m.SportArt.ToString()})";
                li.Value = m.Name;
                teamsList.Items.Add(li);
            });
        }
        protected void loadMembers(Mannschaft.OrderBy ob = Mannschaft.OrderBy.UNSORTED) {
            //Team Members
            ApplicationController.loadPersonenFromRepository(GetUserFromSession().getSessionId());
       
            membersListContainer.Controls.Clear();
            personListDelete.Items.Clear();
            this.teamName.InnerHtml = ApplicationController.TempMannschaft.Name;
            ApplicationController.TempMannschaft
                .rule(ob)
                .enableGroupSort()
                .applySearchPattern()
                    .ForEach(p =>
            {
                createPersonEntry(p, "");

                ListItem li = new ListItem();
                li.Text = $"{p.Nachname}, {p.Name} ({p.Birthdate}) [{basetypeName(p)}]";
                li.Value = $"{p.Name}{p.Nachname}-{p.Birthdate}";
                personListDelete.Items.Add(li);
            });

            personList.Items.Clear();
            ApplicationController.getAvailablePersonen().ForEach(p =>
            {
                ListItem li = new ListItem();
                li.Text = $"{p.Nachname}, {p.Name} ({p.Birthdate}) [{basetypeName(p)}]";
                li.Value = $"{p.Name}{p.Nachname}-{p.Birthdate}";
                personList.Items.Add(li);
            });
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

            roleType.Text = $"{basetypeName(p)} <br />";
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
            newTeamnameBox.Text = ApplicationController.TempMannschaft.Name;
            newTeamtype.Text = ApplicationController.TempMannschaft.SportArt.ToString();
            newTeamtype.SelectedValue = ApplicationController.TempMannschaft.SportArt.ToString();
            ApplicationController.EditMode = true;
            newTeamBtn.Text = "Änderung anwenden";
        }

        protected void addPersonToMannschaft(object sender, EventArgs e)
        {
            string searchPattern = this.personList.SelectedValue;
            ApplicationController.getAvailablePersonen().ForEach(p => { 
                if (searchPattern == $"{p.Name}{p.Nachname}-{p.Birthdate}")
                {
                    Mannschaft copy = ApplicationController.TempMannschaft;
                    ApplicationController.TempMannschaft.Personen.Add(p);
                    ApplicationController.Mannschaften.Remove(copy);
                    ApplicationController.Mannschaften.Add(ApplicationController.TempMannschaft);
                    ApplicationController.addPersonToMannschaft(p, copy, GetUserFromSession().getSessionId());
                }
            });

            this.prepareData();
            this.loadMembers();
        }

        protected void removePersonFromMannschaft(object sender, EventArgs e)
        {
            Mannschaft work = ApplicationController.TempMannschaft;
            DataRepository repo = new DataRepository();
            string searchPattern = this.personListDelete.SelectedValue;
            Person person = work.Personen.Find(p => searchPattern == $"{p.Name}{p.Nachname}-{p.Birthdate}");

            work.Personen.Remove(person);
            repo.removePersonFromMannschaft(person, work);
           
            ApplicationController.Mannschaften.Remove(ApplicationController.TempMannschaft);
            ApplicationController.Mannschaften.Add(work);

            this.prepareData();
            this.loadMembers();
        }

        protected void createTeam(object sender, EventArgs e)
        {
            string teamname = this.newTeamnameBox.Text;
            string type = this.newTeamtype.SelectedValue;
            SportArt sa = (SportArt)Enum.Parse(typeof(SportArt), type);

            if (ApplicationController.EditMode)
            {
                ApplicationController.TempMannschaft.Name = teamname;
                ApplicationController.TempMannschaft.SportArt = sa;

                DataRepository repo = new DataRepository();
                repo.enableSessionbasedQueries()
                    .setSession(GetUserFromSession().getSessionId())
                    .updateMannschaftSettings(
                        ApplicationController.TempMannschaft.ID.ToString(), teamname, sa.ToString());
            }
            else
            {
                Mannschaft mannschaft = new Mannschaft();
                mannschaft
                    .name(teamname)
                    .sportArt(sa);

                ApplicationController.addMannschaftIfNotExists(mannschaft, GetUserFromSession().getSessionId());
            }

            ApplicationController.EditMode = false;
            newTeamBtn.Text = "Anlegen";
            //ApplicationController.TempMannschaft = mannschaft;
            this.prepareData();
            this.loadMembers();
        }

        private string basetypeName(object o)
        {
            string type = o.GetType().ToString();
            string[] segment = type.Split('.');
            return segment[segment.Length - 1];
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
        }

        protected void generateXML(object sender, EventArgs e)
        {
            ApplicationController.generateMannschaftenXML();
        }
        #endregion

    }
}