using MarketplaceBackend.Data;
using MarketplaceBackend.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MarketplaceBackend.Repositories
{
    public class ProductRepository(MongoDbContext context) : IProductRepository
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await context.Products.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, new ObjectId(id));
            return await context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Product product)
        {
            await context.Products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, new ObjectId(id));
            await context.Products.ReplaceOneAsync(filter, product);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, new ObjectId(id));
            await context.Products.DeleteOneAsync(filter);
        }
    }
}
