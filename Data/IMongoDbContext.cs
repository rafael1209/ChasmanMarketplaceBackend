using MarketplaceBackend.Models;
using MongoDB.Driver;

namespace MarketplaceBackend.Data
{
    public interface IMongoDbContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
