using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo.NetCoreRefactoring.Models;

namespace Demo.NetCoreRefactoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public ProductsController()
        {
        }

        // GET: api/Products
        [HttpGet]
        public IEnumerable<Product> GetProduct()
        {
            using (var context = new NorthwindslimContext())
            {
                return context.Product.ToListAsync().Result;
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            using (var context = new NorthwindslimContext())
            {
                var product = context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            using (var context = new NorthwindslimContext())
            {
                context.Product.Add(product);
                await Task.Run(() => context.SaveChangesAsync());
                return CreatedAtAction("GetProduct", new { id = product.ProductId }, product); 
            }
        }
    }
}
