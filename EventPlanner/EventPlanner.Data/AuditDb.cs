using Microsoft.EntityFrameworkCore;
using EventPlanner.Data.Entities;
using MongoDB.EntityFrameworkCore.Extensions;
using MongoDB.Driver;

namespace EventPlanner.Data
{
    public class AuditDb
    {
        public AuditDb() 
        {
            MongoClient client = new("mongodb://localhost:21017");
            const string DatabaseName = "eventaudit";
            var dbExists = client.ListDatabaseNames().ToList().Contains(DatabaseName);
            IMongoDatabase mongoDatabase = client.GetDatabase(DatabaseName);
            var database = mongoDatabase;
        }

    }
}
