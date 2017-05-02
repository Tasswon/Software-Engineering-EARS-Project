using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace COSC3506_Project
{
    public class DBConnection
    {
        private MySqlConnection connection;
        private string server, database, password, username;

        /// <summary>
        /// Constructor - create a new database connection instance.
        /// </summary>
        /// <param name="server">The database server address</param>
        /// <param name="database">Database name</param>
        /// <param name="username">Database username</param>
        /// <param name="password">Database password</param>
        public DBConnection (string server, string database, string username, string password)
        {
            this.server = server;
            this.database = database;
            this.username = username;
            this.password = password;

            Initialize();
        }

        /// <summary>
        /// Build the connection string and initialize MySqlConnection.
        /// </summary>
        private void Initialize()
        {
            string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" +
                "UID=" + username + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Open the connection and return the status.
        /// </summary>
        /// <returns>True/false</returns>
        public bool OpenConnection()
        {
            try
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("OpenConnection: " + e);
                /*switch (e)
                {
                    case 0:
                        Console.WriteLine("A connection to the database could not be established.");
                        break;
                    case 1045:
                        Console.WriteLine("Invalid database credentials.");
                        break;
                }*/
                return false;
            }
        }

        /// <summary>
        /// Close the connection and return the status.
        /// </summary>
        /// <returns>True/false</returns>
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
