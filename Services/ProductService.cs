using MarketplaceBackend.Models;
using MarketplaceBackend.Repositories;

namespace MarketplaceBackend.Services
{
    public class ProductService(IProductRepository productRepository)
    {
        public async Task<List<Product>> GetAllAsync()
        {
            return await productRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(string id)
        {
            return await productRepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(Product product)
        {
            await productRepository.CreateAsync(product);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            await productRepository.UpdateAsync(id, product);
        }

        public async Task DeleteAsync(string id)
        {
            await productRepository.DeleteAsync(id);
        }
    }
}
