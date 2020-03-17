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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            OutputType.Value =
                ApplicationController.getFirstTupleMatch("typematcher");
        }

        protected void confirmPersonSelection(object sender, EventArgs e)
        {
            ApplicationController.buttonPersonTypeSelected(
                this.PersonSelectionType.SelectedValue);
            OutputType.Value =
                ApplicationController.getFirstTupleMatch("typematcher");
        }
        #endregion
    }
}