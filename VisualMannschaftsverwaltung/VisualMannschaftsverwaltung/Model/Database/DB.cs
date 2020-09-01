using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VisualMannschaftsverwaltung
{
    public static class DB
    {
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

        public static class MANNSCHAFT
        {
            public readonly static string TABLE = "MVW_MANNSCHAFT";
            public readonly static string id = "ID";
            public readonly static string name = "NAME";
            public readonly static string type = "TYP";
            public readonly static string gewonneneSpiele = "GEWONNENE_SPIELE";
            public readonly static string gesamteSpiele = "GESAMTE_SPIELE";
            public readonly static string session = "SESSION_ID";
        }

        public static class MANNSCHAFT_PERSON
        {
            public readonly static string TABLE = "MVW_MANNSCHAFT_PERSON";
            public readonly static string id = "ID";
            public readonly static string fkPerson = "FK_PERSON";
            public readonly static string fkMannschaft = "FK_MANNSCHAFT";
        }

        public static class MANNSCHAFT_TURNIER
        {
            public readonly static string TABLE = "MVW_MANNSCHAFT_TURNIER";
            public readonly static string id = "ID";
            public readonly static string fkMannschaft = "MANNSCHAFT_ID";
            public readonly static string fkTurnier = "TURNIER_ID";
        }

        public static class MIGRATION
        {
            public readonly static string TABLE = "MVW_MIGRATION";
            public readonly static string version = "VERSION";
            public readonly static string name = "NAME";
            public readonly static string created = "CREATED";
        }

        public static class PHYSIOTHERAPEUT
        {
            public readonly static string TABLE = "MVW_PHYSIOTHERAPEUT";
            public readonly static string id = "ID";
            public readonly static string fkPerson = "PERSON_FK";
            public readonly static string fkSpielerrolle = "SPIELERROLLE_FK";
            public readonly static string hasLicense = "HAS_LICENSE";
        }

        public static class SPIEL
        {
            public readonly static string TABLE = "MVW_SPIEL";
            public readonly static string id = "ID";
            public readonly static string title = "TITEL";
            public readonly static string fkMannschaftA = "MANNSCHAFT_A_FK";
            public readonly static string fkMannschaftB = "MANNSCHAFT_B_FK";
            public readonly static string resultA = "RESULT_A";
            public readonly static string resultB = "RESULT_B";
            public readonly static string spieltag = "SPIELTAG";
            public readonly static string fkTurnier = "TURNIER_FK";
            public readonly static string sessionId = "SESSION_ID";
        }

        public static class SPIELERROLLE
        {
            public readonly static string TABLE = "MVW_SPIELERROLLE";
            public readonly static string id = "ID";
            public readonly static string name = "NAME";
        }

        public static class TENNISSPIELER
        {
            public readonly static string TABLE = "MVW_TENNISSPIELER";
            public readonly static string id = "ID";
            public readonly static string fk_person = "PERSON_FK";
            public readonly static string fk_spielerrolle = "SPIELERROLLE_FK";
            public readonly static string gewonnene_spiele = "GEWONNENE_SPIELE";
            public readonly static string leftArm = "LEFT_ARM";
        }

        public static class TRAINER
        {
            public readonly static string TABLE = "MVW_TRAINER";
            public readonly static string id = "ID";
            public readonly static string fk_person = "PERSON_FK";
            public readonly static string fk_spielerrolle = "SPIELERROLLE_FK";
            public readonly static string gewonneneSpiele = "GEWONNENE_SPIELE";
            public readonly static string hasLicense = "HAS_LICENSE";
        }

        public static class TURNIER
        {
            public readonly static string TABLE = "MVW_TURNIER";
            public readonly static string id = "ID";
            public readonly static string name = "NAME";
            public readonly static string type = "TYPE";
            public readonly static string turnierart = "TURNIERART";
            public readonly static string session = "SESSION_ID";
        }
    }
}