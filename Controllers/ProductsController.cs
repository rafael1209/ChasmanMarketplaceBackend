using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll()
        {
            var products = await productService.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var product = await productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            await productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Product product)
        {
            await productService.UpdateAsync(id, product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
