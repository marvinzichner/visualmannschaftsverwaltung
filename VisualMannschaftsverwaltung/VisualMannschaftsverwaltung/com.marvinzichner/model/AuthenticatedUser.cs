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
        bool isAuthenticated;
        #endregion

        #region Accessoren / Modifier
        public AuthenticatedUser setAuthenticatedRole(AuthenticatedRole role)
        {
            authenticatedRole = role;
            markUserAsAuthenticated();
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
        public AuthenticatedUser setSessionId(string sessionid)
        {
            this.sessionId = sessionid;
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

        public string getSessionId()
        {
            return sessionId;
        }

        public string getUsername()
        {
            return username;
        }
        #endregion

        #region Konstruktoren
        public AuthenticatedUser()
        {
            this.setUserId(-1);
            this.setUsername("unauthorized");
            this.setAuthenticatedRole(AuthenticatedRole.USER);
            this.isAuthenticated = false;
        }

        public AuthenticatedUser(AuthenticatedUser user)
        {
            this.setUserId(user.getId());
            this.setUsername(user.getUsername());
            this.setAuthenticatedRole(user.getAuthenticatedRole());
        }
        #endregion

        #region Worker
        public bool isAdmin()
        {
            if(isAuthenticated && getAuthenticatedRole() == AuthenticatedRole.ADMIN)
                return true;

            return false;
        }

        public bool isUser()
        {
            if (isAuthenticated && getAuthenticatedRole() == AuthenticatedRole.USER)
                return true;

            return false;
        }

        public void killUserSession()
        {
            this.isAuthenticated = false;
            this.setUserId(-1);
            this.setUsername("KILLED_SESSION");
            this.setAuthenticatedRole(AuthenticatedRole.USER);
        }

        public void markUserAsAuthenticated()
        {
            this.isAuthenticated = true;
        }

        public bool hasAccess()
        {
            if (isAuthenticated)
                return true;

            return false;
        }
        #endregion
    }
}