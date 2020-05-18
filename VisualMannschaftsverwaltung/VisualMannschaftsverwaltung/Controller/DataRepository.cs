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
        #endregion

        #region Accessoren / Modifier
        public RepositorySettings RepositorySettings { get => _repositorySettings; set => _repositorySettings = value; }
        public MySqlConnection MySqlConnection { get => _mySqlConnection; set => _mySqlConnection = value; }
        public bool ConnectionReady { get => _connectionReady; set => _connectionReady = value; }
        #endregion

        #region Konstruktoren
        public DataRepository()
        {
            RepositorySettings = new RepositorySettings();
            MySqlConnection = new MySqlConnection();
            ConnectionReady = false;
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

        public List<Person> loadPersonen(string mid = "")
        {
            List<Person> Personen = new List<Person>();
            string joinCondition = "";
            string mannschaftId = "";
            
            if(mid != "")
            {
                joinCondition = "left join mvw_mannschaft_person as mp on mp.FK_PERSON = p.ID";
                mannschaftId = $"and mp.FK_MANNSCHAFT = {mid}";
            }

            if (createConnection()) {
                //Fussballspieler
                string sql = $"select * from MVW_PERSON as p left join MVW_FUSSBALLSPIELER as f on p.ID = f.PERSON_FK {joinCondition} where p.ID = f.PERSON_FK {mannschaftId};";
                MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    FussballSpieler fussballSpieler = new FussballSpieler();
                    fussballSpieler
                         .id(Convert.ToInt32(reader.GetValue(0).ToString()))
                         .name(reader.GetValue(1).ToString())
                         .nachname(reader.GetValue(2).ToString())
                         .birthdate(String.Concat(reader.GetValue(3).ToString().Substring(0, 10)))
                         .sportArt(SportArt.FUSSBALL)
                            .toFussballSpieler()
                            .spielSiege(Convert.ToInt32(reader.GetValue(7).ToString()))
                            .isLeftFeet(Utils.convertFromBasic(reader.GetValue(8).ToString()));  

                    Personen.Add(fussballSpieler);
                }
                
                reader.Close();

                //Handballspieler
                sql = $"select * from MVW_PERSON as p left join MVW_HANDBALLSPIELER as h on p.ID = h.PERSON_FK {joinCondition} where p.ID = h.PERSON_FK {mannschaftId};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    HandballSpieler handballSpieler = new HandballSpieler();
                    handballSpieler
                         .id(Convert.ToInt32(reader.GetValue(0).ToString()))
                         .name(reader.GetValue(1).ToString())
                         .nachname(reader.GetValue(2).ToString())
                         .birthdate(String.Concat(reader.GetValue(3).ToString().Substring(0, 10)))
                         .sportArt(SportArt.HANDBALL)
                            .toHandballSpieler()
                            .spielSiege(Convert.ToInt32(reader.GetValue(7).ToString()))
                            .isLeftHand(Utils.convertFromBasic(reader.GetValue(8).ToString()));

                    Personen.Add(handballSpieler);
                }

                reader.Close();

                //Tennisspieler
                sql = $"select * from MVW_PERSON as p left join MVW_TENNISSPIELER as t on p.ID = t.PERSON_FK {joinCondition} where p.ID = t.PERSON_FK {mannschaftId};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    TennisSpieler tennisSpieler = new TennisSpieler();
                    tennisSpieler
                         .id(Convert.ToInt32(reader.GetValue(0).ToString()))
                         .name(reader.GetValue(1).ToString())
                         .nachname(reader.GetValue(2).ToString())
                         .birthdate(String.Concat(reader.GetValue(3).ToString().Substring(0, 10)))
                         .sportArt(SportArt.TENNIS)
                            .toTennisSpieler()
                            .spielSiege(Convert.ToInt32(reader.GetValue(7).ToString()))
                            .isLeftHand(Utils.convertFromBasic(reader.GetValue(8).ToString()));

                    Personen.Add(tennisSpieler);
                }

                reader.Close();

                //Trainer
                sql = $"select * from MVW_PERSON as p left join MVW_TRAINER as t on p.ID = t.PERSON_FK {joinCondition} where p.ID = t.PERSON_FK {mannschaftId};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Trainer trainer = new Trainer();
                    trainer
                         .id(Convert.ToInt32(reader.GetValue(0).ToString()))
                         .name(reader.GetValue(1).ToString())
                         .nachname(reader.GetValue(2).ToString())
                         .birthdate(String.Concat(reader.GetValue(3).ToString().Substring(0, 10)))
                         .sportArt(SportArt.KEINE)
                            .toTrainer()
                            .hasLicense(Utils.convertFromBasic(reader.GetValue(7).ToString()));

                    Personen.Add(trainer);
                }

                reader.Close();

                //Trainer
                sql = $"select * from MVW_PERSON as p left join MVW_PHYSIOTHERAPEUT as t on p.ID = t.PERSON_FK {joinCondition} where p.ID = t.PERSON_FK {mannschaftId};";
                command = new MySqlCommand(sql, MySqlConnection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Physiotherapeut trainer = new Physiotherapeut();
                    trainer
                         .id(Convert.ToInt32(reader.GetValue(0).ToString()))
                         .name(reader.GetValue(1).ToString())
                         .nachname(reader.GetValue(2).ToString())
                         .birthdate(String.Concat(reader.GetValue(3).ToString().Substring(0, 10)))
                         .sportArt(SportArt.KEINE)
                            .toPhysiotherapeut()
                            .hasLicense(Utils.convertFromBasic(reader.GetValue(7).ToString()));

                    Personen.Add(trainer);
                }

                reader.Close();

                closeConnection();
            }

            return Personen;
        }

        public void removePerson(Person p)
        {
            string personId = p.ID.ToString();
            string className = Utils.basicClassName(p).ToUpper();

            string removePersonalizedData = $"delete from MVW_{className} where PERSON_FK = {personId}";
            string removePerson = $"delete from MVW_PERSON where id = {personId}";
            string removeMannschaftEntries = $"delete from MVW_MANNSCHAFT_PERSON where FK_PERSON = {personId}";

            executeSql(removePersonalizedData);
            executeSql(removePerson);
            executeSql(removeMannschaftEntries);
        }

        public void addPerson(Person p)
        {
            string addPerson = $"insert into MVW_PERSON (VORNAME, NACHNAME, GEBURTSDATUM) " +
                $"values ('{p.Name}', '{p.Nachname}', STR_TO_DATE('{p.Birthdate}', '%d.%m.%Y'))";
            string details = p.getSpecifiedSqlStatement();

            executeSql(addPerson);
            executeSql(details);
        }

        public string getLatestDatabaseVersion()
        {
            string DB_VERSION = "Derzeit nicht bekannt";

            if (createConnection())
            {
                string sql = $"select * from MVW_MIGRATION;";
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
                    string sql = $"select * from MVW_MIGRATION;";
                    MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        DB_VERSION = Convert.ToInt32(reader.GetValue(1));
                    }
                    
                    reader.Close();
                } 
                catch (Exception)
                {
                    DB_VERSION = -1;
                }
                
            }

            closeConnection();
            return DB_VERSION;
        }

        public List<Mannschaft> getMannschaften()
        {
            List<Mannschaft> Mannschaften = new List<Mannschaft>();
            if (createConnection())
            {
                string sql = $"select * from MVW_MANNSCHAFT;";
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
            return Mannschaften;
        }

        public void addPersonToMannschaft(Person p, Mannschaft m)
        {
            string sql = $"insert into mvw_mannschaft_person (FK_PERSON, FK_MANNSCHAFT) values ({p.ID}, {m.ID})";
            executeSql(sql);
        }

        public void removePersonFromMannschaft(Person p, Mannschaft m)
        {
            string sql = $"delete from mvw_mannschaft_person where FK_PERSON={p.ID} and FK_MANNSCHAFT={m.ID}";
            executeSql(sql);
        }

        public void createMannschaft(Mannschaft m)
        {
            string sql = $"insert into MVW_MANNSCHAFT (NAME, TYP, GEWONNENE_SPIELE, GESAMTE_SPIELE) " +
                $"values ('{m.Name}', '{m.SportArt.ToString()}', 0, 0)";
            executeSql(sql);
        }

        public void removeMannschaft(Mannschaft m)
        {
            string sql = $"delete from mvw_mannschaft where ID={m.ID}";
            string removePersonEntries = $"delete from MVW_MANNSCHAFT_PERSON where FK_MANNSCHAFT = {m.ID}";

            executeSql(sql);
            executeSql(removePersonEntries);
        }

        public void updateMannschaftSettings(string id, string name, string type)
        {
            string sql = $"update mvw_mannschaft set NAME='{name}', TYP='{type}' where ID={id}";
            executeSql(sql);
        }
        #endregion
    }
}
