using MarketplaceBackend.Models;
using MongoDB.Driver;

namespace MarketplaceBackend.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        private readonly IMongoDatabase _database;
        private const string ConstProductsCollection = "products";
        private const string ConstUsersCollection = "users";

        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("MongoDb:ConnectionString"));
            _database = client.GetDatabase(configuration.GetValue<string>("MongoDb:Name"));
        }

        public IMongoCollection<Product> Products => _database.GetCollection<Product>(ConstProductsCollection);

        public IMongoCollection<User> Users => _database.GetCollection<User>(ConstUsersCollection);
    }
}