using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;
using System.Security.Claims;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        public CartController(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetCartItems()
        {
            var getid = Convert.ToInt32(HttpContext.User.FindFirstValue("UserId"));
            var Cart = (from cart in _dbContext.carts
                        join cartitem in _dbContext.CartItems on cart.Id equals cartitem.CartId
                        join user in _dbContext.Users on cart.UserId equals user.Id
                        where cart.UserId == getid
                        select new
                        {
                            userID = user.Id,
                            cartitemID = cartitem.Id,
                            productId = cartitem.ProductId,
                            quantity = cartitem.Quantity,
                            productPrice = cartitem.Price,


                        }); ;

            return Ok(Cart);
        }
    }
}
