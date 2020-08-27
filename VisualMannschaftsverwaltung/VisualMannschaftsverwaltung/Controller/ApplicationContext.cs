//Name          Marvin Zichner
//Datum         12.03.2020
//Datei         ApplicationContext.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VisualMannschaftsverwaltung
{
    public class ApplicationContext
    {
        #region Eigenschaften
        private static string _basePath;
        #endregion

        #region Accessoren / Modifier
        public static string BasePath { get => _basePath; set => _basePath = value; }
        #endregion

        #region Konstruktoren
        public ApplicationContext()
        {
            BasePath = "\\Mac\\Home\\Documents\\dev\\repo\\visualmannschaftsverwaltung\\VisualMannschaftsverwaltung\\VisualMannschaftsverwaltung";
        }
        #endregion

        #region Worker
        public static void createDatabaseContext()
        {
            DataRepository repo = new DataRepository();
            if(repo.databaseIsConnectedAndReady()) { 
                int DB_VERSION = repo.getLatestVersion();
                int currentFile = 0;

                string currentPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                string migrationPath = $"{currentPath}\\Controller\\Repository\\Migration";
                string[] scripts = Directory.GetFiles(migrationPath);
                foreach(string script in scripts)
                {
                    if (currentFile > DB_VERSION)
                    {
                        string sql = File.ReadAllText(script, Encoding.UTF8);
                        repo.executeSql(sql);

                        string versionInserter = $"insert into MVW_MIGRATION (VERSION, NAME, CREATED) values ({currentFile}, '{script}', NOW())";
                        repo.executeSql(versionInserter);
                    }

                    currentFile++;
                }
            }
        }

        public static void createFilesystemStructure()
        {
            Directory.CreateDirectory($"{System.AppDomain.CurrentDomain.BaseDirectory.ToString()}\\mv.data\\");
        }

        public static string getContextPath()
        {
            return $"{System.AppDomain.CurrentDomain.BaseDirectory.ToString()}\\mv.data\\";
        }

        public static string getBasePath()
        {
            return $"{System.AppDomain.CurrentDomain.BaseDirectory.ToString()}\\";
        }
        #endregion
    }
}
