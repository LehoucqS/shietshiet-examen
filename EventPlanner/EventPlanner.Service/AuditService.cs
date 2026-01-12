using EventPlanner.Data;
using EventPlanner.Service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventPlanner.Service
{
    public class AuditService
    {
        private readonly IMongoCollection<AuditTrail> _audits;

        public AuditService(IOptions<MongoDbSettings> settings)
        {
            MongoClient client = new(settings.Value.ConnectionString);
            IMongoDatabase database = client.GetDatabase(settings.Value.DatabaseName);
            _audits = database.GetCollection<AuditTrail>(settings.Value.CollectionName);
        }

        public async Task<List<AuditTrail>> GetAll()
        {
            List<AuditTrail> result =  await _audits.Find(_ => true).ToListAsync();
            return result;
        }

        public async Task AddAudit(string operation, string type, List<string> data)
        {
            AuditTrail toAdd = new AuditTrail
            {
                Operation = operation,
                Type = type,
                Data = data,
                Time = DateTime.Now
            };
            await _audits.InsertOneAsync(toAdd);
        }
    }
}
