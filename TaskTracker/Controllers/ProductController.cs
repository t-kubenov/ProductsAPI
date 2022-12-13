using Microsoft.AspNetCore.Mvc;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("/api/[controller]")]
    public class ProductController : Controller
    {
        private static List<Product> Products = new List<Product>(new[] {
            new Product() { Id = 1, Name = "Soap", Price = 2000 },
            new Product() { Id = 2, Name = "Toothpaste", Price = 3000 },
            new Product() { Id = 3, Name = "Towel", Price = 10000 }
        });

        [HttpGet]
        public IEnumerable<Product> Get() => Products;

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = Products.SingleOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);    
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Products.Remove(Products.FirstOrDefault(x => x.Id == id));
            return Ok();
        }
        private int NextProductId => Products.Count() == 0 ? 1 : Products.Max(x => x.Id) + 1;

        [HttpGet("GetNextProductId")]
        public int GetNextProductId() => NextProductId;

        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = NextProductId;
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        [HttpPost("AddProduct")]
        public IActionResult PostBody([FromBody] Product product) => Post(product);

        [HttpPut]
        public IActionResult Put(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var storedProduct = Products.FirstOrDefault(x => x.Id == product.Id);
            if (storedProduct == null) return NotFound();

            storedProduct.Name = product.Name;
            storedProduct.Price = product.Price;
            return Ok(storedProduct);
        }

        [HttpPut("UpdateProduct")]
        public IActionResult PutBody([FromBody] Product product) => Put(product);

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
