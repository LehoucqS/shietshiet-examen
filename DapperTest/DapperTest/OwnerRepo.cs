using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperTest
{
    public class OwnerRepo : IOwnerRepository
    {
        private string _connectionString;
        public OwnerRepo() 
        {
            _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=postgres";
        }
        public void Create(Owner owner)
        {
            throw new NotImplementedException();
        }

        public List<Owner> GetAll()
        {
            const string Query = "SELECT * FROM owners";
            using var connection = new NpgsqlConnection(_connectionString);
            var result = connection.Query<Owner>(Query);
            return result.ToList();
        }

        public Owner? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Owner GetByIdIncludePets(int id)
        {
            throw new NotImplementedException();
        }
    }
}
