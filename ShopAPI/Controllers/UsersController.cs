using AuthenticationPlugin;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Model;
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

        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var userWithSameEmail = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already exist");
            }
            if ((user.Name != "") || (user.Password != "") || (user.Email != ""))
            {
                var userobj = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = SecurePasswordHasherHelper.Hash(user.Password),
                    Role = "User"
                };
                _dbContext.Users.Add(userobj);
                _dbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, "created");
            }
            else
            {
                return BadRequest("All fields required");
            }
        }


    }
}
