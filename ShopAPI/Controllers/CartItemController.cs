using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;
using ShopAPI.Model;

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
            var productId = _dbContext.Products.Find(cartItem.ProductId);
            var price = productId.price * cartItem.Quantity;
            cartItem.Price = price;
            _dbContext.CartItems.Add(cartItem);
            _dbContext.SaveChanges();
            return Created();
        }
    }
}
