using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class ResultScoreAnalyzer
    {
        #region Eigenschaften
        private KeyValueList kv;
        private List<Spiel> spiele;
        private Dictionary<int, int> stored;
        #endregion

        #region Accessoren / Modifier
        #endregion

        #region Konstruktoren
        public ResultScoreAnalyzer() {
            this.kv = new KeyValueList();
            this.spiele = new List<Spiel>();
            this.stored = new Dictionary<int, int>();
        }
        public ResultScoreAnalyzer(KeyValueList kvList)
        {
            this.kv = kvList;
        }
        #endregion

        #region Worker
        public void append(Spiel spiel)
        {
            this.spiele.Add(spiel);
        }

        public void analyze()
        {
            // check if mannschaften exist in dictionary
            spiele.ForEach(spiel =>
            {
                if (!stored.ContainsKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_A)))
                    stored.Add(spiel.getMannschaft(Spiel.TeamUnit.TEAM_A), 0);
                if (!stored.ContainsKey(spiel.getMannschaft(Spiel.TeamUnit.TEAM_B)))
                    stored.Add(spiel.getMannschaft(Spiel.TeamUnit.TEAM_B), 0);
            });

            // logic 
            spiele.ForEach(spiel =>
            {
                int A = spiel.getResult(Spiel.TeamUnit.TEAM_A);
                int B = spiel.getResult(Spiel.TeamUnit.TEAM_B);
                int TEAM_A = spiel.getMannschaft(Spiel.TeamUnit.TEAM_A);
                int TEAM_B = spiel.getMannschaft(Spiel.TeamUnit.TEAM_B);

                if (A == B)
                {
                    stored[TEAM_A] = stored[TEAM_A] + 1;
                    stored[TEAM_B] = stored[TEAM_B] + 1;
                }
                else if (A > B)
                {
                    stored[TEAM_A] = stored[TEAM_A] + 3;
                }
                else if (A < B)
                {
                    stored[TEAM_B] = stored[TEAM_B] + 3;
                }
            });
        }

        public List<KeyValuePair<int, int>> getSpiele()
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();

            foreach (var team in this.stored)
            {
                list.Add(new KeyValuePair<int, int>(team.Key, team.Value));
            }

            list.Sort(
                (x, y) => Convert.ToInt32(x.Value).CompareTo(Convert.ToInt32(y.Value)));
            list.Reverse();

            return list;
        }
        #endregion
    }
}