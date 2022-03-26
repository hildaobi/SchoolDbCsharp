using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MySql.Data.MySqlClient;

namespace SchoolDbCsharp.Models
{
    public class SchoolDbContext
    {
        //These are readonly "secret" properties.
        //Only the SchoolDbContext class can use them.
        //Change these to match your own local database!

        private static string User { get { return "root"; } }
        private static string Password{ get { return "root"; } }
        private static string Database{ get { return "school"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } }
       
        //ConnectionString is a series of credentials used to connect to the datadase.
        protected static string ConnectionString
        {
            get
            {
                return "server = " + Server
                    + "; user = " + User
                    + "; database = " + Database
                    + "; port = " + Port
                    + "; password = " + Password;
            }        
        }
        //This is the method we actually used to get the database!
        ///<summary>
        ///Returns connection to the school database
       /// </summary>
       /// <example>
       /// private SchoolDbContext School= new SchoolDbContext();
       /// MySqlConnection Conn = School.AccessDatabase();
       /// </example>
       /// <returns>A MySqlconnection Object</returns>
       
        public MySqlConnection AccessDatabase()
        {
            //spacific connection to our database.
            return new MySqlConnection(ConnectionString);
        }
    }
}