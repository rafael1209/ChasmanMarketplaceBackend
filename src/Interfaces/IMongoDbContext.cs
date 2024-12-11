using MarketplaceBackend.Models;
using MongoDB.Driver;

namespace MarketplaceBackend.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<User> Users { get; }
    }
}
