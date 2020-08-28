using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public static class DB
    {
        public readonly static string MANNSCHAFT = "MVW_MANNSCHAFT";
        public readonly static string MANNSCHAFT_PERSON = "MVW_MANNSCHAFT_PERSON";
        public readonly static string MANNSCHAFT_TURNIER = "MVW_MANNSCHAFT_TURNIER";
        public readonly static string MIGRATION = "MVW_MIGRATION";
        public readonly static string PHYSIOTHERAPEUT = "MVW_PHYSIOTHERAPEUT";
        public readonly static string SPIEL = "MVW_SPIEL";
        public readonly static string SPIELERROLLE = "MVW_SPIELERROLLE";
        public readonly static string TENNISSPIELER = "MVW_TENNISSPIELER";
        public readonly static string TRAINER = "MVW_TRAINER";
        public readonly static string TURNIER = "MVW_TURNIER";

        public static class AUTH
        {
            public readonly static string TABLE = "MVW_AUTH";
            public readonly static string id = "ID";
            public readonly static string username = "usernamePlain";
            public readonly static string password = "passwordPlain";
            public readonly static string role = "role";
        }

        public static class FUSSBALLSPIELER
        {
            public readonly static string TABLE = "MVW_FUSSBALLSPIELER";
            public readonly static string id = "ID";
            public readonly static string fk_person = "PERSON_FK";
            public readonly static string fk_spielerrolle = "SPIELERROLLE_FK";
            public readonly static string gewonnene_spiele = "GEWONNENE_SPIELE";
            public readonly static string left_foot = "LEFT_FOOT";
        }

        public static class HANDBALLSPIELER
        {
            public readonly static string TABLE = "MVW_HANDBALLSPIELER";
            public readonly static string id = "ID";
            public readonly static string fk_person = "PERSON_FK";
            public readonly static string fk_spielerrolle = "SPIELERROLLE_FK";
            public readonly static string gewonnene_spiele = "GEWONNENE_SPIELE";
            public readonly static string left_hand = "LEFT_HAND";
        }

        public static class PERSON
        {
            public readonly static string TABLE = "MVW_PERSON";
            public readonly static string id = "ID";
            public readonly static string vorname = "VORNAME";
            public readonly static string nachname = "NACHNAME";
            public readonly static string geburtsdatum = "GEBURTSDATUM";
            public readonly static string session = "SESSION_ID";
        }
    }
}