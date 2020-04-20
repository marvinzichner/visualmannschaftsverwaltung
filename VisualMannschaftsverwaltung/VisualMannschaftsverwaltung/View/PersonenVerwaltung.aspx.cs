using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class PersonenVerwaltung : System.Web.UI.Page
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        private string _targetPersonType;
        private int _selectedPerson;
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        public string TargetPersonType { get => _targetPersonType; set => _targetPersonType = value; }
        public int SelectedPerson { get => _selectedPerson; set => _selectedPerson = value; }
        #endregion

        #region Konstruktor
        #endregion

        #region Worker
        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fieldVorname.Enabled = false;
            fieldNachname.Enabled = false;
            fieldBirthdate.Enabled = false;
            SelectedPerson = -1;

            this.loadPersonen();
        }

        protected void createNewPerson(object sender, EventArgs e)
        {
            List<InputValidationException> violationList = new List<InputValidationException>();
            this.errorMessages.InnerHtml = "";
            string selectedType = ApplicationController.getFirstTupleMatch("typematcher");
            string fieldVorname = this.fieldVorname.Text;
            string fieldNachname = this.fieldNachname.Text;
            string fieldBirthdate = this.fieldBirthdate.Text;

            try
            {
                Type targetType = Type.GetType("VisualMannschaftsverwaltung." + selectedType);
                Person reflectedInstance = (Person)Activator.CreateInstance(targetType);
                
                reflectedInstance
                    .name(fieldVorname)
                    .nachname(fieldNachname)
                    .birthdate(fieldBirthdate);

                List<KeyValuePair<string, string>> attr = new List<KeyValuePair<string, string>>();
                getAllGenericAttributes().ForEach(attribute =>
                {
                    attr.Add(new KeyValuePair<string, string>(
                        attribute, Request["ctl00$MainContent$generatedField-attribute-" + attribute]));
                });

                if (selectedType == "FussballSpieler")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.FUSSBALL)
                            .toFussballSpieler()
                            .spielSiege(
                                Utils.convertToInt(attr.Find(x => x.Key == "SpielSiege").Value, 0, "SpielSiege"))
                            .isLeftFeet(
                                Utils.convertToBool(attr.Find(x => x.Key == "IsLeftFeet").Value, false, "IsLeftFeet"))
                    );
                }
                else if (selectedType == "HandballSpieler")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.HANDBALL)
                            .toHandballSpieler()
                            .spielSiege(
                                Utils.convertToInt(attr.Find(x => x.Key == "SpielSiege").Value, 0, "SpielSiege"))
                            .isLeftHand(
                                Utils.convertToBool(attr.Find(x => x.Key == "IsLeftHand").Value, false, "IsLeftHand"))
                    );
                }
                else if (selectedType == "TennisSpieler")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.TENNIS)
                            .toTennisSpieler()
                            .spielSiege(
                                Utils.convertToInt(attr.Find(x => x.Key == "SpielSiege").Value, 0, "SpielSiege"))
                            .isLeftHand(
                                Utils.convertToBool(attr.Find(x => x.Key == "IsLeftHand").Value, false, "IsLeftHand"))
                    );
                }
                else if (selectedType == "Trainer")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.KEINE)
                            .toTrainer()
                            .hasLicense(
                               Utils.convertToBool(attr.Find(x => x.Key == "HasLicense").Value, false, "HasLicense"))
                    );
                }
                else if (selectedType == "Physiotherapeut")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.KEINE)
                            .toPhysiotherapeut()
                            .hasLicense(
                               Utils.convertToBool(attr.Find(x => x.Key == "HasLicense").Value, false, "HasLicense"))
                    );
                }
                else
                {
                    ApplicationController.addPerson(reflectedInstance);
                }
            }
            catch (InputValidationException ie)
            {
                violationList.Add(ie);
            }
            catch (Exception ex)
            {
                errorMessages.InnerHtml =
                   $"<b>Unbekannter Fehler aufgetreten</b><br>{ex.Message}";
            }

            violationList.ForEach(violation =>
            {
                errorMessages.InnerHtml =
                   $"<b>Eingabeüberprüfung</b><br>{violation.HumanReadable}.<br>" +
                   $"<i>Eingabefeld:</i>&emsp;{violation.Field}<br>" +
                   $"<i>Pattern:</i>&emsp;{violation.Pattern}<br><br>";
            });

            this.fieldVorname.Text = "";
            this.fieldNachname.Text = "";
            this.fieldBirthdate.Text = "";
            dynamicPersonList.Controls.Clear();
            dynamicFlow.Controls.Clear();
            staticPersonListHeader.Controls.Clear();
            this.loadPersonen();
        }

        protected List<string> getAllGenericAttributes()
        {
            List<string> tableKeys = new List<string>();
            List<Person> list = new List<Person>();

            tableKeys.Add("Vorname");
            tableKeys.Add("Nachname");
            tableKeys.Add("Geburtsdatum");
            list.Add(new FussballSpieler());
            list.Add(new HandballSpieler());
            list.Add(new TennisSpieler());
            list.Add(new Trainer());
            list.Add(new Physiotherapeut());

            list.ForEach(person =>
            {
                person.getGenericAttribues().ForEach(attribute =>
                {
                    if (!tableKeys.Contains(attribute))
                    {
                        tableKeys.Add(attribute);
                    }
                });
            });

            return tableKeys;
        }

        protected List<string> getAllAttributes()
        {
            List<string> tableKeys = new List<string>();
            tableKeys.Add("Vorname");
            tableKeys.Add("Nachname");
            tableKeys.Add("Geburtsdatum");
            tableKeys.Add("Sportart");
            ApplicationController.Personen.ForEach(person =>
            {
                person.getGenericAttribues().ForEach(attribute =>
                {
                    if (!tableKeys.Contains(attribute))
                    {
                        tableKeys.Add(attribute);
                    }
                });
            });
            tableKeys.Add("Aktionen");

            return tableKeys;
        }

        protected void loadPersonen()
        {
            dynamicPersonList.Controls.Clear();
            dynamicFlow.Controls.Clear();
            staticPersonListHeader.Controls.Clear();

            int percent = 100 / getAllAttributes().Count;
            getAllAttributes().ForEach(attribute =>
            {
                Label l = new Label();

                l.Text = attribute;
                l.Attributes.CssStyle.Add("width", percent + "%");
                l.Attributes.CssStyle.Add("float", "left");
                l.Attributes.CssStyle.Add("border-bottom", "3px solid #e6e6e6");
                staticPersonListHeader.Controls.Add(l);
            });

            int cnt = 0;
            ApplicationController.getPersonen(
                ApplicationController.StorageOrderBy.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value,
                ApplicationController.StorageSearchTerm.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value).ForEach(person =>
            {
                getAllAttributes().ForEach(attribute =>
                {
                    Label l = new Label();
                    l.Attributes.CssStyle.Add("width", percent + "%");
                    l.Attributes.CssStyle.Add("float", "left");
                    l.Attributes.CssStyle.Add("border-bottom", "1px solid #e6e6e6");
                    string cellContent = "&emsp;";

                    if (attribute == "Vorname")
                    {
                        cellContent = person.Name;
                    }
                    else if (attribute == "Nachname")
                    {
                        cellContent = person.Nachname;
                    }
                    else if (attribute == "Geburtsdatum")
                    {
                        cellContent = person.Birthdate;
                    }
                    else if (attribute == "Sportart")
                    {
                        cellContent = person.SportArt.ToString();
                    }
                    else if (attribute == "Aktionen")
                    {
                        cellContent = "";
                        l.Attributes.CssStyle.Add("width","0%");

                        Button b = new Button();
                        b.Attributes.CssStyle.Add("width", percent + "%");
                        b.Attributes.CssStyle.Add("float", "left");
                        b.Attributes.CssStyle.Add("border", "none");
                        b.Attributes.CssStyle.Add("background", "white");
                        b.Attributes.CssStyle.Add("border-bottom", "1px solid #e6e6e6");
                        b.ID = cnt.ToString();
                        b.Click += new EventHandler(this.testClickEvent);
                        b.Text = "auswählen";
                        dynamicPersonList.Controls.Add(b);

                        cnt += 1;
                    }
                    else
                    {
                        try {
                            Type personType = person.GetType();
                            PropertyInfo pi = personType.GetProperty(attribute);
                            cellContent = pi.GetValue(person).ToString();
                        }
                        catch (Exception e)
                        {
                            cellContent = "&emsp;";
                        }
                    }

                    l.Text = cellContent;
                    dynamicPersonList.Controls.Add(l);
                });
            });
        }

        void testClickEvent(Object sender, EventArgs e)
        {
            Button b = (Button)sender;
            String id = b.ID;
            int pid = Convert.ToInt32(id);

            ApplicationController.setTuple("personenverwaltung.pid", pid.ToString());
            deleteButton.Visible = true;
            editButton.Visible = true;
        }

        protected void removeSelectedPerson(object sender, EventArgs e)
        {
            Person p = new Trainer();
            List<Person> list = ApplicationController.getPersonen(
                ApplicationController.StorageOrderBy.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value,
                ApplicationController.StorageSearchTerm.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value);
            p = list[Convert.ToInt32(
                ApplicationController.getFirstTupleMatch("personenverwaltung.pid"))];

            ApplicationController.Personen.Remove(p);
            deleteButton.Visible = false;
            editButton.Visible = false;
            this.loadPersonen();
        }

        protected void editSelectedPerson(object sender, EventArgs e)
        {
            Object p = new Trainer();
            List<Person> list = ApplicationController.getPersonen(
                ApplicationController.StorageOrderBy.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value,
                ApplicationController.StorageSearchTerm.FindLast(
                    x => x.Key == "PERSONENVERWALTUNG").Value);
            p = list[Convert.ToInt32(
                ApplicationController.getFirstTupleMatch("personenverwaltung.pid"))];

            PersonSelectionTypeDD.SelectedValue = p.GetType().ToString();
            this.confirmPersonSelection(sender, e);
        }

        protected void confirmPersonSelection(object sender, EventArgs e)
        {
            string selection = this.PersonSelectionTypeDD.SelectedValue;
            ApplicationController.buttonPersonTypeSelected(
                this.PersonSelectionTypeDD.SelectedValue);

            try { 
                Type reflectedPerson = 
                    Type.GetType("VisualMannschaftsverwaltung." + selection);
                Person reflectedInstance = 
                    (Person) Activator.CreateInstance(reflectedPerson);

                reflectedInstance.getGenericAttribues().ForEach(genericType =>
                {
                    dynamicFlow.Controls.Add(
                        generateNewField("attribute-" + genericType, genericType));
                });

                fieldVorname.Enabled = true;
                fieldNachname.Enabled = true;
                fieldBirthdate.Enabled = true;
            } 
            catch (Exception)
            {
                dynamicFlow.Controls.Add(
                    generateNewField("attribute-error", "reflection failed: " + selection));
            }
        }

        protected Control generateNewField(string id, string text)
        {
            Control c = new Control();
            Label l = new Label();
            TextBox tb = new TextBox();
            System.Web.UI.HtmlControls.HtmlGenericControl div = 
                new System.Web.UI.HtmlControls.HtmlGenericControl();

            l.Text = text;
            l.CssClass = "listLabelFlow";
            tb.CssClass = "listField";
            tb.ID = "generatedField-" + id;

            div.Controls.Add(l);
            div.Controls.Add(tb);
            c.Controls.Add(div);
   
            return c;
        }
        #endregion

        protected void dropDownSortingChanged(object sender, EventArgs e)
        {
            string sortingRule = this.dropDownSorting.SelectedValue;
            try
            {
                Mannschaft.OrderBy parsed = (Mannschaft.OrderBy)Enum.Parse(typeof(Mannschaft.OrderBy), sortingRule);

                ApplicationController.StorageOrderBy.Add(
                    new KeyValuePair<string, Mannschaft.OrderBy>("PERSONENVERWALTUNG", parsed));
                ApplicationController.StorageSearchTerm.Add(
                   new KeyValuePair<string, Mannschaft.SearchTerm>("PERSONENVERWALTUNG", Mannschaft.SearchTerm.ALL));
            } 
            catch(Exception exception)
            {
                errorMessages.InnerHtml =
                   $"<b>Reflection failed</b><br>'{sortingRule}' did not match any sorting type.";
            }
 
            dynamicPersonList.Controls.Clear();
            dynamicFlow.Controls.Clear();
            staticPersonListHeader.Controls.Clear();
            this.loadPersonen();
        }
    }
}