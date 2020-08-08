using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class KeyValueList
    {
        #region Eigenschaften
        private List<KeyValuePair<string, string>> kvList;
        #endregion

        #region Accessoren / Modifier
        public List<KeyValuePair<string, string>> KvList { get => kvList; set => kvList = value; }
        #endregion

        #region Konstruktor
        public KeyValueList()
        {
            kvList = new List<KeyValuePair<string, string>>();
        }

        public KeyValueList(List<KeyValuePair<string, string>> kvList)
        {
            this.kvList = kvList;
        }
        #endregion

        #region Worker
        public void extractDataFromCombinedString(string s)
        {
            List<KeyValuePair<string, string>> extractedData =
                new List<KeyValuePair<string, string>>();

            if (s == null) s = "alpha=beta#beta=alpha";
            List<string> pairs = new List<string>(s.Split('#'));
            pairs.ForEach(pair =>
            {
                string[] splitted = pair.Split('=');
                extractedData.Add(
                    new KeyValuePair<string, string>(splitted[0], splitted[1]));
            });

            this.kvList = extractedData;
        }

        public string getValueFromKeyValueList(string key)
        {
            string result = this.kvList.Find(x => x.Key.Equals(key)).Value;
            if (result.Equals("")) result = "undefined_expression";
            return result;
        }
        #endregion
    }
}