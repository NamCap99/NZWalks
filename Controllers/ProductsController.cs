using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.Controllers
{
    public class ProductsController : ControllerBase
    {
        // Get
        [HttpGet]
        public IActionResult Get()
        {
            // Get all products
            var products = new List<string>
            {
                "Product 1",
                "Product 2",
                "Product 3"
            };
            return Ok(products);
        }
        // Get by id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Get product by id
            var products = new List<string>
            {
                "Product 1",
                "Product 2",
                "Product 3"
            };
        }
        // Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            // Validate product
            if (product == null || string.IsNullOrEmpty(product.Name))
            {
                return BadRequest("Invalid product data.");
            }
            // Simulate adding product to database
            var products = new List<string>
            {
                "Product 1",
                "Product 2",
                "Product 3"
            };
            products.Add(product.Name);
            return CreatedAtAction(nameof(GetById), new { id = products.Count }, product);

        }
        // Update
        // [HttpPut("{id}")]
        // public IActionResult Update(int id, ProductsController)

    }
}