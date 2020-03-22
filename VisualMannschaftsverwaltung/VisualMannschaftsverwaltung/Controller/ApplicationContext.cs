//Name          Marvin Zichner
//Datum         12.03.2020
//Datei         ApplicationContext.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class ApplicationContext
    {
        #region Eigenschaften
        #endregion

        #region Accessoren / Modifier
        #endregion

        #region Konstruktoren
        public ApplicationContext()
        {
           
        }
        #endregion

        #region Worker
        public static List<Mannschaft> createApplicationContext()
        {
            List<Mannschaft> returnableList = new List<Mannschaft>();
            Mannschaft mannschaft = new Mannschaft("Köln");

            mannschaft
                .sportArt(SportArt.FUSSBALL)
                .flushPersonen();

            returnableList.Add(mannschaft);

            return returnableList;
        }
        #endregion
    }
}
