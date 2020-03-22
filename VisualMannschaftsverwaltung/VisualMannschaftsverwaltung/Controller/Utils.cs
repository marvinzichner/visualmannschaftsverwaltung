//Name          Marvin Zichner
//Datum         04.03.2020
//Datei         Utils.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    class Utils
    {
        #region Eigenschaften
        private List<Person> _personen;
        #endregion

        #region Accessoren / Modifier
        public List<Person> Personen { get => _personen; set => _personen = value; }
        #endregion

        #region Konstruktoren
        public Utils()
        {
            Personen = new List<Person>();
        }

        public Utils(Utils u)
        {
            Personen = u.Personen;
        }
        #endregion

        #region Worker
        public static int getLetterCode(string s)
        {
            char[] c = s.ToCharArray();
            return char.ToUpper(c[0]) - 64;
        }

        public static void generatePersonTable(List<Person> persons)
        {
            string PATTERN = "|{0,22}|{1,6}|{2,10}|{3,10}|{4,6}|{5,6}";
            Console.WriteLine(String.Format(PATTERN, "Name", "Nr", "Art", "Rolle", "XP", "Lic."));
            Console.WriteLine("-------------------------------------------------------------------");

            foreach (Person p in persons)
            {
                if (p.isFussballSpieler())
                {
                    FussballSpieler fs = p.toFussballSpieler();
                    Console.WriteLine(
                        String.Format(
                            PATTERN,
                            fs.Name,
                            fs.SpielerNummer,
                            fs.getSportArt(),
                            fs.getSpielerRolle(),
                            fs.SpielSiege,
                            ""
                        )
                    );
                }
                else if (p.isHandballSpieler())
                {
                    HandballSpieler fs = p.toHandballSpieler();
                    Console.WriteLine(
                        String.Format(
                            PATTERN,
                            fs.Name,
                            fs.SpielerNummer,
                            fs.getSportArt(),
                            fs.getSpielerRolle(),
                            fs.SpielSiege,
                            ""
                        )
                    );
                }
                else if (p.isTennisSpieler())
                {
                    TennisSpieler fs = p.toTennisSpieler();
                    Console.WriteLine(
                        String.Format(
                            PATTERN,
                            fs.Name,
                            fs.SpielerNummer,
                            fs.getSportArt(),
                            fs.getSpielerRolle(),
                            fs.SpielSiege,
                            ""
                        )
                    );
                }
                else if (p.isTrainer())
                {
                    Trainer fs = p.toTrainer();
                    Console.WriteLine(
                        String.Format(
                            PATTERN,
                            fs.Name,
                            "",
                            fs.getSportArt(),
                            "TRAINER",
                            "",
                            fs.HasLicense
                        )
                    );
                }
            }
        }

        public List<Person> flushPersonList()
        {
            this.Personen = new List<Person>();
            return this.Personen;
        }

        public List<Person> addPerson(Person p)
        {
            this.Personen.Add(p);
            return this.Personen;
        }
        #endregion
    }
}
