using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Pigeon.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Description { get; set; }

        public string Category { get; set; }

        public string Author { get; set; }
    }
}