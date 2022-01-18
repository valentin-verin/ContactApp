using System;
using System.Collections.Generic;
using ContactApp.Tools;
using MySql.Data.MySqlClient;

namespace ContactApp.Models
{
    public class Employee
    {

        private int id;
        private string firstName;
        private string lastname;
        private string phoneNumber;
        private int idSite;
        private string site;
        private int idService;
        private string service;
        private string mail;
        private string cellphoneNumber;

        private static string request;
        private static MySqlCommand command;
        private static MySqlConnection connection;
        private static MySqlDataReader reader;

        public int Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public int IdSite { get => idSite; set => idSite = value; }
        public string Site { get => site; set => site = value; }
        public int IdService { get => idService; set => idService = value; }
        public string Service { get => service; set => service = value; }
        public string Mail { get => mail; set => mail = value; }
        public string CellphoneNumber { get => cellphoneNumber; set => cellphoneNumber = value; }

        public Employee()
        {
        }


        public bool AddEmployee()
        {
            request =   "INSERT INTO " +
                            "employee (firstname, lastname, phone_number, id_Site_FK, Id_service_FK, mail, cellphone_number) " +
                            "values (@firstname, @lastname, @phone_number, @id_Site_FK, @Id_service_FK, @mail, @cellphone_number)";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@firstname", FirstName));
            command.Parameters.Add(new MySqlParameter("@lastname", Lastname));
            command.Parameters.Add(new MySqlParameter("@phone_number", PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@id_Site_FK", IdSite));
            command.Parameters.Add(new MySqlParameter("@Id_service_FK", IdService));
            command.Parameters.Add(new MySqlParameter("@mail", Mail));
            command.Parameters.Add(new MySqlParameter("@cellphone_number", CellphoneNumber));
            connection.Open();
            Id = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            connection.Close();
            return CountEmployee();
        }

        public bool UpdateEmployee()
        {
            request =   "UPDATE employee " +
                        "SET SET " +
                            "phone_number = @phone_number, " +
                            "id_Site_FK = @id_Site_FK, " +
                            "Id_Service_FK = @Id_Service_FK, " +
                            "mail = @mail, " +
                            "cellphone_number = @cellphone_number, " +
                        "WHERE id-employee = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@phone_number", PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@id_Site_FK", IdSite));
            command.Parameters.Add(new MySqlParameter("@Id_Service_FK", IdService));
            command.Parameters.Add(new MySqlParameter("@mail", Mail));
            command.Parameters.Add(new MySqlParameter("@cellphone_number", CellphoneNumber));
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public bool DeleteEmployee()
        {
            request = "DELETE FROM employee WHERE Id_Employee = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", Id));
            connection.Open();
            int nb = command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
            return nb == 1;
        }

        public bool CountEmployee()
        {
            request =   "SELECT COUNT(*) FROM `employee` " +
                        "WHERE (firstname = @firstname " +
                        "AND lastname = @lastname) " +
                        "OR phone_number = @phone_number " +
                        "OR mail = @mail " +
                        "OR cellphone_number = @cellphone_number ";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@firstname", FirstName));
            command.Parameters.Add(new MySqlParameter("@lastname", Lastname));
            command.Parameters.Add(new MySqlParameter("@phone_number", PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@mail", Mail));
            command.Parameters.Add(new MySqlParameter("@cellphone_number", CellphoneNumber));
            connection.Open();
            int nb = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            return nb > 0;
        }

        public bool CountEmployeeUPD()
        {
            request = "SELECT COUNT(*) FROM `employee` " +
                        "WHERE firstname = @firstname " +
                        "OR lastname = @lastname " +
                        "OR phone_number = @phone_number " +
                        "OR mail = @mail " +
                        "OR cellphone_number = @cellphone_number " +
                        "AND id_employee != @id_employee ";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@firstname", FirstName));
            command.Parameters.Add(new MySqlParameter("@lastname", Lastname));
            command.Parameters.Add(new MySqlParameter("@phone_number", PhoneNumber));
            command.Parameters.Add(new MySqlParameter("@mail", Mail));
            command.Parameters.Add(new MySqlParameter("@cellphone_number", CellphoneNumber));
            command.Parameters.Add(new MySqlParameter("@id_employee", Id));
            connection.Open();
            int nb = Convert.ToInt32(command.ExecuteScalar());
            command.Dispose();
            return nb > 0;
        }

        public static Employee GetEmployee(int id)
        {
            Employee employee = null;
            request = "SELECT firstname, lastname, phone_number, id_Site_FK, s.site_city, Id_service_FK, S2.service_name, mail, cellphone_number " +
                        "FROM employee e " +
                        "INNER JOIN site s on e.id_Site_FK = s.Id_site " +
                        "INNER JOIN service s2 on e.Id_Service_FK = s2.Id_Service " +
                        "WHERE Id_Employee = @id";
            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            connection.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                employee = new Employee()
                {
                    Id = id,
                    FirstName = reader.GetString(0),
                    Lastname = reader.GetString(1),
                    PhoneNumber = reader.GetString(2),
                    IdSite = reader.GetInt32(3),
                    Site = reader.GetString(4),
                    IdService = reader.GetInt32(5),
                    Service = reader.GetString(6),
                    Mail = reader.GetString(7),
                    CellphoneNumber = reader.GetString(8)
                };
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return employee;
        }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            request =   "SELECT Id_Employee, firstname, lastname, phone_number, s.site_city, S2.service_name, mail, cellphone_number " +
                        "FROM employee e " +
                        "INNER JOIN site s on e.id_Site_FK = s.Id_site " +
                        "INNER JOIN service s2 on e.Id_Service_FK = s2.Id_Service";

            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    Site = reader.GetString(4),
                    Service = reader.GetString(5),
                    Mail = reader.GetString(6),
                    CellphoneNumber = reader.GetString(7)
                };

                employees.Add(employee);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return employees;
        }

        public static List<Employee> GetEmployeesSearch(string name)
        {
            List<Employee> employees = new List<Employee>();
            request = "SELECT Id_Employee, firstname, lastname, phone_number, s.site_city, S2.service_name, mail, cellphone_number " +
                        "FROM employee e " +
                        "INNER JOIN site s on e.id_Site_FK = s.Id_site " +
                        "INNER JOIN service s2 on e.Id_Service_FK = s2.Id_Service " +
                        "WHERE firstname LIKE @name " +
                        "OR lastname LIKE @name ";

            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@name", name + "%"));
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    Site = reader.GetString(4),
                    Service = reader.GetString(5),
                    Mail = reader.GetString(6),
                    CellphoneNumber = reader.GetString(7)
                };

                employees.Add(employee);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return employees;
        }


        public static List<Employee> GetEmployeesBySite(int id)
        {
            List<Employee> employees = new List<Employee>();
            request = "SELECT Id_Employee, firstname, lastname, phone_number, s.site_city, S2.service_name, mail, cellphone_number " +
                        "FROM employee e " +
                        "INNER JOIN site s on e.id_Site_FK = s.Id_site " +
                        "INNER JOIN service s2 on e.Id_Service_FK = s2.Id_Service " +
                        "WHERE id_site_fk = @id";

            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    Site = reader.GetString(4),
                    Service = reader.GetString(5),
                    Mail = reader.GetString(6),
                    CellphoneNumber = reader.GetString(7)
                };

                employees.Add(employee);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return employees;
        }


        public static List<Employee> GetEmployeesByService(int id)
        {
            List<Employee> employees = new List<Employee>();
            request = "SELECT Id_Employee, firstname, lastname, phone_number, s.site_city, S2.service_name, mail, cellphone_number " +
                        "FROM employee e " +
                        "INNER JOIN site s on e.id_Site_FK = s.Id_site " +
                        "INNER JOIN service s2 on e.Id_Service_FK = s2.Id_Service " +
                        "WHERE id_service_fk = @id";

            connection = Db.Connection;
            command = new MySqlCommand(request, connection);
            command.Parameters.Add(new MySqlParameter("@id", id));
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee()
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    Lastname = reader.GetString(2),
                    PhoneNumber = reader.GetString(3),
                    Site = reader.GetString(4),
                    Service = reader.GetString(5),
                    Mail = reader.GetString(6),
                    CellphoneNumber = reader.GetString(7)
                };

                employees.Add(employee);
            }
            reader.Close();
            command.Dispose();
            connection.Close();
            return employees;
        }


    }
}
