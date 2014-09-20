using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace MySQLCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> insert = new Dictionary<string,string>();
            insert["something"] = "rawr";
            insert["somethingelse"] = "oskdf";

            SQL.Insert("test", insert);
            insert["somethingelse"] = "xxxx";
            SQL.Insert("test", insert);

            MySqlDataReader reader = SQL.Select("SELECT * FROM test");
            while (reader.Read())
            {
                Console.WriteLine(reader["id"] + ": " + reader["something"]);
            }
            Console.ReadKey();
        }
    }
}
