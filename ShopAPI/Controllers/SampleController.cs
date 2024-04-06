using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        public SampleController(OnlineShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Fuck()
        {
            return Ok("Everything is fuckd");
        }
    }
}
