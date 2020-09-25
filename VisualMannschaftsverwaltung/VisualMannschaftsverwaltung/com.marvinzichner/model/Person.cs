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
        private string _nachname;
        private string _bithdate;
        private SportArt _sportArt;
        private int _ID;
        private List<string> _genericAttributes;
        #endregion

        #region Accessoren / Modifier
        public string Name { get => _name; set => _name = value; }
        public SportArt SportArt { get => _sportArt; set => _sportArt = value; }
        public List<string> GenericAttributes { get => _genericAttributes; set => _genericAttributes = value; }
        public string Nachname { get => _nachname; set => _nachname = value; }
        public string Birthdate { get => _bithdate; set => _bithdate = value; }
        public int ID { get => _ID; set => _ID = value; }
        #endregion

        #region Konstruktoren
        public Person()
        {
            Name = "Max";
            Nachname = "Mustermann";
            Birthdate = "12.34.5678";
            SportArt = SportArt.KEINE;
            GenericAttributes = new List<string>();
        }

        public Person(string name)
        {
            Name = name;
            Nachname = "Mustermann";
            Birthdate = "12.34.5678";
            SportArt = SportArt.KEINE;
            GenericAttributes = new List<string>();
        }

        public Person(Person p)
        {
            Name = p.Name;
            Nachname = p.Nachname;
            Birthdate = p.Birthdate;
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
        public Person toDefinedPlayerBy(Type t)
        {
            if(t == typeof(FussballSpieler))
            {
                return (FussballSpieler)this;
            }
            if (t == typeof(HandballSpieler))
            {
                return (HandballSpieler)this;
            }
            if (t == typeof(TennisSpieler))
            {
                return (TennisSpieler)this;
            }
            if (t == typeof(Trainer))
            {
                return (Trainer)this;
            }
            if (t == typeof(Physiotherapeut))
            {
                return (Physiotherapeut)this;
            }
            else
            {
                return (Trainer)this;
            }
        }

        public abstract int getSpielSiege();

        public abstract int compareBySpielSiege(Person p);

        public abstract int compareByName(Person p);

        public abstract int compareByBirthdate(Person p);

        public abstract string getSpecifiedSqlStatement();

        public abstract string getSpecifiedUpdateSqlStatement(string id);

        public abstract string saySkills();

        public abstract Person buildFromKeyValueAttributeList(List<KeyValuePair<string, string>> attr);

        public virtual Person name(string s)
        {
            this.Name = s;
            return this;
        }

        public virtual Person nachname(string s)
        {
            s = ApplicationContext.disarmHijacking(s);
            this.Nachname = s;
            return this;
        }

        public virtual Person id(int id)
        {
            this.ID = id;
            return this;
        }

        public virtual Person birthdate(string s)
        {
            s = ApplicationContext.disarmHijacking(s);
            s = s.Substring(0, 10);
            this.Birthdate = s;
            return this;
        }
        public virtual Person sportArt(SportArt s)
        {
            this.SportArt = s;
            return this;
        }

        public static Type[] getTypes()
        {
            Type[] personTypes = {
                typeof(Person),
                typeof(Spieler),
                typeof(FussballSpieler),
                typeof(HandballSpieler),
                typeof(TennisSpieler),
                typeof(Trainer),
                typeof(Physiotherapeut)
            };

            return personTypes;
        }
        #endregion
    }
}
