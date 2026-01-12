using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventPlanner.Service.Models
{
    public class AuditTrail
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("operation")]
        public string? Operation { get; set; }

        [BsonElement("type")]
        public string? Type { get; set; }

        [BsonElement("data")]
        public List<string>? Data { get; set; }

        [BsonElement("time")]
        public DateTime Time { get; set; }
    }
}
