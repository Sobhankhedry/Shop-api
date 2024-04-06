using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;
using ShopAPI.Model;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        public ProductsController(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult RegisterProduct([FromBody] Product product)
        {
            var productWithSameName = _dbContext.Products.Where(u => u.Name == product.Name).SingleOrDefault();
            if (productWithSameName != null)
            {
                return BadRequest("this product already exist");
            }
            else
            {
                var prodobj = new Product
                {
                    Name = product.Name,
                    price = product.price
                };
                _dbContext.Products.Add(prodobj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult AllProducts()
        {
            var products = from product in _dbContext.Products
                           select new
                           {
                               Id = product.Id,
                               Name = product.Name,
                               Price = product.price
                           };
            return Ok(products);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var del = _dbContext.Products.Find(id);
            if (del == null)
            {
                return NotFound("not fond");
            }
            else
            {
                _dbContext.Products.Remove(del);
                _dbContext.SaveChanges();
                return Ok("record has been deleted");
            }
        }


    }
}
