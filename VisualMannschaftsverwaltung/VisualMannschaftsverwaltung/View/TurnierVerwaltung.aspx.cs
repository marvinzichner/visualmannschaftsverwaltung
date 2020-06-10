using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace VisualMannschaftsverwaltung.View
{
    public partial class TurnierVerwaltung : System.Web.UI.Page
    {
        #region Eigenschaften
        private ApplicationController applicationController;
        #endregion

        #region Accessoren / Modifier
        public ApplicationController ApplicationController { get => applicationController; set => applicationController = value; }
        #endregion

        #region Konstruktor
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            ApplicationController = Global.ApplicationController;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            loadTurniere();
        }

        private HtmlTableCell createCell(string text, string classes)
        {
            HtmlTableCell tc = new HtmlTableCell();
            tc.InnerHtml = text;
            tc.Attributes.Add("class", classes);

            return tc;
        }

        private void loadTurniere()
        {
            HtmlTableRow trHead = new HtmlTableRow();
            trHead.Cells.Add(createCell($"Globale Id", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Name", "tablecell cellHead"));
            trHead.Cells.Add(createCell($"Typ", "tablecell cellHead"));
            storedTurniere.Rows.Add(trHead);

            ApplicationController.getTurniere().ForEach(turnier => {
                HtmlTableRow tr = new HtmlTableRow();
                tr.Attributes.Add("class", "cellBodyHover");
                tr.Cells.Add(createCell($"{turnier.getId()}", "tablecell cellBody"));
                tr.Cells.Add(createCell($"{turnier.getName()}", "tablecell cellBody"));
                tr.Cells.Add(createCell($"{turnier.getType()}", "tablecell cellBody"));
                storedTurniere.Rows.Add(tr);

                int len = turnier.getMannschaften().Count;
                int count = 1;
                turnier.getMannschaften().ForEach(mannschaft =>
                {
                    string cellClass = "cellBodyHover cellEnd";
                    HtmlTableRow trEx = new HtmlTableRow();

                    if (len != count)
                        cellClass = "cellBodyHover";
                    trEx.Attributes.Add("class", cellClass);
                    trEx.Cells.Add(createCell($"", "tablecell cellReadOnly cellBody"));
                    trEx.Cells.Add(createCell($"&rarr; {mannschaft.Name}", "tablecell cellReadOnly cellBody"));
                    trEx.Cells.Add(createCell($"{mannschaft.SportArt}", "tablecell cellReadOnly cellBody"));
                    storedTurniere.Rows.Add(trEx);

                    count++;
                });
            });
        }

        #region Worker
        #endregion
    }
}