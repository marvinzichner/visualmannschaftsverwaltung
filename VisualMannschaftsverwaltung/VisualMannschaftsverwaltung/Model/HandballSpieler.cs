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
    public class HandballSpieler : Spieler
    {
        #region Eigenschaften
        bool _isLeftHand;
        int _SpielSiege;
        #endregion

        #region Accessoren / Modifier
        public bool IsLeftHand { get => _isLeftHand; set => _isLeftHand = value; }
        public int SpielSiege { get => _SpielSiege; set => _SpielSiege = value; }
        #endregion

        #region Konstruktoren
        public HandballSpieler():base()
        {
            IsLeftHand = true;
            SpielSiege = 0;
            GenericAttributes =
                new List<string> { "IsLeftHand", "SpielSiege" };
        }
        public HandballSpieler(string name, int number, bool isLeftHand) 
            : base(name, number, SportArt.HANDBALL, SpielerRolle.KEINE, 0)
        {
            IsLeftHand = isLeftHand;
            SpielSiege = 0;
            GenericAttributes =
                new List<string> { "IsLeftHand", "SpielSiege" };
        }

        public HandballSpieler(HandballSpieler hs) 
            : base(hs)
        {
            IsLeftHand = hs.IsLeftHand;
            SpielSiege = hs.SpielSiege;
            GenericAttributes = hs.GenericAttributes;
        }
        #endregion

        #region Worker
        public HandballSpieler spielerNummer(int i)
        {
            this.SpielerNummer = i;
            return this;
        }
        public HandballSpieler spielSiege(int i)
        {
            this.SpielSiege = i;
            return this;
        }
        public HandballSpieler spielerRolle(SpielerRolle sr)
        {
            this.SpielerRolle = sr;
            return this;
        }
        public HandballSpieler isLeftHand(bool b)
        {
            this.IsLeftHand = b;
            return this;
        }

        public override void sayHello()
        {
            base.sayHello();
            Console.WriteLine("    Ich habe auf dem Feld die Position " + this.SpielerRolle);
            Console.WriteLine("    Verwende ich beim Werfen die linke Hand? " + this.IsLeftHand);
        }

        public override int getSpielSiege()
        {
            return this.SpielSiege;
        }

        public override int compareByBirthdate(Person p)
        {
            DateTime thisDate = DateTime.Parse(this.Birthdate);
            DateTime otherDate = DateTime.Parse(p.Birthdate);
            return Utils.compareDates(thisDate, otherDate);
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

        public override string getSpecifiedSqlStatement()
        {
            return $"insert into MVW_HANDBALLSPIELER (PERSON_FK, GEWONNENE_SPIELE, LEFT_HAND) " +
                    $"values (LAST_INSERT_ID(), {SpielSiege.ToString()}, {Utils.convertToBasic(IsLeftHand)})";
        }
        #endregion
    }
}
