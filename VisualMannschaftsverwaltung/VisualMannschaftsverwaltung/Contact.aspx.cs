using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RepositorySettings settings = new RepositorySettings();
            ApplicationController application = Global.ApplicationController;
            DataRepository repo = new DataRepository();

            string currentVersion = repo.getLatestDatabaseVersion();

            infoDatabase.InnerHtml = settings.Database;
            infoServer.InnerHtml = settings.Server;
            infoUsername.InnerHtml = settings.Uid;
            infoStatus.InnerHtml = application.DatabaseOk.ToString();
            infoScheme.InnerHtml = currentVersion;
        }
    }
}