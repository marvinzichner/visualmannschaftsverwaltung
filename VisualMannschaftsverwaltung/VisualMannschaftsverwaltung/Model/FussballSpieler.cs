//Name          Marvin Zichner
//Datum         06.02.2020
//Datei         FussballSpieler.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class FussballSpieler : Spieler
    {
        #region Eigenschaften
        bool _isLeftFeet;
        int _SpielSiege;
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
        #endregion
    }
}
