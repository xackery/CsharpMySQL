using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySQLCLI
{
    class SQL
    {
        private static MySqlConnection connection;
        private static MySqlCommand lastCommand;
        private static SQL mInstance;

        private static void Init()
        {
            if (connection == null)
            {
                connection = new MySqlConnection("Server=localhost;Database=test;username=root");
                connection.Open();
            }
        }

        /// <summary>
        /// Creates a SELECT statement and returns the data in a reader format 
        /// </summary>
        /// <param name="query">SELECT * FROM table</param>
        /// <returns>(while (reader.Read()) { //reader["id] })</returns>
        public static MySqlDataReader Select(string query) {
            Init();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = query;
            lastCommand = command;
            return command.ExecuteReader();            
        }

        /// <summary>
        /// Creates a INSERT statement
        /// </summary>
        /// <param name="table">Table name to insert into</param>
        /// <param name="insert">Keyvalue pair, name the column, and value inserted</param>
        public static void Insert(string table, Dictionary<string, string> insert)
        {
            Init();
            MySqlCommand command = connection.CreateCommand();
            string columns = "";
            string parameters = "";
            foreach (KeyValuePair<string, string> row in insert)
            {
                columns += row.Key + ", ";
                parameters += "@" + row.Key + ", ";
                command.Parameters.AddWithValue("@"+row.Key, row.Value);
            }
            columns = columns.Substring(0,columns.Length-2);
            parameters = parameters.Substring(0,parameters.Length - 2);
            command.CommandText = "INSERT INTO " + table + " ("+columns+") VALUES ("+parameters+")";
            command.ExecuteNonQuery();
        }

        public static MySqlCommand GetLastCommand()
        {
            return lastCommand;
        }
    }
}
