//Name          Marvin Zichner
//Datum         07.08.2020
//Datei         Spiel.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class Spiel
    {
        #region Eigenschaften
        int _id;
        string _title;
        int _mannschaft_a_fk;
        int _mannschaft_b_fk;
        int _result_a;
        int _result_b;
        string _spieltag;
        int _turnier_fk;
        public enum TeamUnit
        {
            TEAM_A = 0,
            TEAM_B = 1
        }
        #endregion

        #region Accessoren / Modifier
        public int getId() { return this._id; }
        public string getTitle() { return this._title; }
        public string getSpieltag() { return this._spieltag; }
        public int getTurnierId() { return this._turnier_fk; }
        public int getMannschaft(TeamUnit t)
        {
            if (t.Equals(TeamUnit.TEAM_A))
                return this._mannschaft_a_fk;
            if (t.Equals(TeamUnit.TEAM_B))
                return this._mannschaft_b_fk;
            return -1;
        }
        public int getResult(TeamUnit t)
        {
            if (t.Equals(TeamUnit.TEAM_A))
                return this._result_a;
            if (t.Equals(TeamUnit.TEAM_B))
                return this._result_b;
            return -1;
        }

        public Spiel setId(int i) { this._id = i; return this; }
        public Spiel setTitle(string s) { s = ApplicationContext.disarmHijacking(s); this._title = s; return this; }
        public Spiel setSpieltag(string s) { s = ApplicationContext.disarmHijacking(s); this._spieltag = s; return this; }
        public Spiel setTurnierId(int i) { this._turnier_fk = i; return this; }
        public Spiel setMannschaft(TeamUnit t, int i) {
            if (t.Equals(TeamUnit.TEAM_A))
                this._mannschaft_a_fk = i;
            if (t.Equals(TeamUnit.TEAM_B))
                this._mannschaft_b_fk = i;
            return this;
        }
        public Spiel setResult(TeamUnit t, int i)
        {
            if (t.Equals(TeamUnit.TEAM_A))
                this._result_a = i;
            if (t.Equals(TeamUnit.TEAM_B))
                this._result_b = i;
            return this;
        }


        public Spiel getInstance()
        {
            return this;
        }
        #endregion

        #region Konstruktoren
        public Spiel()
        {
            this._id = -1;
            this._title = "undefined_entity";
            this._mannschaft_a_fk = -1;
            this._mannschaft_b_fk = -1;
            this._result_a = 0;
            this._result_b = 0;
            this._spieltag = "01.01.1901";
            this._turnier_fk = -1;
        }
       
        public Spiel(Spiel s) 
        {
            this._id = s._id;
            this._title = s._title;
            this._mannschaft_a_fk = s._mannschaft_a_fk;
            this._mannschaft_b_fk = s._mannschaft_b_fk;
            this._result_a = s._result_a;
            this._result_b = s._result_b;
            this._spieltag = s._spieltag;
            this._turnier_fk = s._turnier_fk;
        }
        #endregion

        #region Worker
        
        #endregion
    }
}