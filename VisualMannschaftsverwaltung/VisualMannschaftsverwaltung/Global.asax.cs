﻿using System;
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
        static private Property _property;

        static public ApplicationController ApplicationController { get => _applicationController; set => _applicationController = value; }
        public static Property PropertyManager { get => _property; set => _property = value; }

        void Application_Start(object sender, EventArgs e)
        {
            // Code, der beim Anwendungsstart ausgeführt wird
            PropertyManager = new Property();
            PropertyManager.importProperties();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if(ApplicationContext.databaseIsReady())
                ApplicationContext.createDatabaseContext();
            ApplicationController = new ApplicationController();
            ApplicationContext.createFilesystemStructure();
            ApplicationController.prepareTuple();
            if (ApplicationContext.databaseIsReady())
            { 
                ApplicationController.loadPersonenFromRepository();
                ApplicationController.loadMannschaftenFromRepository("");
            }
        }
    }
}