using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;
using ShopAPI.Model;
using System.Security.Claims;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        public CartItemController(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddingToCartItem([FromBody] CartItem cartItem)
        {
            var getid = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            var foundedUser = _dbContext.carts.FirstOrDefault(x => x.UserId == getid);
            var product = _dbContext.Products.Find(cartItem.ProductId);
            var price = cartItem.Quantity * product.price;
            cartItem.Price = price;
            cartItem.CartId = foundedUser.Id;
            _dbContext.CartItems.Add(cartItem);
            _dbContext.SaveChanges();
            return Ok("Added");
        }


        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var cartItem = _dbContext.CartItems.Find(id);
            if (cartItem == null)
            {
                return NotFound();
            }
            _dbContext.Remove(cartItem);
            _dbContext.SaveChanges();
            return Ok();
        }



    }
}
