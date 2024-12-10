using MarketplaceBackend.Models;
using MarketplaceBackend.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Moq.AutoMock;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace MarketplaceBackend.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        private AutoMocker Mocker { get; set; }

        private ProductRepository Repository { get; set; }

        public ProductRepositoryTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            Mocker = new AutoMocker();
            Mocker.Use<IConfiguration>(configuration);
            Repository = Mocker.CreateInstance<ProductRepository>();

        }

        [Fact]
        public async Task GetProductsTest()
        {
            try
            {
                var result = await Repository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected on exception, but got: " + ex);
            }
        }

        [Fact]
        public async Task CreateProductsTest()
        {
            try
            {
                var product = new Product
                {
                    Id = ObjectId.GenerateNewId(),
                    Name = "Test Product",
                    Price = 100
                };

                await Repository.CreateAsync(product);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected on exception, but got: " + ex);
            }
        }
    }
}
