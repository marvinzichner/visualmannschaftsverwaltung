//Name          Marvin Zichner
//Datei         DataRepository.cs
//Aenderungen   Initales Erzeugen und erste Eigenschaften

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace VisualMannschaftsverwaltung
{
    public class DataRepository
    {
        #region Eigenschaften
        private RepositorySettings _repositorySettings;
        private MySqlConnection _mySqlConnection;
        private bool _connectionReady;
        private string _session;
        private bool _sessionQuery;
        #endregion

        #region Accessoren / Modifier
        public RepositorySettings RepositorySettings { get => _repositorySettings; set => _repositorySettings = value; }
        public MySqlConnection MySqlConnection { get => _mySqlConnection; set => _mySqlConnection = value; }
        public bool ConnectionReady { get => _connectionReady; set => _connectionReady = value; }
        public string Session { get => _session; set => _session = value; }
        public bool SessionQuery { get => _sessionQuery; set => _sessionQuery = value; }
        #endregion

        #region Konstruktoren
        public DataRepository()
        {
            RepositorySettings = new RepositorySettings();
            MySqlConnection = new MySqlConnection();
            ConnectionReady = false;
            Session = "ALL";
        }
        #endregion

        #region Worker
        public bool createConnection()
        {
            try { 
                MySqlConnection.ConnectionString = RepositorySettings.getConnectionString();
                MySqlConnection.Open();
                ConnectionReady = true;
                
                return true;
            } 
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool checkConnection()
        {
            createConnection();
            closeConnection();
            return ConnectionReady;
        }

        public void closeConnection()
        {
            MySqlConnection.Close();
        }

        public int executeSql(string sql)
        {
            int rowsAffected = 0;
            if (createConnection())
            {
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                rowsAffected = command.ExecuteNonQuery();
            }
            else
            {
                rowsAffected = 0;
            }

            closeConnection();
            return rowsAffected;
        }

        public DataRepository setSession(string s)
        {
            this.Session = s;
            return this;
        }

        public DataRepository enableSessionbasedQueries()
        {
            this.SessionQuery = true;
            return this;
        }

        public DataRepository disableSessionbasedQueries()
        {
            this.SessionQuery = false;
            return this;
        }

        public List<Person> loadPersonen(string mid = "")
        {
            mid = ApplicationContext.disarmHijacking(mid);

            List<Person> Personen = new List<Person>();
            string joinCondition = "";
            string mannschaftId = "";
            string sessionSql = "";
            
            if(mid != "")
            {
                joinCondition = $"left join {DB.MANNSCHAFT_PERSON.TABLE} as mp on mp.{DB.MANNSCHAFT_PERSON.fkPerson} = p.ID";
                mannschaftId = $"and mp.{DB.MANNSCHAFT_PERSON.fkMannschaft} = {mid}";
            }
            if (SessionQuery)
            {
                sessionSql = $"and p.SESSION_ID = '{Session}'";
            }

            if (createConnection()) {
                //Fussballspieler
                string sql = $"" +
                    $"select *, DATE_FORMAT({DB.PERSON.geburtsdatum}, \"%d.%m.%Y\") as BDAY " +
                    $"from {DB.PERSON.TABLE} as p " +
                    $"left join {DB.FUSSBALLSPIELER.TABLE} as f on p.{DB.PERSON.id} = f.{DB.FUSSBALLSPIELER.fk_person} {joinCondition} " +
                    $"where p.{DB.PERSON.id} = f.{DB.FUSSBALLSPIELER.fk_person} {mannschaftId} {sessionSql};";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FussballSpieler fussballSpieler = new FussballSpieler();
                    fussballSpieler
                         .id(Convert.ToInt32(reader[DB.PERSON.id].ToString()))
                         .name(reader[DB.PERSON.vorname].ToString())
                         .nachname(reader[DB.PERSON.nachname].ToString())
                         .birthdate(reader[DB.PERSON.geburtsdatum].ToString())
                         .sportArt(SportArt.FUSSBALL)
                            .toFussballSpieler()
                            .spielSiege(Convert.ToInt32(reader[DB.FUSSBALLSPIELER.gewonnene_spiele].ToString()))
                            .isLeftFeet(Utils.convertFromBasic(reader[DB.FUSSBALLSPIELER.left_foot].ToString()));  

                    Personen.Add(fussballSpieler);
                }
                
                reader.Close();

                //Handballspieler
                sql = $"select *, DATE_FORMAT({DB.PERSON.geburtsdatum}, \"%d.%m.%Y\") as BDAY " +
                    $"from {DB.PERSON.TABLE} as p left join {DB.HANDBALLSPIELER.TABLE} as h " +
                    $"on p.{DB.PERSON.id} = h.{DB.HANDBALLSPIELER.fk_person} {joinCondition} " +
                    $"where p.{DB.PERSON.id} = h.{DB.HANDBALLSPIELER.fk_person} {mannschaftId} {sessionSql};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HandballSpieler handballSpieler = new HandballSpieler();
                    handballSpieler
                         .id(Convert.ToInt32(reader[DB.PERSON.id].ToString()))
                         .name(reader[DB.PERSON.vorname].ToString())
                         .nachname(reader[DB.PERSON.nachname].ToString())
                         .birthdate(reader[DB.PERSON.geburtsdatum].ToString())
                         .sportArt(SportArt.HANDBALL)
                            .toHandballSpieler()
                            .spielSiege(Convert.ToInt32(reader.GetValue(8).ToString()))
                            .isLeftHand(Utils.convertFromBasic(reader.GetValue(9).ToString()));

                    Personen.Add(handballSpieler);
                }

                reader.Close();

                //Tennisspieler
                sql = $"select *, DATE_FORMAT({DB.PERSON.geburtsdatum}, \"%d.%m.%Y\") as BDAY " +
                    $"from {DB.PERSON.TABLE} as p left join {DB.TENNISSPIELER.TABLE} as t " +
                    $"on p.{DB.PERSON.id} = t.{DB.TENNISSPIELER.fk_person} {joinCondition} " +
                    $"where p.{DB.PERSON.id} = t.{DB.TENNISSPIELER.fk_person} {mannschaftId} {sessionSql};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TennisSpieler tennisSpieler = new TennisSpieler();
                    tennisSpieler
                         .id(Convert.ToInt32(reader[DB.PERSON.id].ToString()))
                         .name(reader[DB.PERSON.vorname].ToString())
                         .nachname(reader[DB.PERSON.nachname].ToString())
                         .birthdate(reader[DB.PERSON.geburtsdatum].ToString())
                         .sportArt(SportArt.TENNIS)
                            .toTennisSpieler()
                            .spielSiege(Convert.ToInt32(reader.GetValue(8).ToString()))
                            .isLeftHand(Utils.convertFromBasic(reader.GetValue(9).ToString()));

                    Personen.Add(tennisSpieler);
                }

                reader.Close();

                //Trainer
                sql = $"select *, DATE_FORMAT({DB.PERSON.geburtsdatum}, \"%d.%m.%Y\") as BDAY " +
                    $"from {DB.PERSON.TABLE} as p left join {DB.TRAINER.TABLE} as t " +
                    $"on p.{DB.PERSON.id} = t.{DB.TRAINER.fk_person} {joinCondition} " +
                    $"where p.{DB.PERSON.id} = t.{DB.TRAINER.fk_person} {mannschaftId} {sessionSql};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Trainer trainer = new Trainer();
                    trainer
                        .id(Convert.ToInt32(reader[DB.PERSON.id].ToString()))
                         .name(reader[DB.PERSON.vorname].ToString())
                         .nachname(reader[DB.PERSON.nachname].ToString())
                         .birthdate(reader[DB.PERSON.geburtsdatum].ToString())
                         .sportArt(SportArt.KEINE)
                            .toTrainer()
                            .hasLicense(Utils.convertFromBasic(reader[DB.TRAINER.hasLicense].ToString()));

                    Personen.Add(trainer);
                }

                reader.Close();

                //PHYSIOTHERAPEUT
                sql = $"select *, DATE_FORMAT({DB.PERSON.geburtsdatum}, \"%d.%m.%Y\") as BDAY " +
                    $"from {DB.PERSON.TABLE} as p left join {DB.PHYSIOTHERAPEUT.TABLE} as t " +
                    $"on p.{DB.PERSON.id} = t.{DB.PHYSIOTHERAPEUT.fkPerson} {joinCondition} " +
                    $"where p.{DB.PERSON.id} = t.{DB.PHYSIOTHERAPEUT.fkPerson} {mannschaftId} {sessionSql};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Physiotherapeut physiotherapeut = new Physiotherapeut();
                    physiotherapeut
                         .id(Convert.ToInt32(reader[DB.PERSON.id].ToString()))
                         .name(reader[DB.PERSON.vorname].ToString())
                         .nachname(reader[DB.PERSON.nachname].ToString())
                         .birthdate(reader[DB.PERSON.geburtsdatum].ToString())
                         .sportArt(SportArt.KEINE)
                            .toPhysiotherapeut()
                            .hasLicense(Utils.convertFromBasic(reader[DB.PHYSIOTHERAPEUT.hasLicense].ToString()));

                    Personen.Add(physiotherapeut);
                }

                reader.Close();

                closeConnection();
            }

            return Personen;
        }

        public bool databaseIsConnectedAndReady()
        {
            MySqlConnection.ConnectionString = RepositorySettings.getConnectionString(); 
            try
            {
                MySqlConnection.Open();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                MySqlConnection.Close();
            }
        }

        public void removePerson(Person p)
        {
            string personId = p.ID.ToString();
            string className = Utils.basicClassName(p).ToUpper();

            string removePersonalizedData = $"delete from MVW_{className} where PERSON_FK = {personId}";
            string removePerson = $"delete from {DB.PERSON.TABLE} where {DB.PERSON.id} = {personId}";
            string removeMannschaftEntries = $"delete from {DB.MANNSCHAFT_PERSON.TABLE} where FK_PERSON = {personId}";

            executeSql(removePersonalizedData);
            executeSql(removePerson);
            executeSql(removeMannschaftEntries);
        }

        public void addPerson(Person p, string session)
        {
            session = ApplicationContext.disarmHijacking(session);

            string addPerson = $"insert into {DB.PERSON.TABLE} ({DB.PERSON.vorname}, {DB.PERSON.nachname}, {DB.PERSON.geburtsdatum}, {DB.PERSON.session}) " +
                $"values ('{p.Name}', '{p.Nachname}', STR_TO_DATE('{p.Birthdate}', '%d.%m.%Y'), '{session}')";
            string details = p.getSpecifiedSqlStatement();

            executeSql(addPerson);
            executeSql(details);
        }

        public void getRankedMannschaftenByTurnier(int turnierId)
        {
            string sqlPartA = $"SELECT *, SUM(RESULT_A) as CALCULATED_A FROM {DB.SPIEL.TABLE} " +
                $"where {DB.SPIEL.fkTurnier}={turnierId.ToString()} " +
                $"group by `{DB.SPIEL.fkMannschaftA}` order by CALCULATED_A desc";
        }

        public void createNewSpielOfTurnier(string title, int playerA, int playerB, string spieltag, int turnierFk)
        {
            title = ApplicationContext.disarmHijacking(title);
            spieltag = ApplicationContext.disarmHijacking(spieltag);

            string sql = $"insert into {DB.SPIEL.TABLE} " +
                $"({DB.SPIEL.title}, {DB.SPIEL.fkMannschaftA}, {DB.SPIEL.fkMannschaftB}, {DB.SPIEL.resultA}, {DB.SPIEL.resultB}, {DB.SPIEL.spieltag}, {DB.SPIEL.fkTurnier}, {DB.SPIEL.sessionId}) " +
                $"values ('{title}', {playerA.ToString()}, {playerB.ToString()}, 0, 0, '{spieltag}', {turnierFk}, '{Session}')";

            executeSql(sql);
        }

        public void updateSpielWithResults(int id, int a, int b)
        {
            string sql = $"update {DB.SPIEL.TABLE} " +
                $"set {DB.SPIEL.resultA}={a.ToString()}, {DB.SPIEL.resultB}={b.ToString()} " +
                $"where ID = {id.ToString()}";

            executeSql(sql);
        }

        public void updatePerson(Person p, string session)
        {
            session = ApplicationContext.disarmHijacking(session);

            string addPerson = $"update {DB.PERSON.TABLE} set " +
                $"{DB.PERSON.vorname}='{p.Name}', {DB.PERSON.nachname}='{p.Nachname}', {DB.PERSON.geburtsdatum}=STR_TO_DATE('{p.Birthdate}', '%d.%m.%Y') " +
                $"where {DB.PERSON.id} = {p.ID}";
            string details = p.getSpecifiedUpdateSqlStatement(p.ID.ToString());

            executeSql(addPerson);
            executeSql(details);
        }

        public bool checkCredentials(string username, string password)
        {
            username = ApplicationContext.disarmHijacking(username);
            password = ApplicationContext.disarmHijacking(password);

            bool result = false;
            if (createConnection())
            {
                string sql = $"select * from {DB.AUTH.TABLE} " +
                    $"where {DB.AUTH.username}='{username}' and {DB.AUTH.password}='{password}'";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = true;
                }
            }
            closeConnection();

            return result;
        }

        public AuthenticatedRole getRoleFromUsername(string username)
        {
            username = ApplicationContext.disarmHijacking(username);
            AuthenticatedRole role = AuthenticatedRole.USER;

            if (createConnection())
            {
                string sql = $"select * from {DB.AUTH.TABLE} where {DB.AUTH.username}='{username}'";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string roleString = reader[DB.AUTH.role].ToString();

                    if (roleString.Equals("ADMIN"))
                    {
                        role = AuthenticatedRole.ADMIN;
                    }
                }
            }
            closeConnection();

            return role;
        }

        public string getLatestDatabaseVersion()
        {
            string DB_VERSION = "Derzeit nicht bekannt";

            if (createConnection())
            {
                string sql = $"select * from {DB.MIGRATION.TABLE};";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DB_VERSION = reader.GetValue(1).ToString();
                }
            }
            closeConnection();

            return DB_VERSION;
        }

        public int getLatestVersion()
        {
            int DB_VERSION = -1;
            
            if (createConnection())
            {
                try { 
                    string sql = $"select * from {DB.MIGRATION.TABLE};";
                    MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DB_VERSION = Convert.ToInt32(reader.GetValue(1));
                    }
                    
                    reader.Close();
                } 
                catch (Exception e)
                {
                    Console.Write(e);
                    DB_VERSION = -1;
                }
                
            }

            closeConnection();
            return DB_VERSION;
        }

        public List<Mannschaft> getMannschaften()
        {
            string sessionLoader = "";
            if (SessionQuery)
            {
                sessionLoader = $" where SESSION_ID = '{Session}' ";
            }

            List<Mannschaft> Mannschaften = new List<Mannschaft>();
            if (createConnection())
            {
                string sql = $"select * from {DB.MANNSCHAFT.TABLE} {sessionLoader} order by {DB.MANNSCHAFT.name} ASC;";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Mannschaft mannschaft = new Mannschaft();
                    mannschaft
                        .id(Convert.ToInt32(reader.GetValue(0)))
                        .name(reader.GetValue(1).ToString())
                        .sportArt(
                            (SportArt)Enum.Parse(typeof(SportArt), reader.GetValue(2).ToString()))
                        .gewonneneSpiele(Convert.ToInt32(reader.GetValue(3)))
                        .gesamteSpiele(Convert.ToInt32(reader.GetValue(4)));
                        //.personen(loadPersonen(reader.GetValue(0).ToString()));

                    Mannschaften.Add(mannschaft);
                }
                
                reader.Close();
            }
            closeConnection();

            Mannschaften.ForEach(mannschaft => {
                mannschaft.personen(loadPersonen(mannschaft.ID.ToString()));
            });

            return Mannschaften;
        }

        public void addPersonToMannschaft(Person p, Mannschaft m)
        {
            string sql = $"insert into {DB.MANNSCHAFT_PERSON.TABLE} " +
                $"({DB.MANNSCHAFT_PERSON.fkPerson}, {DB.MANNSCHAFT_PERSON.fkMannschaft}) " +
                $"values ({p.ID}, {m.ID})";
            executeSql(sql);
        }

        public void removePersonFromMannschaft(Person p, Mannschaft m)
        {
            string sql = $"delete from {DB.MANNSCHAFT_PERSON.TABLE} " +
                $"where {DB.MANNSCHAFT_PERSON.fkPerson}={p.ID} and {DB.MANNSCHAFT_PERSON.fkMannschaft}={m.ID}";
            executeSql(sql);
        }

        public void createMannschaft(Mannschaft m)
        {
            string sql = $"insert into {DB.MANNSCHAFT.TABLE} " +
                $"({DB.MANNSCHAFT.name}, {DB.MANNSCHAFT.type}, {DB.MANNSCHAFT.gewonneneSpiele}, {DB.MANNSCHAFT.gesamteSpiele}, {DB.MANNSCHAFT.session}) " +
                $"values ('{m.Name}', '{m.SportArt.ToString()}', 0, 0, '{Session}')";
            executeSql(sql);
        }

        public void removeMannschaft(Mannschaft m)
        {
            string sql = $"delete from {DB.MANNSCHAFT.TABLE} where {DB.MANNSCHAFT.id}={m.ID}";
            string removePersonEntries = $"delete from {DB.MANNSCHAFT_PERSON.TABLE} where {DB.MANNSCHAFT_PERSON.fkMannschaft} = {m.ID}";

            executeSql(sql);
            executeSql(removePersonEntries);
        }

        public void updateMannschaftSettings(string id, string name, string type)
        {
            id = ApplicationContext.disarmHijacking(id);
            name = ApplicationContext.disarmHijacking(name);
            type = ApplicationContext.disarmHijacking(type);

            string sql = $"update {DB.MANNSCHAFT.TABLE} " +
                $"set {DB.MANNSCHAFT.name}='{name}', {DB.MANNSCHAFT.type}='{type}' " +
                $"where {DB.MANNSCHAFT.id}={id}";
            executeSql(sql);
        }

        public void addMappingOfTurnierAndMannschaft(string mannschaft, string turnier)
        {
            mannschaft = ApplicationContext.disarmHijacking(mannschaft);
            turnier = ApplicationContext.disarmHijacking(turnier);

            string sql = $"insert into {DB.MANNSCHAFT_TURNIER.TABLE} " +
                $"({DB.MANNSCHAFT_TURNIER.fkMannschaft}, {DB.MANNSCHAFT_TURNIER.fkTurnier}) " +
                $"values ('{mannschaft}', '{turnier}')";
            executeSql(sql);
        }

        public void deleteMappingOfTurnierAndMannschaft(string mannschaft, string turnier)
        {
            mannschaft = ApplicationContext.disarmHijacking(mannschaft);
            turnier = ApplicationContext.disarmHijacking(turnier);

            string sql = $"delete from {DB.MANNSCHAFT_TURNIER.TABLE} " +
                $"where {DB.MANNSCHAFT_TURNIER.fkMannschaft}={mannschaft} and {DB.MANNSCHAFT_TURNIER.fkTurnier}={turnier}";
            executeSql(sql);
        }

        public void deleteTurnierAndAllDependentEntities(string turnier)
        {
            turnier = ApplicationContext.disarmHijacking(turnier);

            string sql = $"delete from {DB.MANNSCHAFT_TURNIER.TABLE} " +
                $"where {DB.MANNSCHAFT_TURNIER.fkTurnier}={turnier}";
            executeSql(sql);

            string sqlTurnier = $"delete from {DB.TURNIER.TABLE} " +
                $"where {DB.TURNIER.id}={turnier}";
            executeSql(sqlTurnier);
        }

        public List<Spiel> getSpiele()
        {
            List<Spiel> Spiele = new List<Spiel>();

            if (createConnection())
            {
                string sql = $"select * from {DB.SPIEL.TABLE} where {DB.SPIEL.sessionId} = '{Session}';";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Spiel spiel = new Spiel();
                    spiel.setId(Utils.convertToInteger32(reader[DB.SPIEL.id].ToString()))
                        .setTitle(reader[DB.SPIEL.title].ToString())
                        .setMannschaft(
                            Spiel.TeamUnit.TEAM_A, 
                            Utils.convertToInteger32(
                                reader[DB.SPIEL.fkMannschaftA].ToString()))
                        .setMannschaft(
                            Spiel.TeamUnit.TEAM_B,
                            Utils.convertToInteger32(
                                reader[DB.SPIEL.fkMannschaftB].ToString()))
                        .setResult(
                            Spiel.TeamUnit.TEAM_A,
                            Utils.convertToInteger32(
                                reader[DB.SPIEL.resultA].ToString()))
                        .setResult(
                            Spiel.TeamUnit.TEAM_B,
                            Utils.convertToInteger32(
                                reader[DB.SPIEL.resultB].ToString()))
                        .setSpieltag(
                            reader[DB.SPIEL.spieltag].ToString())
                        .setTurnierId(
                            Utils.convertToInteger32(reader[DB.SPIEL.fkTurnier].ToString()));

                    Spiele.Add(spiel);
                }
            }

            return Spiele;
        }

        public List<Turnier> getTurniere()
        {
            List<Turnier> Turniere = new List<Turnier>();
            string sessionLoader = "";

            if (SessionQuery) { sessionLoader = $" where SESSION_ID = '{Session}' "; }

            if (createConnection())
            {
                string sql = $"select * from {DB.TURNIER.TABLE} {sessionLoader} order by {DB.TURNIER.name} ASC;";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Turnier turnier = new Turnier();
                    turnier
                        .setId(reader[DB.TURNIER.id].ToString())
                        .setType(reader[DB.TURNIER.type].ToString())
                        .setName(reader[DB.TURNIER.name].ToString());
                    Turniere.Add(turnier);
                }

                closeConnection();
            }

            disableSessionbasedQueries();
            List<Mannschaft> possibleMannschaften = getMannschaften();
            enableSessionbasedQueries();
            Turniere.ForEach(turnier =>
            {
                if (createConnection()) { 
                    string query = $"select * from {DB.MANNSCHAFT_TURNIER.TABLE} " +
                        $"where {DB.MANNSCHAFT_TURNIER.fkTurnier}='{turnier.getId()}'";
                    MySqlCommand command2 = new MySqlCommand(query, MySqlConnection);
                    MySqlDataReader reader2 = command2.ExecuteReader();

                    while (reader2.Read())
                    {
                        Mannschaft m = new Mannschaft();
                        m = possibleMannschaften.Find(
                            x => x.ID == Convert.ToInt32(
                                reader2[DB.MANNSCHAFT_TURNIER.fkMannschaft]));
                        turnier.addMannschaft(m);
                    }
                    closeConnection();
                }
            });

            return Turniere;
        }
        
        public void addTurnier(string name, SportArt sportArt)
        {
            name = ApplicationContext.disarmHijacking(name);

            string sql = $"insert into {DB.TURNIER.TABLE} " +
                $"({DB.TURNIER.name}, {DB.TURNIER.type}, {DB.TURNIER.session}) " +
                $"values ('{name}', '{sportArt.ToString()}', '{this.Session}')";
            executeSql(sql);
        }
        #endregion
    }
}
