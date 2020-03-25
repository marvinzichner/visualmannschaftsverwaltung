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
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        public string TargetPersonType { get => _targetPersonType; set => _targetPersonType = value; }
        #endregion

        #region Konstruktor
        #endregion

        #region Worker
        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;

            this.loadPersonen();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            fieldVorname.Enabled = false;
            fieldNachname.Enabled = false;
            fieldBirthdate.Enabled = false;

            this.loadPersonen();
        }

        protected void createNewPerson(object sender, EventArgs e)
        {
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

                ApplicationController.addPerson(reflectedInstance);
            }
            catch (Exception)
            {
                dynamicFlow.Controls.Add(
                    generateNewField("attribute-error", "reflection failed: " + selectedType));
            }
        }

        protected void loadPersonen()
        {
            ApplicationController.Personen.ForEach(person =>
            {
                Control c = new Control();
                Label l = new Label();

                l.Text = person.Name + " " + person.Nachname;
                c.Controls.Add(l);

                dynamicPersonList.Controls.Add(c);
            });
        }
        protected void confirmPersonSelection(object sender, EventArgs e)
        {
            string selection = this.PersonSelectionType.SelectedValue;
            ApplicationController.buttonPersonTypeSelected(
                this.PersonSelectionType.SelectedValue);

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
    }
}