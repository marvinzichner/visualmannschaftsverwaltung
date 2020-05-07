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
        #endregion

        #region Accessoren / Modifier
        public RepositorySettings RepositorySettings { get => _repositorySettings; set => _repositorySettings = value; }
        public MySqlConnection MySqlConnection { get => _mySqlConnection; set => _mySqlConnection = value; }
        #endregion

        #region Konstruktoren
        public DataRepository()
        {
            RepositorySettings = new RepositorySettings();
            MySqlConnection = new MySqlConnection();
        }
        #endregion

        #region Worker
        public bool createConnection()
        {
            try { 
                MySqlConnection.ConnectionString = RepositorySettings.getConnectionString();
                MySqlConnection.Open();
                
                return true;
            } 
            catch (Exception ex)
            {
                return false;
            }
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

        public List<Person> loadPersonen()
        {
            List<Person> Personen = new List<Person>();
            createConnection();

            string sql = $"select * from MVW_PERSON;";
            MySqlCommand command = new MySqlCommand(sql, MySqlConnection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                FussballSpieler f = new FussballSpieler();
                f.ID = Convert.ToInt32(reader.GetValue(0).ToString());
                f.Name = reader.GetValue(1).ToString();
                f.Nachname = reader.GetValue(2).ToString();
                Personen.Add(f);
            }

            closeConnection();
            return Personen;
        }
        #endregion
    }
}
