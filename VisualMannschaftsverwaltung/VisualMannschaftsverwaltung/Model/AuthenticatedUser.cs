//Name          Marvin Zichner
//Datum         14.08.2020
//Datei         AuthenticatedUser.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;

namespace VisualMannschaftsverwaltung
{
    public class AuthenticatedUser
    {
        #region Eigenschaften
        AuthenticatedRole authenticatedRole;
        string username;
        int userId;
        string sessionId;
        #endregion

        #region Accessoren / Modifier
        public AuthenticatedUser setAuthenticatedRole(AuthenticatedRole role)
        {
            authenticatedRole = role;
            return this;
        }
        public AuthenticatedUser setUserId(int id)
        {
            userId = id;
            return this;
        }
        public AuthenticatedUser setUsername(string username)
        {
            this.username = username;
            return this;
        }

        public AuthenticatedRole getAuthenticatedRole()
        {
            return authenticatedRole;
        }

        public int getId()
        {
            return userId;
        }

        public string getUsername()
        {
            return username;
        }
        #endregion

        #region Konstruktoren
        public AuthenticatedUser()
        {

        }

        public AuthenticatedUser(AuthenticatedUser user)
        {

        }
        #endregion

        #region Worker
        #endregion
    }
}