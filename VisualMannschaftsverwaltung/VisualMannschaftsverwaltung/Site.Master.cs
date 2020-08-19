using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            if (this.Session["UserAuth"] != null)
            {
                authRequired.Visible = false;
                auth.Visible = true;
                MainContent.Visible = true;

                AuthenticatedUser user = (AuthenticatedUser)this.Session["UserAuth"];
                session = $"{user.getUsername()} ({user.getSessionId()})";
                displayName.InnerText = $"{user.getUsername()} ({user.getAuthenticatedRole().ToString()})";
            }
            else
            {
                authRequired.Visible = true;
                auth.Visible = false;
                MainContent.Visible = false;
            }

            SessionText.InnerText = session;
        }

        protected void authenticate(object sender, EventArgs e)
        {
            string session = "undefined";

            if(username.Text != "" && password.Text != "") {
                DataRepository repo = new DataRepository();
                if (repo.checkCredentials(username.Text, password.Text)) {
                    string[] shortUsername = username.Text.Split('.');

                    session = $"{shortUsername[0]}";
                    byte[] encodedPassword = new UTF8Encoding().GetBytes(session);
                    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    string encoded = BitConverter.ToString(hash)
                        .Replace("-", string.Empty)
                        .ToLower();

                    AuthenticatedUser user = new AuthenticatedUser();
                    user.setSessionId(encoded)
                        .setUsername(username.Text)
                        .setAuthenticatedRole(repo.getRoleFromUsername(username.Text));

                    this.Session["UserAuth"] = user;

                    authRequired.Controls.Clear();
                    authRequired.InnerHtml = $"Laden Sie die Seite neu, um die Anmeldung abzuschließen.";
                    Page.Response.Redirect("/", true);
                }
            }
        }

        protected void destroySession(object sender, EventArgs e)
        {
            this.Session["UserAuth"] = null;

            auth.Controls.Clear();
            auth.InnerHtml = $"Laden Sie die Seite neu, um die Abmeldung abzuschließen.";
            Page.Response.Redirect("/", true);
        }
    }
}