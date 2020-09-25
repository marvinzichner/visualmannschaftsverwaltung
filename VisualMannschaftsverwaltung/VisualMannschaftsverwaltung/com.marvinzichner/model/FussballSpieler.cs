//Name          Marvin Zichner
//Datum         06.02.2020
//Datei         FussballSpieler.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class FussballSpieler : Spieler
    {
        #region Eigenschaften
        private bool _isLeftFeet;
        private int _SpielSiege;
        #endregion

        #region Accessoren / Modifier
        public bool IsLeftFeet { get => _isLeftFeet; set => _isLeftFeet = value; }
        public int SpielSiege { get => _SpielSiege; set => _SpielSiege = value; }
        #endregion

        #region Konstruktoren
        public FussballSpieler():base()
        {
            IsLeftFeet = true;
            SpielSiege = 0;
            GenericAttributes = 
                new List<string> { "IsLeftFeet", "SpielSiege" };
        }
        public FussballSpieler(string name, int number, bool isLeftFeet, SpielerRolle spielerRolle, int siege) 
            : base(name, number, SportArt.FUSSBALL, spielerRolle, siege)
        {
            IsLeftFeet = isLeftFeet;
            SpielSiege = siege;
            GenericAttributes =
                new List<string> { "IsLeftFeet", "SpielSiege" };
        }

        public FussballSpieler(FussballSpieler fs)
            :base(fs)
        {
            IsLeftFeet = fs.IsLeftFeet;
            SpielSiege = fs.SpielSiege;
            GenericAttributes = fs.GenericAttributes;
        }
        #endregion

        #region Worker
        public FussballSpieler spielerNummer(int i)
        {
            this.SpielerNummer = i;
            return this;
        }
        public FussballSpieler spielerRolle(SpielerRolle sr)
        {
            this.SpielerRolle = sr;
            return this;
        }
        public FussballSpieler isLeftFeet(bool b)
        {
            this.IsLeftFeet = b;
            return this;
        }
        public FussballSpieler spielSiege(int i)
        {
            this.SpielSiege = i;
            return this;
        }

        public override void sayHello()
        {
            base.sayHello();
            Console.WriteLine("    Ich habe auf dem Feld die Position " + this.SpielerRolle);
            Console.WriteLine("    Mein starker Fuss ist Links? " + this.IsLeftFeet);
        }

        public override int getSpielSiege()
        {
            return this.SpielSiege;
        }

        public override int compareBySpielSiege(Person p)
        {
            int compareResult = -1;

            if (p.isFussballSpieler() 
                && this.getSpielSiege() < p.getSpielSiege())
            {
                compareResult = -1;
            }
            else if (p.isFussballSpieler() 
                && this.getSpielSiege() == p.getSpielSiege())
            {
                compareResult = 0;
            }
            else if (p.isFussballSpieler() 
                && this.getSpielSiege() < p.getSpielSiege())
            {
                compareResult = 1;
            }

            return compareResult;
        }

        public override int compareByName(Person p)
        {
            int result = -2;

            string thisName = this.Name.Substring(0, 1);
            string otherName = p.Name.Substring(0, 1);

            if (Utils.getLetterCode(thisName) < Utils.getLetterCode(otherName))
            {
                result = -1;
            }
            else if (Utils.getLetterCode(thisName) == Utils.getLetterCode(otherName))
            {
                result = 0;
            }
            else if (Utils.getLetterCode(thisName) > Utils.getLetterCode(otherName))
            {
                result = 1;
            }

            return result;
        }

        public override string saySkills()
        {
            return $"Siege: {SpielSiege} | Geburtstag: {Birthdate}";
        }

        public override int compareByBirthdate(Person p)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("de-DE");
            DateTime thisDate = DateTime.Parse(this.Birthdate, cultureInfo);
            DateTime otherDate = DateTime.Parse(p.Birthdate, cultureInfo);
            return Utils.compareDates(thisDate, otherDate);
        }

        public override string getSpecifiedSqlStatement()
        {
            return $"insert into MVW_FUSSBALLSPIELER (PERSON_FK, GEWONNENE_SPIELE, LEFT_FOOT) " +
                    $"values (LAST_INSERT_ID(), {SpielSiege.ToString()}, {Utils.convertToBasic(IsLeftFeet)})";
        }

        public override string getSpecifiedUpdateSqlStatement(string id)
        {
            return $"update MVW_FUSSBALLSPIELER set " +
                    $"GEWONNENE_SPIELE={SpielSiege.ToString()}, LEFT_FOOT={Utils.convertToBasic(IsLeftFeet)} " +
                    $"where PERSON_FK = {id}";
        }

        public override Person buildFromKeyValueAttributeList(List<KeyValuePair<string, string>> attr)
        {
            this.sportArt(SportArt.FUSSBALL)
                .toFussballSpieler()
                .spielSiege(
                    Utils.convertToInt(attr.Find(x => x.Key == "SpielSiege").Value, 0, "SpielSiege"))
                .isLeftFeet(
                    Utils.convertToBool(attr.Find(x => x.Key == "IsLeftFeet").Value, false, "IsLeftFeet"));

            return this;
        }
        #endregion
    }
}
