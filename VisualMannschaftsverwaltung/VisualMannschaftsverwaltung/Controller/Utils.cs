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

        public static int compareDates(DateTime d1, DateTime d2)
        {
            int result = -1;

            if(d1 < d2)
            {
                result = -1;
            }
            else if (d1 == d2)
            {
                result = 0;
            }
            else if (d1 > d2)
            {
                result = 1;
            }

            return result;
        }

        public List<Person> flushPersonList()
        {
            this.Personen = new List<Person>();
            return this.Personen;
        }

        public static string basicClassName(object o)
        {
            return o.GetType().Name.ToString();
        }

        public static bool convertFromBasic(string s)
        {
            int basicInt = 0;

            try { 
                basicInt = Convert.ToInt32(s);
            } catch(Exception) { }

            if(basicInt == 0)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }

        public static string convertToBasic(bool b)
        {
            if (b)
            {
                return "1";
            }
            else
            {
                return "0";
            }     
        }

        public List<Person> addPerson(Person p)
        {
            this.Personen.Add(p);
            return this.Personen;
        }

        public static string simpleFileDate()
        {
            return $"{DateTime.Now.Year.ToString()}{DateTime.Now.Month.ToString()}{DateTime.Now.Day.ToString()}" +
                $"{DateTime.Now.Hour.ToString()}{DateTime.Now.Minute.ToString()}{DateTime.Now.Second.ToString()}";
        }

        static public bool convertToBool(object o, bool standard, string field)
        {
            bool b = standard;
            try
            {
                b = Convert.ToBoolean(o);
            }
            catch (Exception e)
            {
                throw new InputValidationException(
                    "Die Eingabe konnte nicht in einen boolischen Wert umgewandelt werden",
                    "PersonenVerwaltung",
                    field,
                    "'True' oder 'False'");
            }
            return b;
        }

        static public int convertToInt(object o, int standard, string field)
        {
            int i = standard;
            try
            {
                i = Convert.ToInt32(o);
            }
            catch (Exception e)
            {
                throw new InputValidationException(
                    "Die Eingabe konnte nicht in eine Zahl umgewandelt werden",
                    "PersonenVerwaltung",
                    field,
                    "Beliebige Anzahl von Ziffern im Raum 0-9");
            }
            return i;
        }
        #endregion
    }
}
