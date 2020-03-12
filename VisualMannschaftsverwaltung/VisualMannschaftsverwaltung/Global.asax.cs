using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace VisualMannschaftsverwaltung
{
    public class Global : HttpApplication
    {
        static private ApplicationController _applicationController;
        static public ApplicationController ApplicationController { get => _applicationController; set => _applicationController = value; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationController = new ApplicationController();
        }
    }
}