using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Homework48.Data
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class Manager
    {
        private string _ConnectionString { get; set; }
        public Manager(string connectionString)
        {
            _ConnectionString = connectionString;
        }
        public void AddPeople(Person person)
        {
            using var connection = new SqlConnection(_ConnectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES (@FirstName, @LastName, @Age)";
            cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
            cmd.Parameters.AddWithValue("@LastName", person.LastName);
            cmd.Parameters.AddWithValue("@Age", person.Age);
            connection.Open();
            cmd.ExecuteNonQuery();

        }
        public List <Person> GetPeople()
        {
            using var connection = new SqlConnection(_ConnectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var people = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }
            return people;
        }
    }
    
}
