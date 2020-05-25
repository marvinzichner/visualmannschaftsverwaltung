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
                session = (string)this.Session["User"];
            }
            else
            {
                session = Guid.NewGuid().ToString();
                this.Session["User"] = session;
            }

            SessionText.InnerText = session;
        }

    }
}