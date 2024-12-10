using MarketplaceBackend.Models;
using MongoDB.Driver;

namespace MarketplaceBackend.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private const string ConstProductsCollection = "products";

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDb:Name"));
        }

        public virtual IMongoCollection<Product> Products => _database.GetCollection<Product>(ConstProductsCollection);
    }
}