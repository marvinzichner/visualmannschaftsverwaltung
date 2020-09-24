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
        #endregion

        #region Accessoren / Modifier
        #endregion

        #region Konstruktoren
        public ResultScoreAnalyzer() {
            this.kv = new KeyValueList();
        }
        public ResultScoreAnalyzer(KeyValueList kvList)
        {
            this.kv = kvList;
        }
        #endregion

        #region Worker
        public Dictionary<string, string> getGoalsByTeamId()
        {
            


            return new Dictionary<string, string>();
        }
        #endregion
    }
}