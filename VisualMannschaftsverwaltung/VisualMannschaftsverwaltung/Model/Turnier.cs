//Name          Marvin Zichner
//Datum         12.02.2020
//Datei         Turnier.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

namespace VisualMannschaftsverwaltung
{
    public class Turnier
    {
        #region Eigenschaften
        private string _Typ;
        private string _TurnierArt;
        #endregion

        #region Accessoren / Modifier
        public string Typ { get => _Typ; set => _Typ = value; }
        public string TurnierArt { get => _TurnierArt; set => _TurnierArt = value; }
        #endregion

        #region Konstruktoren
        public Turnier()
        {
           
        }
        public Turnier(string name) 
        { 

        }

        public Turnier(Turnier c) 
        {
           
        }
        #endregion

        #region Worker
        #endregion
    }
}