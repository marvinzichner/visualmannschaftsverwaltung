//Name          Marvin Zichner
//Datum         12.02.2020
//Datei         Verein.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class Verein
    {
        #region Eigenschaften
        private List<Mannschaft> _Mannschaften;
        #endregion

        #region Accessoren / Modifier
        public List<Mannschaft> Mannschaften {get => _Mannschaften; set => _Mannschaften = value; }
        #endregion

        #region Konstruktoren
        public Verein()
        {
           
        }
        public Verein(string name) 
        { 

        }

        public Verein(Verein c) 
        {
           
        }
        #endregion

        #region Worker
        #endregion
    }
}