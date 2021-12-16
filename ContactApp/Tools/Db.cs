using System;
using MySql.Data.MySqlClient;

namespace ContactApp.Tools
{
    public class Db
    {
        public Db()
        {
        }
        private static string connectionString = "Server=127.0.0.1;DataBase=Projet_Individuel;UserId=root;password=";
        public static MySqlConnection Connection { get => new MySqlConnection(connectionString); }
    }
}
