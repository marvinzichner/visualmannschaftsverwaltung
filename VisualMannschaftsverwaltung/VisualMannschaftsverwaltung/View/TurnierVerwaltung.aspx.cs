using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class TurnierVerwaltung : CustomPage
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        private string turnier;
        private string mannschaft;
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        public string Turnier { get => turnier; set => turnier = value; }
        public string Mannschaft { get => mannschaft; set => mannschaft = value; }
        #endregion

        #region Konstruktor
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;

            sectionCreateTurnier.Visible = false;
            sectionMapping.Visible = false;

            loadTurniere();
            loadDropdownSelections();
            this.disableAdminFeatures();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            sectionCreateTurnier.Visible = false;
            sectionMapping.Visible = false;
            renameSection.Visible = false;

            if (!IsPostBack)
            {
                loadTurniere();
                loadDropdownSelections();
                this.disableAdminFeatures();
            }
        }

        #region Worker
        private void disableAdminFeatures()
        {
            if (GetUserFromSession().isUser())
            {
                buttonConfirmSelection.Visible = false;
                buttonAddMapping.Visible = false;
                createNewTurnierButton.Visible = false;
                createNewMapping.Visible = false;
                buttonEditTurnier.Visible = false;
            }
        }

        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        public void onCellClick(Object sender, System.EventArgs e)
        {
            KeyValueList kv = new KeyValueList();
            kv.extractDataFromCombinedString(
                ((Button)sender).Attributes["data"].ToString());

            if (kv.getValueFromKeyValueList("method").Equals("deleteMannschaft"))
                ApplicationController.deleteMappingOfTurnierAndMannschaft(
                    kv.getValueFromKeyValueList("mannschaft"),
                    kv.getValueFromKeyValueList("turnier"));

            if (kv.getValueFromKeyValueList("method").Equals("deleteTurnier"))
                ApplicationController.deleteTurnierAndAllDependentEntities(
                    kv.getValueFromKeyValueList("turnier"));

            loadDropdownSelections();
            loadTurniere();
        }

        private HtmlTableCell createCellButton(string text, string classes, string data, bool enableDropdown = false, SportArt sa = SportArt.KEINE, int fkTurnier = -1)
        {
            HtmlTableCell tc = new HtmlTableCell();
            Button button = new Button();

            button.Text = text;
            button.Attributes.Add("data", data);
            button.Click += new EventHandler(onCellClick);
            
            tc.Attributes.Add("class", classes);

            if (enableDropdown)
            {
                DropDownList list = new DropDownList();
                Turnier turnier = new Turnier();
                int relativeCount = 0;

                list.Attributes.Add("data", data);
                list.ID = data;
                turnier = ApplicationController
                    .getTurniere(GetUserFromSession()
                    .getSessionId())
                    .Find(t => t.getId() == fkTurnier);

                ApplicationController
                    .getMannschaften(GetUserFromSession().getSessionId())
                    .FindAll(m => m.SportArt == sa)
                    .ForEach(mannschaft =>
                {
                    ListItem item = new ListItem();
                    item.Text = $"({mannschaft.ID.ToString()}) {mannschaft.Name}";
                    item.Attributes.Add("data", mannschaft.ID.ToString());

                    if (turnier.getMannschaften().Find(m => m.ID == mannschaft.ID) == null)
                    {
                        list.Items.Add(item);
                        relativeCount++;
                    }
                });

                Button dropdownButton = new Button();
                dropdownButton.Attributes.Add("data", data);
                dropdownButton.Text = "Hinzufügen";
                dropdownButton.Click += new EventHandler(this.addMannschaft);
                dropdownButton.Attributes.Add("style", "margin-right: 20px;");

                if(relativeCount > 0) { 
                    tc.Controls.Add(list);
                    tc.Controls.Add(dropdownButton);
                }
                else
                {
                    Label l = new Label();
                    l.Text = "Keine Mannschaft gefunden";
                    l.Attributes.Add("style", "margin-right: 20px;");

                    tc.Controls.Add(l);
                }
            }

            tc.Controls.Add(button);
            return tc;
        }

        private void loadTurniere()
        {
            storedTurniere.Rows.Clear();

            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Globale Id", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Name", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Typ", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Aktionen", "tablecell cellHead"));
            storedTurniere.Rows.Add(trHead);

            ApplicationController.getTurniere(GetUserFromSession().getSessionId()).ForEach(turnier => {
                HtmlTableRow tr = new HtmlTableRow();
                tr.Attributes.Add("class", "cellBodyHover");
                tr.Cells.Add(createCell($"{turnier.getId()}", "tablecell cellBody"));
                tr.Cells.Add(createCell($"{turnier.getName()}", "tablecell cellBody"));
                tr.Cells.Add(createCell($"{turnier.getType()}", "tablecell cellBody"));

                // add Turnier to Dropdown Menue


                if (GetUserFromSession().isAdmin())
                {
                    tr.Cells.Add(createCellButton(
                        $"Turnier und Mannschaften löschen",
                        "tablecell cellBody",
                        $"method=deleteTurnier#turnier={turnier.getId()}",
                        true,
                        turnier.getType(),
                        turnier.getId()));
                }

                if (GetUserFromSession().isUser())
                    tr.Cells.Add(createCell(
                        $"",
                        "tablecell cellBody"));

                storedTurniere.Rows.Add(tr);

                int len = turnier.getMannschaften().Count;
                int count = 1;

                if(len > 0)
                    turnier.getMannschaften().ForEach(mannschaft =>
                    {
                        string cellClass = "cellBodyHover cellEnd";
                        HtmlTableRow trEx = new HtmlTableRow();

                        string checkupSameTypeClass = "";
                        if (!turnier.getType().Equals(mannschaft.SportArt))
                            checkupSameTypeClass = "notMatching";

                        if (len != count)
                            cellClass = "cellBodyHover";
                        trEx.Attributes.Add("class", cellClass);
                        trEx.Cells.Add(createCell($"&emsp; {mannschaft.ID.ToString()}", "tablecell cellReadOnly cellBody"));
                        trEx.Cells.Add(createCell($"{mannschaft.Name}", "tablecell cellReadOnly cellBody"));
                        trEx.Cells.Add(createCell($"{mannschaft.SportArt}", $"tablecell cellReadOnly {checkupSameTypeClass} cellBody"));

                        if (GetUserFromSession().isAdmin())
                            trEx.Cells.Add(createCellButton(
                                $"Mannschaft entfernen",
                                "tablecell cellInteraction cellBody",
                                $"method=deleteMannschaft#mannschaft={mannschaft.ID}#turnier={turnier.getId()}"));

                        if (GetUserFromSession().isUser())
                            tr.Cells.Add(createCell(
                                $"",
                                "tablecell cellBody"));

                        storedTurniere.Rows.Add(trEx);

                        count++;
                    });            
            });
        }

        private void loadDropdownSelections()
        {
            dropdownMannschaften.Items.Clear();
            dropdownTurniere.Items.Clear();
            turnierDropdownList.Items.Clear();

            ApplicationController.getMannschaften(GetUserFromSession().getSessionId()).ForEach(mannschaft =>
            {
                ListItem item = new ListItem();
                item.Text = $"{mannschaft.ID.ToString()}: {mannschaft.Name} [{mannschaft.SportArt.ToString()}]";
                item.Value = mannschaft.ID.ToString();

                dropdownMannschaften.Items.Add(item);
            });

            ApplicationController.getTurniere(GetUserFromSession().getSessionId()).ForEach(turnier =>
            {
                ListItem item = new ListItem();
                item.Text = $"{turnier.getId().ToString()}: {turnier.getName()} [{turnier.getType().ToString()}]";
                item.Value = turnier.getId().ToString();

                dropdownTurniere.Items.Add(item);
            });

            ApplicationController.getTurniere(GetUserFromSession().getSessionId()).ForEach(turnier =>
            {
                ListItem item = new ListItem();
                item.Text = $"{turnier.getName()} [{turnier.getType().ToString()}]";
                item.Value = turnier.getId().ToString();

                turnierDropdownList.Items.Add(item);
            });
        }

        public void openCreationMode(Object sender, EventArgs e) {
            sectionTurnierlist.Visible = false;
            sectionCreateTurnier.Visible = true;
            sectionMapping.Visible = false;
        }

        public void cancelAction(Object sender, EventArgs e)
        {
            sectionTurnierlist.Visible = true;
            sectionCreateTurnier.Visible = false;
            sectionMapping.Visible = false;
            renameSection.Visible = false;
        }

        public void openMappingMode(Object sender, EventArgs e)
        {
            sectionTurnierlist.Visible = false;
            sectionCreateTurnier.Visible = false;
            sectionMapping.Visible = true;
        }

        public void createNewTurnier(Object sender, EventArgs e)
        {
            SportArt sportArt = (SportArt)Enum.Parse(typeof(SportArt), turnierTypeField.Text);

            ApplicationController.createNewTurnier(
                turnierNameField.Text,
                sportArt,
                GetUserFromSession().getSessionId());

            this.loadTurniere();
            this.cancelAction(sender, e);
        }

        public int extractIdFromInterpolatedString(string s)
        {
            try { 
                int extractedResult = 0;
                string[] extract = s.Split(':');
                extractedResult = Convert.ToInt32(extract[0]);
                return extractedResult;
            } catch (Exception e) {
                return -1;
            }
        }

        public void doMapping(Object sender, EventArgs e)
        {
            ApplicationController.addMappingOfTurnierAndMannschaft(
                dropdownMannschaften.SelectedValue,
                dropdownTurniere.SelectedValue);

            this.loadTurniere();
            this.loadDropdownSelections();
            this.cancelAction(sender, e);
        }

        public void openRenameMode(Object sender, EventArgs e)
        {
            renameSection.Visible = true;
        }

        public void doTurnierRenaming(Object sender, EventArgs e)
        {
            try {
                string newName = ApplicationContext.disarmHijacking(newTurnierName.Text);
                int selectedTurnier = Utils.convertToInteger32(
                    turnierDropdownList.SelectedValue.ToString());
                Turnier turnier = ApplicationController.getTurniere(GetUserFromSession().getSessionId())
                    .Find(t => t.getId() == selectedTurnier);

                turnier.setName(newName);
                ApplicationController.updateTurnier(turnier);
            }
            catch (Exception exception)
            {

            }

            this.loadTurniere();
            this.loadDropdownSelections();
            this.cancelAction(sender, e);
        }

        public void addMannschaft(Object sender, EventArgs e)
        {
            KeyValueList kv = new KeyValueList();
            Button obj = (Button)sender;

            string btnData = obj.Attributes["data"].ToString();
            kv.extractDataFromCombinedString(btnData);

            string value = Request[$"ctl00$MainContent${btnData}"];
            string[] fragments = value.Split(')');
            string mannschaftId = fragments[0].Substring(1);
            string turnierId = kv.getValueFromKeyValueList("turnier");

            ApplicationController
                .addMappingOfTurnierAndMannschaft(mannschaftId, turnierId);

            loadTurniere();
            loadDropdownSelections();
        }
        #endregion
    }
}