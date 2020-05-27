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
        public static List<Mannschaft> createApplicationContext()
        {
            List<Mannschaft> returnableList = new List<Mannschaft>();
            
            Mannschaft mannschaft = new Mannschaft();
            //mannschaft.Personen = createPersonData();
            returnableList.Add(
                mannschaft
                    .name("1. FC Köln")
                    .sportArt(SportArt.FUSSBALL));

            mannschaft = new Mannschaft();
            returnableList.Add(
                mannschaft
                    .name("TC Grün-Rot Kaiserslautern")
                    .sportArt(SportArt.TENNIS)
                    .flushPersonen());

            return returnableList;
        }

        public static void createDatabaseContext()
        {
            DataRepository repo = new DataRepository();
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

        public static void createFilesystemStructure()
        {
            Directory.CreateDirectory($"{System.AppDomain.CurrentDomain.BaseDirectory.ToString()}\\mv.data\\");
        }

        public static string getContextPath()
        {
            return $"{System.AppDomain.CurrentDomain.BaseDirectory.ToString()}\\mv.data\\";
        }

        public static List<Person> createPersonData()
        {
            List<Person> personen = new List<Person>();
            FussballSpieler Marvin = new FussballSpieler();
            HandballSpieler Henry = new HandballSpieler();
            TennisSpieler Lars = new TennisSpieler();
            Trainer John = new Trainer();
            Physiotherapeut Thomas = new Physiotherapeut();
            FussballSpieler Zili = new FussballSpieler();
            TennisSpieler Jeff = new TennisSpieler();

            Marvin
                .name("Marvin")
                .nachname("Zichner")
                .sportArt(SportArt.FUSSBALL)
                .birthdate("09.08.1999")
                .toFussballSpieler()
                .isLeftFeet(true)
                .spielerNummer(12)
                .spielSiege(66)
                .spielerRolle(SpielerRolle.STUERMER);

            Henry
                .name("Henry C.")
                .nachname("Johnson")
                .birthdate("16.01.2000")
                .sportArt(SportArt.HANDBALL)
                .toHandballSpieler()
                .isLeftHand(false)
                .spielerNummer(2)
                .spielSiege(6)
                .spielerRolle(SpielerRolle.KEINE);

            Lars
                .name("Lars T.-J.")
                .nachname("Smith")
                .birthdate("13.07.1971")
                .sportArt(SportArt.TENNIS)
                .toTennisSpieler()
                .isLeftHand(true)
                .spielerNummer(1)
                .spielSiege(11)
                .spielerRolle(SpielerRolle.KEINE);

            Jeff
                .name("Jeff")
                .nachname("Miller")
                .birthdate("12.06.1971")
                .sportArt(SportArt.TENNIS)
                .toTennisSpieler()
                .isLeftHand(true)
                .spielerNummer(2)
                .spielSiege(2)
                .spielerRolle(SpielerRolle.KEINE);

            John
                .name("John")
                .nachname("de Reginald")
                .birthdate("11.11.1981")
                .sportArt(SportArt.KEINE)
                .toTrainer()
                .hasLicense(true)
                .sportArt(SportArt.FUSSBALL);

            Zili
                .name("Carsten")
                .nachname("Stahl-Grubers")
                .birthdate("09.08.1999")
                .sportArt(SportArt.FUSSBALL)
                .toFussballSpieler()
                .isLeftFeet(true)
                .spielerNummer(12)
                .spielSiege(23)
                .spielerRolle(SpielerRolle.STUERMER);

            Thomas
                .name("Thomas")
                .nachname("Britt")
                .birthdate("23.12.2000")
                .sportArt(SportArt.KEINE)
                .toPhysiotherapeut()
                .hasLicense(true);

            personen.Add(Marvin);
            personen.Add(Henry);
            personen.Add(Lars);
            personen.Add(John);
            personen.Add(Zili);
            personen.Add(Thomas);
            personen.Add(Jeff);

            return personen;
        }
        #endregion
    }
}
