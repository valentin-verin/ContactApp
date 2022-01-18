using System;
using System.Collections.Generic;
using ContactApp.Tools;
using MySql.Data.MySqlClient;

namespace ContactApp.Models
{
    public class Service
    {
        private int id;
        private string serviceName;

        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int Id { get => id; set => id = value; }
        public string ServiceName { get => serviceName; set => serviceName = value; }

        public Service()
        {
        }

        public bool AddService()
        {
            request = "INSERT INTO service (service_name) values (@serviceName)";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@serviceName", ServiceName));
            connection.Open();
            Id = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return CountService();
        }

        public bool CountServiceUPD()
        {
            request = "SELECT COUNT(*) FROM service WHERE service_name = @servicename AND id_service != @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@servicename", serviceName));
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            return nb > 0;
        }

        public bool CountServiceDEL()
        {
            request = "SELECT COUNT(*) FROM employee WHERE id_service_fk = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            return nb > 0;
        }

        public bool CountService()
        {
            request = "SELECT COUNT(*) FROM service WHERE service_name = @servicename";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@servicename", serviceName));
            connection.Open();
            int nb = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            return nb > 0;
        }

        public bool UpdateService()
        {
            request = "UPDATE service SET service_name = @servicename WHERE id_service = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@servicename", ServiceName));
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public bool DeleteService()
        {
            request = "DELETE FROM service WHERE id_service = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public static Service GetService(int id)
        {
            Service service = null;
            request = "SELECT id_service, service_name FROM service where id_service = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                service = new Service()
                {
                    Id = id,
                    ServiceName = reader.GetString(1)
                };
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return service;
        }

        public static List<Service> GetServices()
        {
            List<Service> services = new List<Service>();
            request = "SELECT id_service, service_name FROM service";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Service service = new Service()
                {
                    Id = reader.GetInt32(0),
                    ServiceName = reader.GetString(1)
                };

                services.Add(service);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return services;
        }

    }
}
