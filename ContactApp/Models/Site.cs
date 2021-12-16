using System;
using System.Collections.Generic;
using ContactApp.Tools;
using MySql.Data.MySqlClient;

namespace ContactApp.Models
{
    public class Site
    {        
        private int id;
        private string city;

        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int Id { get => id; set => id = value; }
        public string City { get => city; set => city = value; }
        

        public Site()
        {
        }

        public bool AddSite()
        {
            request = "INSERT INTO site (site_city) values (@site_city)";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@site_city", City));
            connection.Open();
            Id = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return id > 0;
        }

        public bool DeleteSite()
        {
            request = "DELETE FROM site WHERE id_site = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = command.ExecuteNonQuery() ;
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public static Site GetSite(int id)
        {
            Site site = null;
            request = "SELECT id_site, site_city FROM site where id_site = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            connection.Open();
            reader = command.ExecuteReader();
            if(reader.Read())
            {
                site = new Site()
                {
                    Id = id,
                    City = reader.GetString(1)
                };
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return site;
        }

        public static List<Site> GetSites()
        {
            List<Site> sites = new List<Site>();
            request = "SELECT id_site, site_city FROM site";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Site site = new Site()
                {
                    Id = reader.GetInt32(0),
                    City = reader.GetString(1)
                };

                sites.Add(site);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return sites;
        }

    }
}
