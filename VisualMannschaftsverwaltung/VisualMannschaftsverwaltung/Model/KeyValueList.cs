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

        public List<KeyValuePair<string, string>> sortByNumericValue()
        {
            this.kvList.Sort(
                (x, y) => Convert.ToInt32(x.Value).CompareTo(Convert.ToInt32(y.Value)));
            this.kvList.Reverse();

            return this.kvList;
        }

        public void increaseOrUpdateKeyByInt32(string key, int increase)
        {
            KeyValuePair<string, string> result = this.kvList.FindLast(x => x.Key.Equals(key));
            try
            {
                if (!result.Equals(""))
                {
                    int current = Convert.ToInt32(result.Value);
                    int newValue = current + increase;
                    this.kvList.Remove(result);
                    this.kvList.Add(new KeyValuePair<string, string>(key, newValue.ToString()));
                }
            }
            catch (Exception e)
            {
                this.kvList.Add(new KeyValuePair<string, string>(key, increase.ToString()));
            }
        }

        public List<KeyValuePair<string, string>> getKeyList()
        {
            return this.kvList;
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