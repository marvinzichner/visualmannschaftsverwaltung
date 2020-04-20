//Name          Marvin Zichner
//Datum         12.02.2020
//Datei         Mannschaft.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class Mannschaft
    {
        #region Eigenschaften
        private List<Person> _personen;
        private string _name;
        private OrderBy _sortRule;
        private int _orderTimes;
        private SearchTerm _filterRule;
        private SportArt _sportArt;
        private bool _enableGrouping;
        public enum OrderBy
        {
            UNSORTED = 0,
            ERFOLG_ASC = 1,
            BIRTHDATE_ASC = 2,
            NAME_ASC = 3
        }
        public enum SearchTerm
        {
            ALL = 0,
            FUSSBALLSPIELER = 1,
            TENNISSPIELER = 2,
            HANDBALLSPIELER = 3
        }
        #endregion

        #region Accessoren / Modifier

        public string Name { get => _name; set => _name = value; }
        public List<Person> Personen { get => _personen; set => _personen = value; }
        public OrderBy SortRule { get => _sortRule; set => _sortRule = value; }
        public SearchTerm FilterRule { get => _filterRule; set => _filterRule = value; }
        public SportArt SportArt { get => _sportArt; set => _sportArt = value; }
        public int OrderTimes { get => _orderTimes; set => _orderTimes = value; }
        public bool EnableGrouping { get => _enableGrouping; set => _enableGrouping = value; }
        #endregion

        #region Konstruktoren
        public Mannschaft()
        {
            Name = "Musterverein";
            Personen = new List<Person>();
            SortRule = OrderBy.UNSORTED;
            FilterRule = SearchTerm.ALL;
            SportArt = SportArt.KEINE;
            OrderTimes = 0;
            EnableGrouping = false;
        }
        public Mannschaft(string name)
        {
            Name = name;
            Personen = new List<Person>();
            SortRule = OrderBy.UNSORTED;
            FilterRule = SearchTerm.ALL;
            SportArt = SportArt.KEINE;
            OrderTimes = 0;
            EnableGrouping = false;
        }

        public Mannschaft(string name, List<Person> personen) 
        {
            Name = name;
            Personen = personen;
            SortRule = OrderBy.UNSORTED;
            FilterRule = SearchTerm.ALL;
            SportArt = SportArt.KEINE;
            OrderTimes = 0;
            EnableGrouping = false;
        }

        public Mannschaft(Mannschaft c) 
        {
            Name = c.Name;
            Personen = c.Personen;
            SortRule = c.SortRule;
            FilterRule = c.FilterRule;
            SportArt = c.SportArt;
            OrderTimes = c.OrderTimes;
            EnableGrouping = c.EnableGrouping;
        }
        #endregion

        #region Worker
        public Mannschaft add(Person p)
        {
            this.Personen.Add(p);
            return this;
        }

        public Mannschaft rule(OrderBy ob)
        {
            SortRule = ob;
            return this;
        }
        public Mannschaft enableGroupSort()
        {
            OrderTimes = 2;
            EnableGrouping = true;
            return this;
        }


        public Mannschaft sportArt(SportArt sa)
        {
            SportArt = sa;
            return this;
        }

        public Mannschaft name(string s)
        {
            Name = s;
            return this;
        }

        public Mannschaft rule(SearchTerm st)
        {
            FilterRule = st;
            return this;
        }

        public Mannschaft flushPersonen()
        {
            this.Personen = new List<Person>();
            return this;
        }

        public Mannschaft addRange(List<Person> ps)
        {
            foreach(Person p in ps) { 
                this.Personen.Add(p);
            }
            return this;
        }

        public List<Person> applySearchPattern()
        {
            List<Person> persons = new List<Person>();
            Mannschaft mannschaft = new Mannschaft(this);

            foreach(Person p in mannschaft.Personen) {
                if (FilterRule == SearchTerm.ALL)
                {
                    persons.Add(p);
                }
                else if (FilterRule == SearchTerm.FUSSBALLSPIELER
                    && p.isFussballSpieler())
                {
                    persons.Add(p);
                }
                else if (FilterRule == SearchTerm.HANDBALLSPIELER
                    && p.isHandballSpieler())
                {
                    persons.Add(p);
                }
                else if (FilterRule == SearchTerm.TENNISSPIELER
                    && p.isTennisSpieler())
                {
                    persons.Add(p);
                }
            }

           
            for(int o = 1; o < OrderTimes; o++)
            { 
                for(int i = 0; i < persons.Count; i++)
                {
                    for (int j = 0; j < persons.Count; j++)
                    {
                        Person p1 = persons[i];
                        Person p2 = persons[j];

                        if(SortRule == OrderBy.ERFOLG_ASC && EnableGrouping && p1.getSportArtCode() < p2.getSportArtCode() ||
                            SortRule == OrderBy.ERFOLG_ASC && !EnableGrouping && p1.compareBySpielSiege(p2) < 0 ||
                            SortRule == OrderBy.NAME_ASC && p1.compareByName(p2) < 0 ||
                            SortRule == OrderBy.BIRTHDATE_ASC && p1.compareByBirthdate(p2) < 0) {
                            int idx1 = persons.IndexOf(p1);
                            int idx2 = persons.IndexOf(p2);
                            persons[idx1] = p2;
                            persons[idx2] = p1;
                        }
                    }
                }
            }

            if(EnableGrouping)
            {
                EnableGrouping = false;
            }

            mannschaft.Personen = persons;
            return mannschaft.Personen;
        }

        public void printRules()
        {
            Console.WriteLine("==>  [ OrderBy." + SortRule + " with SearchTerm." + FilterRule + " ]");
        }
        #endregion
    }
}