//Name          Marvin Zichner
//Datum         12.02.2020
//Datei         Turnier.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class Turnier
    {
        #region Eigenschaften
        private int id;
        private string name;
        private SportArt type;
        private string turnierArt;
        private List<Mannschaft> mannschaften;
        #endregion

        #region Accessoren / Modifier
        public Turnier setId(string idString) { id = Convert.ToInt32(idString); return this; }
        public int getId(){ return id; }

        public Turnier setType(SportArt s) { type = s; return this; }
        public SportArt getType() { return type; }

        public Turnier setTurnierArt(string s) { turnierArt = s; return this; }
        public string getTurnierArt() { return turnierArt; }

        public Turnier setName(string s) { name = s; return this; }
        public string getName() { return name; }

        public Turnier setMannschaft(List<Mannschaft> list) { mannschaften = list; return this; }
        public List<Mannschaft> getMannschaften() { return mannschaften; }
        #endregion

        #region Konstruktoren
        public Turnier()
        {
            id = 9999;
            type = SportArt.KEINE;
            turnierArt = "undefined";
            mannschaften = new List<Mannschaft>();
        }
        public Turnier(string name) 
        { 

        }

        public Turnier(Turnier c) 
        {
           
        }
        #endregion

        #region Worker
        public Turnier setType(string s)
        {
            s = ApplicationContext.disarmHijacking(s);
            SportArt sa = SportArt.KEINE;
            sa = (SportArt)Enum.Parse(typeof(SportArt), s);

            type = sa;
            return this;
        }

        public Turnier addMannschaft(Mannschaft m)
        {
            mannschaften.Add(m);
            return this;
        }
        #endregion
    }
}