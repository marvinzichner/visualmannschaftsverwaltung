//Name          Marvin Zichner
//Datum         06.02.2020
//Datei         Spieler.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public abstract class Spieler : Person
    {
        #region Eigenschaften
        private int _spielerNummer;
        private SpielerRolle _spielerRolle;
        private int _SpielSiege;
        #endregion

        #region Accessoren / Modifier
        public int SpielerNummer { get => _spielerNummer; set => _spielerNummer = value; }
        public SpielerRolle SpielerRolle { get => _spielerRolle; set => _spielerRolle = value; }
        public int SpielSiege { get => _SpielSiege; set => _SpielSiege = value; }
        #endregion

        #region Konstruktoren
        public Spieler()
            :base()
        {
            SpielerNummer = -9999;
            SpielerRolle = SpielerRolle.KEINE;
            SpielSiege = 0;
        }

        public Spieler(string name, int number, SportArt sportArt, SpielerRolle spielerRolle, int spielSiege)
            :base(name)
        {
            SpielerNummer = number;
            SportArt = sportArt;
            SpielerRolle = spielerRolle;
            SpielSiege = spielSiege;
        }

        public Spieler(Spieler s)
            :base(s)
        {
            SpielerNummer = s.SpielerNummer;
            SportArt = s.SportArt;
            SpielerRolle = s.SpielerRolle;
            SpielSiege = s.SpielSiege;
        }
        #endregion

        #region Worker
        public Spieler spielerNummer(int i)
        {
            this.SpielerNummer = i;
            return this;
        }

        public Spieler spielerRolle(SpielerRolle sr)
        {
            this.SpielerRolle = sr;
            return this;
        }
        public SportArt getSportArt()
        {
            return this.SportArt;
        }

        public SpielerRolle getSpielerRolle()
        {
            return this.SpielerRolle;
        }

        public virtual void sayHello()
        {
            Console.WriteLine("  - " + this.Name + " (" + this.SpielerNummer + ")");
        }

        public override abstract int compareBySpielSiege(Person p);

        public override abstract int compareByName(Person p);
        #endregion
    }
}
