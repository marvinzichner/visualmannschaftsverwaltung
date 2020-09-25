//Name          Marvin Zichner
//Datei         DataRepository.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class RepositorySettings
    {
        #region Eigenschaften
        private string _server;
        private string _database;
        private string _uid;
        private string _password;
        #endregion

        #region Accessoren / Modifier
        public string Server { get => _server; set => _server = value; }
        public string Database { get => _database; set => _database = value; }
        public string Uid { get => _uid; set => _uid = value; }
        public string Password { get => _password; set => _password = value; }
        #endregion

        #region Konstruktoren
        public RepositorySettings()
        {
            Server = Global.PropertyManager.getProperty("DB_SERVER");
            Database = Global.PropertyManager.getProperty("DB_DATABASE");
            Uid = Global.PropertyManager.getProperty("DB_UID");
            Password = Global.PropertyManager.getProperty("DB_PASSWORD");
        }
        #endregion

        #region Worker
        public string getConnectionString()
        {
            return $"server={Server};database={Database};uid={Uid};password={Password};";
        }
        #endregion
    }
}
