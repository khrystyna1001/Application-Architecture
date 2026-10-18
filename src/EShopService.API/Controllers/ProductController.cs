using Microsoft.AspNetCore.Mvc;
using EShopService.Domain.Entities;

namespace EShopService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        // Static mock data utilizing your specific Ean, Sku, Stock, and Name properties
        private static readonly List<Product> _products = new List<Product>
        {
            new Product 
            { 
                Id = 1, 
                Name = "Gaming Laptop", 
                Price = 1299.99M, 
                Ean = "1234567890123", 
                Sku = "LAP-GAME-01", 
                Stock = 15,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid()
            },
            new Product 
            { 
                Id = 2, 
                Name = "Wireless Mouse", 
                Price = 49.99M, 
                Ean = "9876543210987", 
                Sku = "MSE-WRLS-02", 
                Stock = 120,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = Guid.NewGuid()
            }
        };

        // GET: api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            var activeProducts = _products.Where(p => !p.Deleted).ToList();
            return Ok(activeProducts);
        }

        // GET: api/product/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            return Ok(product);
        }

        // POST: api/product
        [HttpPost]
        public ActionResult<Product> Create([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest(new { Message = "Product data cannot be empty." });
            }

            newProduct.Id = _products.Count > 0 ? _products.Max(p => p.Id) + 1 : 1;
            newProduct.CreatedAt = DateTime.UtcNow;
            newProduct.CreatedBy = Guid.NewGuid();
            newProduct.Deleted = false;
            
            _products.Add(newProduct);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/product/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest(new { Message = "Updated data cannot be empty." });
            }

            var existingProduct = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Ean = updatedProduct.Ean;
            existingProduct.Sku = updatedProduct.Sku;
            existingProduct.Stock = updatedProduct.Stock;
            existingProduct.CategoryId = updatedProduct.CategoryId;

            existingProduct.UpdatedAt = DateTime.UtcNow;
            existingProduct.UpdatedBy = Guid.NewGuid();

            return NoContent();
        }

        // DELETE: api/product/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id && !p.Deleted);
            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            product.Deleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            
            return NoContent();
        }
    }
}
