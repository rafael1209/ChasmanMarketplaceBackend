using MarketplaceBackend.Models;
using MongoDB.Driver;

namespace MarketplaceBackend.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("marketplace-dev-cluster");
        }

        public virtual IMongoCollection<Product> Products => _database.GetCollection<Product>("products");
    }
}
