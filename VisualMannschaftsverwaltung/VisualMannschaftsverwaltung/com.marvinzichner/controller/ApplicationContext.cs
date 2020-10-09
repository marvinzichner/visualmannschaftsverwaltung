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
        public static bool databaseIsReady()
        {
            DataRepository repo = new DataRepository();
            return repo.databaseIsConnectedAndReady();
        }

        public static void doMigration(bool isReady)
        {
            int DB_VERSION, DB_VERSION_LAUNCH, currentFile = 0;
            DataRepository repo = new DataRepository();
            repo.tryBasicConnectionIfErrorAppears();

            if (isReady)
            {
                DB_VERSION = repo.getLatestVersion();
                DB_VERSION_LAUNCH = repo.getLatestVersion();
            }
            else
            {
                DB_VERSION = -1;
                DB_VERSION_LAUNCH = -1;
            }

            string currentPath = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
            string migrationPath = $"{currentPath}\\com.marvinzichner\\controller\\repository\\migration";
            string[] scripts = Directory.GetFiles(migrationPath);
            foreach (string script in scripts)
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

            if (DB_VERSION_LAUNCH == -1)
            {
                GeneratorUtil generatorUtil = new GeneratorUtil();
                generatorUtil.generate();
            }
        }
        public static void createDatabaseContext()
        {
            DataRepository repo = new DataRepository();
            if (repo.databaseIsConnectedAndReady())
            {
                // repo.FORCE_DELETE_DATABASE();
                doMigration(true);
            }

            if (repo.databaseExists() == false)
            {
                doMigration(false);
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

        public static string disarmHijacking(string s)
        {
            s = s.Replace('"', ' ')
                .Replace('\'', ' ')
                .Replace('`', ' ')
                .Replace(';', ' ')
                .Replace(',', ' ')
                .Replace('%', ' ')
                .Replace('$', ' ');

            return s;
        }
        #endregion
    }
}
