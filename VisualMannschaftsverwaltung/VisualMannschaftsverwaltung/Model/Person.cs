//Name          Marvin Zichner
//Datum         06.02.2020
//Datei         Person.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public abstract class Person
    {
        #region Eigenschaften
        private string _name;
        private SportArt _sportArt;
        private List<string> _genericAttributes;
        #endregion

        #region Accessoren / Modifier
        public string Name { get => _name; set => _name = value; }
        public SportArt SportArt { get => _sportArt; set => _sportArt = value; }
        public List<string> GenericAttributes { get => _genericAttributes; set => _genericAttributes = value; }
        #endregion

        #region Konstruktoren
        public Person()
        {
            Name = "Max Mustermann";
            SportArt = SportArt.KEINE;
            GenericAttributes = new List<string>();
        }

        public Person(string name)
        {
            Name = name;
            SportArt = SportArt.KEINE;
            GenericAttributes = new List<string>();
        }

        public Person(Person p)
        {
            Name = p.Name;
            SportArt = p.SportArt;
            GenericAttributes = p.GenericAttributes;
        }

        #endregion

        #region Worker
        
        public bool isFussballSpieler()
        {
            return ReferenceEquals(typeof(FussballSpieler), this.GetType());
        }
        public FussballSpieler toFussballSpieler()
        {
            return (FussballSpieler) this;
        }

        public SportArt getSportArtCode()
        {
            return this.SportArt;
        }

        public List<string> getGenericAttribues()
        {
            return this.GenericAttributes;
        }
        public bool isTennisSpieler()
        {
            return ReferenceEquals(typeof(TennisSpieler), this.GetType());
        }
        public TennisSpieler toTennisSpieler()
        {
            return (TennisSpieler)this;
        }
        public bool isHandballSpieler()
        {
            return ReferenceEquals(typeof(HandballSpieler), this.GetType());
        }
        public HandballSpieler toHandballSpieler()
        {
            return (HandballSpieler)this;
        }
        public bool isTrainer()
        {
            return ReferenceEquals(typeof(Trainer), this.GetType());
        }
        public Trainer toTrainer()
        {
            return (Trainer)this;
        }
        public bool isPhysiotherapeut()
        {
            return ReferenceEquals(typeof(Physiotherapeut), this.GetType());
        }
        public Physiotherapeut toPhysiotherapeut()
        {
            return (Physiotherapeut)this;
        }
        public Spieler toGenericSpieler()
        {
            return (Spieler)this;
        }

        public abstract int getSpielSiege();

        public abstract int compareBySpielSiege(Person p);

        public abstract int compareByName(Person p);

        public virtual Person name(string s)
        {
            this.Name = s;
            return this;
        }
        public virtual Person sportArt(SportArt s)
        {
            this.SportArt = s;
            return this;
        }

        #endregion
    }
}
