//Name          Marvin Zichner
//Datum         12.03.2020
//Datei         ApplicationController.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        private bool _editMode;
        private bool _DatabaseOk;
        #endregion

        #region Accessoren / Modifier
        public List<Tuple<string, string>> UserData { get => _userData; set => _userData = value; }
        public List<Mannschaft> Mannschaften { get => _mannschaften; set => _mannschaften = value; }
        public List<Person> Personen { get => _personen; set => _personen = value; }
        public List<KeyValuePair<string, Mannschaft.OrderBy>> StorageOrderBy { get => _storageOrderBy; set => _storageOrderBy = value; }
        public List<KeyValuePair<string, Mannschaft.SearchTerm>> StorageSearchTerm { get => _storageSearchTerm; set => _storageSearchTerm = value; }
        public Mannschaft TempMannschaft { get => _tempMannschaft; set => _tempMannschaft = value; }
        public bool EditMode { get => _editMode; set => _editMode = value; }
        public bool DatabaseOk { get => _DatabaseOk; set => _DatabaseOk = value; }
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
            this.EditMode = false;
            this.DatabaseOk = false;
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

        public List<Mannschaft> getMannschaften(string session)
        {
            DataRepository repo = new DataRepository();
            repo.enableSessionbasedQueries()
                .setSession(session);
            this.Mannschaften = repo.getMannschaften();
            return this.Mannschaften;
        }

        public List<Person> getAvailablePersonen()
        {
            DataRepository repo = new DataRepository();

            List<Person> list = new List<Person>();
            SportArt matchSportArt = TempMannschaft.SportArt;
            List<Person> matchMembers = TempMannschaft.Personen;

            Personen.ForEach(p => { 
                if(p.SportArt == matchSportArt && matchMembers.Find(x => x.ID == p.ID) == null || 
                    p.isPhysiotherapeut() && matchMembers.Find(x => x.ID == p.ID) == null ||
                    p.isTrainer() && matchMembers.Find(x => x.ID == p.ID) == null)
                {
                    list.Add(p);
                } 
            });

            return list;
        }

        public void addMannschaftIfNotExists(Mannschaft newMannschaft, string session)
        {
            DataRepository repo = new DataRepository();
            repo.enableSessionbasedQueries()
                .setSession(session);
            bool existing = false;
            Mannschaften.ForEach(m =>
            {
                if(newMannschaft.Name == m.Name
                    && newMannschaft.SportArt == m.SportArt)
                {
                    existing = true;
                }
            });

            if (!existing && newMannschaft.Name != "")
            {
                repo.createMannschaft(newMannschaft);
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

        public List<Person> getPersonen(Mannschaft.OrderBy ob, Mannschaft.SearchTerm st, string session = "ALL")
        {
            loadPersonenFromRepository(session);

            Mannschaft mannschaft = new Mannschaft("TRANSFERRING_OBJECT");
            mannschaft.Personen = this.Personen;

            if(ob == Mannschaft.OrderBy.NAME_ASC) {
                return mannschaft
                   .rule(ob)
                   .rule(st)
                   .disableGroupSort()
                   .applySearchPattern();
            }
            else
            {
                return mannschaft
                   .rule(ob)
                   .rule(st)
                   .enableGroupSort()
                   .applySearchPattern();
            }
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

        public void addPerson(Person person, string session = "ALL")
        {
            DataRepository repo = new DataRepository();
            repo.addPerson(person, session);
            loadPersonenFromRepository(session);
        }

        public void updatePerson(Person person, string session = "ALL")
        {
            DataRepository repo = new DataRepository();
            repo.updatePerson(person, session);
            loadPersonenFromRepository(session);
        }

        public void loadPersonenFromRepository(string session = "ALL")
        {
            DataRepository repo = new DataRepository();
            repo.setSession(session)
                .enableSessionbasedQueries();
         
            this.DatabaseOk = repo.checkConnection();
            this.Personen = repo.loadPersonen();
        }

        public void loadMannschaftenFromRepository(string session)
        {
            DataRepository repo = new DataRepository();
            repo.enableSessionbasedQueries()
                .setSession(session);
            this.DatabaseOk = repo.checkConnection();
            this.Mannschaften = repo.getMannschaften();
        }

        public void removePerson(Person p)
        {
            DataRepository repo = new DataRepository();
            repo.removePerson(p);
            loadPersonenFromRepository();
        }

        public void addPersonToMannschaft(Person p, Mannschaft m, string s)
        {
            DataRepository repo = new DataRepository();
            repo.addPersonToMannschaft(p, m);
            loadMannschaftenFromRepository(s);
        }
        public void removeMannschaft(Mannschaft m, string session)
        {
            DataRepository repo = new DataRepository();
            repo.setSession(session)
                .enableSessionbasedQueries()
                .removeMannschaft(m);
            Mannschaften.Remove(m);
        }

        public List<Turnier> getTurniere(string session = "ALL")
        {
            List<Turnier> Turniere = new List<Turnier>();
            DataRepository repo = new DataRepository();

            repo.enableSessionbasedQueries()
                .setSession(session);
            Turniere = repo.getTurniere();

            return Turniere;
        }

        public void addMappingOfTurnierAndMannschaft(string mannschaft, string turnier)
        {
            DataRepository repo = new DataRepository();
            repo.addMappingOfTurnierAndMannschaft(mannschaft, turnier);
        }

        public void deleteMappingOfTurnierAndMannschaft(string mannschaft, string turnier)
        {
            DataRepository repo = new DataRepository();
            repo.deleteMappingOfTurnierAndMannschaft(mannschaft, turnier);
        }

        public void deleteTurnierAndAllDependentEntities(string turnier)
        {
            DataRepository repo = new DataRepository();
            repo.deleteTurnierAndAllDependentEntities(turnier);
        }

        public void generatePersonenXML()
        {
            Export export = new Export();
            string filename = $"{Utils.simpleFileDate()}.pse.xml";
            string path = $"{ApplicationContext.getContextPath()}{filename}";

            export.configure(path, filename);
            export.doXmlExport(Personen, Person.getTypes());
            export.doDownload();
        }

        public void generateMannschaftenXML()
        {
            DataRepository repo = new DataRepository();
            Export export = new Export();
            string filename = $"{Utils.simpleFileDate()}.mne.xml";
            string path = $"{ApplicationContext.getContextPath()}{filename}";

            repo.disableSessionbasedQueries();
            export.configure(path, filename);
            export.doXmlExport(repo.getMannschaften(), Mannschaft.getTypes());
            export.doDownload();
        }

        public void createNewTurnier(string name, SportArt sportArt, string session)
        {
            DataRepository repo = new DataRepository();
            repo
                .setSession(session)
                .addTurnier(name, sportArt);
        }
        #endregion
    }
}
