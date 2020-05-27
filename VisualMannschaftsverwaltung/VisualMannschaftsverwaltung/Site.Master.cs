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

            if (this.Session["User"] != null)
            {
                authRequired.Visible = false;
                auth.Visible = true;
                MainContent.Visible = true;

                session = $"{(string)this.Session["Username"]} ({(string)this.Session["User"]})";
                displayName.InnerText = (string)this.Session["Username"];
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

            session = $"{username.Text}{password.Text}";
            byte[] encodedPassword = new UTF8Encoding().GetBytes(session);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
                .Replace("-", string.Empty)
                .ToLower();

            this.Session["User"] = encoded;
            this.Session["Username"] = username.Text;

            authRequired.Controls.Clear();
            authRequired.InnerHtml = $"Laden Sie die Seite neu, um die Anmeldung abzuschließen.";
            Page.Response.Redirect("/", true);
        }

        protected void destroySession(object sender, EventArgs e)
        {
            this.Session["User"] = null;
            this.Session["Username"] = null;
            auth.Controls.Clear();
            auth.InnerHtml = $"Laden Sie die Seite neu, um die Abmeldung abzuschließen.";
            Page.Response.Redirect("/", true);
        }
    }
}