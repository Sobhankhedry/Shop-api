using Microsoft.AspNetCore.Mvc;
using ShopAPI.NewFolder;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        public UsersController(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //[HttpPost]
        //public IActionResult Register([FromBody] User user)
        //{

        //}


    }
}
