using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MarketplaceBackend.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
