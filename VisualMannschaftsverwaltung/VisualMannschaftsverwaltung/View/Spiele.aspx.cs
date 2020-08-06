using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class Spiele : System.Web.UI.Page
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        #endregion

        #region Konstruktor
        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;

            loadAllData();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region woker
        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        public void loadAllData()
        {
            presenterTable.Rows.Clear();

            ApplicationController.getTurniere(getOrCreateSession()).ForEach(turnier =>
            {

                HtmlTableRow trHead = new HtmlTableRow();
                trHead.Cells.Add(createCell($"{turnier.getName()}", "tablecell cellHead"));

                turnier.getMannschaften().ForEach(mannschaft =>
                {
                    trHead.Cells.Add(createCell($"{mannschaft.Name}", "tablecell cellHead"));
                });
                presenterTable.Rows.Add(trHead);

                turnier.getMannschaften().ForEach(mannschaft =>
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    tr.Cells.Add(createCell($"{mannschaft.Name}", "tablecell cellHead"));
                    turnier.getMannschaften().ForEach(inlineMannschaft => {
                        tr.Cells.Add(createCell($"0:0 <br> debug:{mannschaft.Name}-{inlineMannschaft.Name}", "tablecell cellReadOnly"));
                    });
                    presenterTable.Rows.Add(tr);
                });

                HtmlTableRow trEmpty = new HtmlTableRow();
                trEmpty.Cells.Add(createCell($"", "tablecell"));
                presenterTable.Rows.Add(trEmpty);
            });
        }

        public string getOrCreateSession()
        {
            string session = "undefined";
            if (this.Session["User"] != null)
            {
                session = (string)this.Session["User"];
            }
            return session;
        }
        #endregion
    }
}