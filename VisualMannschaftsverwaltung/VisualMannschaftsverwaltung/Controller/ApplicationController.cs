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
        private List<KeyValuePair<string, Mannschaft.OrderBy>> _storageOrderBy;
        private List<KeyValuePair<string, Mannschaft.SearchTerm>> _storageSearchTerm;
        private List<Person> _personen;
        private Mannschaft _tempMannschaft;
        #endregion

        #region Accessoren / Modifier
        public List<Tuple<string, string>> UserData { get => _userData; set => _userData = value; }
        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        public List<Person> Personen { get => _personen; set => _personen = value; }
        public List<KeyValuePair<string, Mannschaft.OrderBy>> StorageOrderBy { get => _storageOrderBy; set => _storageOrderBy = value; }
        public List<KeyValuePair<string, Mannschaft.SearchTerm>> StorageSearchTerm { get => _storageSearchTerm; set => _storageSearchTerm = value; }
        public Mannschaft TempMannschaft { get => _tempMannschaft; set => _tempMannschaft = value; }
        #endregion

        #region Konstruktoren
        public ApplicationController()
        {
            this.UserData = new List<Tuple<string, string>>();
            this.Mannschaften = new List<Mannschaft>();
            this.Personen = new List<Person>();
            this.StorageOrderBy = new List<KeyValuePair<string, Mannschaft.OrderBy>>();
            this.StorageSearchTerm = new List<KeyValuePair<string, Mannschaft.SearchTerm>>();
            this.TempMannschaft = new Mannschaft();
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

        public List<Mannschaft> getMannschaften()
        {
            return this.Mannschaften;
        }

        public List<Person> getAvailablePersonen()
        {
            List<Person> list = new List<Person>();
            SportArt matchSportArt = TempMannschaft.SportArt;
            List<Person> matchMembers = TempMannschaft.Personen;

            Personen.ForEach(p => { 
                if(p.SportArt == matchSportArt && !matchMembers.Contains(p) || 
                    p.isPhysiotherapeut() && !matchMembers.Contains(p) ||
                    p.isTrainer() && !matchMembers.Contains(p))
                {
                    list.Add(p);
                }
            });

            return list;
        }

        public void addMannschaftIfNotExists(Mannschaft newMannschaft)
        {
            bool existing = false;
            Mannschaften.ForEach(m =>
            {
                if(newMannschaft.Name == m.Name
                    && newMannschaft.SportArt == m.SportArt)
                {
                    existing = true;
                }
            });

            if (!existing)
            {
                Mannschaften.Add(newMannschaft);
            }
        }

        public void receiveContext(List<Mannschaft> mns)
        {
            this.Mannschaften = mns;
        }

        public void receivePersonen(List<Person> mns)
        {
            this.Personen = mns;
        }

        public List<Person> getPersonen(Mannschaft.OrderBy ob, Mannschaft.SearchTerm st)
        {
            Mannschaft mannschaft = new Mannschaft("TRANSFERRING_OBJECT");
            mannschaft.Personen = this.Personen;

            return mannschaft
                    .rule(ob)
                    .rule(st)
                    .enableGroupSort()
                    .applySearchPattern();
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

        public void addPerson(Person person)
        {
            this.Personen.Add(person);
        }
        #endregion
    }
}
