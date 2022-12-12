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
            Products.Remove(Products.SingleOrDefault(x => x.Id == id));
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Products.Add(product);
            return CreatedAtAction(nameof(Get), new {id = product.Id}, product);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
