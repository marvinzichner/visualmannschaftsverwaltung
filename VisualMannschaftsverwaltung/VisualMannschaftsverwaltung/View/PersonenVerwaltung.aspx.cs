using System;
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
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void confirmPersonSelection(object sender, EventArgs e)
        {
            Console.WriteLine("hello!");
        }
        #endregion
    }
}