using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string session = "undefined";

            if (this.Session["User"] != null)
            {
                authRequired.Visible = false;
                MainContent.Visible = true;

                session = (string)this.Session["User"];
            }
            else
            {
                authRequired.Visible = true;
                MainContent.Visible = false;
            }

            SessionText.InnerText = session;
        }

        protected void authenticate(object sender, EventArgs e)
        {
            string session = "undefined";

            session = $"{username.Text}{password.Text}";
            this.Session["User"] = session;

            authRequired.Controls.Clear();
            authRequired.InnerHtml = $"Laden Sie die Seite neu, um die Anmeldung abzuschließen.";
        }

    }
}