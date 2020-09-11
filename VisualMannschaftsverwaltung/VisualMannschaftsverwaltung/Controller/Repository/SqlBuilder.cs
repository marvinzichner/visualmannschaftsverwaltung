using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public class SqlBuilder
    {
        #region Eigenschaften
        private string action;
        private List<string> origin;
        private List<string> conditions;
        #endregion

        #region Accessoren / Modifier
       
        #endregion

        #region Konstruktoren
        public SqlBuilder()
        {
            origin = new List<string>();
        }
        #endregion

        #region Worker
        public SqlBuilder select()
        {
            action = "select";
            return this;
        }
        public SqlBuilder insert()
        {
            action = "insert";
            return this;
        }
        public SqlBuilder update()
        {
            action = "update";
            return this;
        }
        public SqlBuilder delete()
        {
            action = "delete";
            return this;
        }
        public SqlBuilder table(string table)
        {
            origin.Add(table);
            return this;
        }
        public SqlBuilder where(string key, string value)
        {
            conditions.Add($"`{key}`=`{value}`");
            return this;
        }
        public SqlBuilder where(string key, int value)
        {
            conditions.Add($"`{key}`={value}");
            return this;
        }

        public string build()
        {
            string sql = "";

            if (action == "select") { 
                sql = $"{sql} select *";
                //TODO: add logic 
            }

            return sql;
        }
        #endregion
    }
}