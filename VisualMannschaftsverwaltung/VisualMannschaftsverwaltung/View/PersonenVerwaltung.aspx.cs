﻿using System;
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

                List<KeyValuePair<string, string>> attr = new List<KeyValuePair<string, string>>();
                getAllAttributes().ForEach(attribute =>
                {
                    attr.Add(new KeyValuePair<string, string>(attribute, Request["attribute-" + attribute]));
                });

                if (selectedType == "FussballSpieler")
                {
                    ApplicationController.addPerson(
                        reflectedInstance
                            .sportArt(SportArt.FUSSBALL)
                            .toFussballSpieler()
                            .spielSiege(25)
                            .isLeftFeet(
                               false)
                    );
                }
                else
                {
                    ApplicationController.addPerson(reflectedInstance);
                }
            }
            catch (Exception)
            {
                dynamicFlow.Controls.Add(
                    generateNewField("attribute-error", "reflection failed: " + selectedType));
            }
        }

        protected List<string> getAllAttributes()
        {
            List<string> tableKeys = new List<string>();
            tableKeys.Add("Vorname");
            tableKeys.Add("Nachname");
            tableKeys.Add("Geburtsdatum");
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

            return tableKeys;
        }

        protected void loadPersonen()
        {
            

            int percent = 100 / getAllAttributes().Count;
            getAllAttributes().ForEach(attribute =>
            {
                Label l = new Label();

                l.Text = attribute;
                l.Attributes.CssStyle.Add("width", percent + "%");
                l.Attributes.CssStyle.Add("float", "left");
                staticPersonListHeader.Controls.Add(l);
            });

            ApplicationController.Personen.ForEach(person =>
            {
                getAllAttributes().ForEach(attribute =>
                {
                    Label l = new Label();
                    l.Attributes.CssStyle.Add("width", percent + "%");
                    l.Attributes.CssStyle.Add("float", "left");
                    string cellContent = "---";

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
                    else
                    {
                        try {
                            Type personType = person.GetType();
                            PropertyInfo pi = personType.GetProperty(attribute);
                            cellContent = pi.GetValue(person).ToString();
                        }
                        catch (Exception e)
                        {
                            cellContent = e.Message;
                        }
                    }


                    l.Text = cellContent;
                    dynamicPersonList.Controls.Add(l);
                });
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