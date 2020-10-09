using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                shortcuts.Visible = true;
                pleaseNote.Visible = false;

                AuthenticatedUser user = (AuthenticatedUser)this.Session["UserAuth"];
                session = $"{user.getUsername()} ({user.getSessionId()})";
                displayName.InnerText = $"{user.getUsername()} ({user.getAuthenticatedRole().ToString()})";
                if (user.isUser()) pleaseNote.Visible = true;
            }
            else
            {
                authRequired.Visible = true;
                auth.Visible = false;
                MainContent.Visible = false;
                shortcuts.Visible = false;
            }

            SessionText.InnerText = session;
            this.checkLifecycle();
        }

        private void checkLifecycle()
        {
            DataRepository repo = new DataRepository();
            if (!repo.databaseIsConnectedAndReady())
            {
                navigationBar.Visible = false;
                bodyContainer.Visible = false;
                connectionTimeout.Visible = true;
            } 
            else
            {
                navigationBar.Visible = true;
                bodyContainer.Visible = true;
                connectionTimeout.Visible = false;
            }
        }

        protected void impersonate(object sender, EventArgs e)
        {
            Button b = sender as Button;
            string user = b.Attributes["USER"].ToString();
            string pass = b.Attributes["PASS"].ToString();

            username.Text = user;
            password.Text = pass;
            authenticate(sender, e);
        }

        protected void authenticate(object sender, EventArgs e)
        {
            string session = "undefined";

            if(!username.Text.Equals("") && !password.Text.Equals("")) {
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
                    Page.Response.Redirect(HttpContext.Current.Request.Url.ToString(), true);
                }
                else
                {
                    invalid.Visible = true;
                }
            } 
            else
            {
                enterPassword.Visible = true;
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