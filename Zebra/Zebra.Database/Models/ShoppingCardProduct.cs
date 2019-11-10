using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Zebra.Database.Models
{
    public class ShoppingCardProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProductId { get; set; }
        public int Count { get; set; }
    }
}