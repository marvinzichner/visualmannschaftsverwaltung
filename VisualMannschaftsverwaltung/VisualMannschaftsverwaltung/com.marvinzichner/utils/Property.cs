//Name          Marvin Zichner
//Datei         Property.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualMannschaftsverwaltung
{
    public class Property
    {
        #region Eigenschaften
        private Dictionary<string, string> properties;
        #endregion

        #region Accessoren / Modifier
        public Dictionary<string, string> Properties { get => properties; set => properties = value; }
        #endregion

        #region Konstruktoren
        public Property()
        {
            Properties = new Dictionary<string, string>();
        }
        #endregion

        #region Worker
        public void importProperties()
        {
            try { 
                string path = $"{ApplicationContext.getBasePath()}Application.properties";
                Dictionary<string, string> properties = new Dictionary<string, string>();

                foreach (var row in File.ReadAllLines(path))
                {
                    properties.Add(row.Split('=')[0], string.Join("=", row.Split('=').Skip(1).ToArray()));
                }

                this.Properties = properties;
            }
            catch (Exception)
            {
                //ignore
            }
        }

        public string getProperty(string prop)
        {
            if(Properties[prop] != null && Properties[prop] != "")
            {
                return Properties[prop];
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}
