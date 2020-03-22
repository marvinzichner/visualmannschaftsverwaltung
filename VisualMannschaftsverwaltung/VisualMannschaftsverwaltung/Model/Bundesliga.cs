//Name          Marvin Zichner
//Datum         12.02.2020
//Datei         Bundesliga.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class Bundesliga
    {
        #region Eigenschaften
        private string _Title;
        private List<Verein> _Vereine;
        private int _LigaNummer;
        private List<Turnier> _Turnier;
        #endregion

        #region Accessoren / Modifier
        public string Title { get => _Title; set => _Title = value; }
        public List<Verein> Vereine { get => _Vereine; set => _Vereine = value; }
        public int LigaNummer { get => _LigaNummer; set => _LigaNummer = value; }
        public List<Turnier> Turnier { get => _Turnier; set => _Turnier = value; }
        #endregion

        #region Konstruktoren
        public Bundesliga()
        {
           
        }
        public Bundesliga(string name) 
        { 

        }

        public Bundesliga(Bundesliga c) 
        {
           
        }
        #endregion

        #region Worker
        #endregion
    }
}