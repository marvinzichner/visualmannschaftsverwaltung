//Name          Marvin Zichner
//Datum         12.03.2020
//Datei         ApplicationController.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class ApplicationController
    {
        #region Eigenschaften
        private List<Tuple<string, string>> _userData;
        private List<Mannschaft> _mannschaften;
        #endregion

        #region Accessoren / Modifier
        public List<Tuple<string, string>> UserData { get => _userData; set => _userData = value; }
        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        #endregion

        #region Konstruktoren
        public ApplicationController()
        {
            this.UserData = new List<Tuple<string, string>>();
            this.Mannschaften = new List<Mannschaft>();
        }
        #endregion

        #region Worker
        public void buttonPersonTypeSelected(string param)
        {
            setTuple("typematcher", param);
        }

        public void setTuple(string key, string value)
        {
            this.UserData.Remove(
                this.UserData.Find(x => x.Item1 == key));
            this.UserData.Add(
                new Tuple<string, string>(key, value));
        }

        public void receiveContext(List<Mannschaft> mns)
        {
            this.Mannschaften = mns;
        }

        public string getFirstTupleMatch(string key)
        {
            string match = this.UserData.Find(x => x.Item1 == key).Item2;
            if (match == "") {
                match = "NoTupleMatchFound";
            }
            return match;
        }

        public void prepareTuple()
        {
            this.UserData.Add(
                new Tuple<string, string>("typematcher", "value"));
        }
        #endregion
    }
}
