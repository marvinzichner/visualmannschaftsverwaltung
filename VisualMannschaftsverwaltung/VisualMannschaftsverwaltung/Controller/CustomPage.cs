using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public partial class CustomPage : System.Web.UI.Page
    {

        public AuthenticatedUser GetUserFromSession()
        {
            AuthenticatedUser user = new AuthenticatedUser();
            if (this.Session["UserAuth"] != null)
                user = (AuthenticatedUser)this.Session["UserAuth"];
            return user;
        }
    }
}