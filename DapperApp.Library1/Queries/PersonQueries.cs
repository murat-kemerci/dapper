using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperApp.Library1.Models;

namespace DapperApp.Library1.Queries
{
    public class PersonQueries : ConnectionFactory
    {
        public List<Person> GetPersons()
        {
            List<Person> persons = null;

            RunCommand((connection) =>
            {
                persons = connection.Query<Person>("select * from Persons").ToList();
            });

            return persons;
        }
        public Person FindPerson(int Id)
        {
            Person person = null;

            RunCommand((connection) =>
            {
                person = connection.QuerySingle<Person>($"select * from Persons where Persons.Id = {Id} ");
            });

            return person;
        }
        public void CreatePerson(Person person)
        {
            RunCommand((connection) =>
            {
                var insertedId = connection.ExecuteScalar<int>(@"insert into Persons values (@name,@surname) select SCOPE_IDENTITY()",
                    new
                    {
                        name = person.Name,
                        surname = person.Surname
                    });

                person.Id = insertedId;
            });
        }
        public void UpdatePerson(int id, string name, string surname)
        {
            RunCommand((connection) =>
            {
                connection.Query<Person>($"Update Persons Set Name = '{name}', Surname='{surname}' where Id = {id}");

            });
        }
        public void DeletePerson(int id)
        {

            RunCommand((connection) =>
            {
                connection.Query<Person>($"DELETE FROM Persons WHERE Id = {id}");

            });
        }
        public List<Person> FilterPersons(string table, string key)
        {
            List<Person> persons = null;
            RunCommand((connetion) =>
            {
                persons = connetion.Query<Person>($"Select * From Persons Where {table} Like'%{key}%'").ToList();
            });
            return persons;
        }
    }
}
