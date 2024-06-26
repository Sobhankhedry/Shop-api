﻿using AuthenticationPlugin;
using Microsoft.AspNetCore.Mvc;
using ShopAPI.Data;
using ShopAPI.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShopAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private OnlineShopDbContext _dbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public UsersController(OnlineShopDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);

        }


        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            var userWithSameEmail = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                return BadRequest("User with same email already exist");
            }
            if ((user.Name?.Trim() != "") && (user.Email?.Trim() != "") && (user.Password?.Trim() != ""))
            {
                var userobj = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = SecurePasswordHasherHelper.Hash(user.Password),
                    Role = "Users"
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


        [HttpPost]
        public IActionResult Login([FromBody] User user)
        {
            var userEmail = _dbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail == null)
            {
                return NotFound("not found");
            }
            if (!SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user?.Email),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, userEmail.Role),
                new Claim("UserId",userEmail.Id.ToString())
            };

            HasCart(userEmail.Id);

            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                //your_role = getid
            });


        }

        private void HasCart(int id)
        {
            var founded = _dbContext.carts.FirstOrDefault(x => x.Id == id);
            if (founded != null)
            {
                return;
            }
            else
            {
                var cartObj = new Cart
                {
                    UserId = id,
                    Status = "Nothing"
                };

                _dbContext.carts.Add(cartObj);
                _dbContext.SaveChanges();
            }



        }
    }
}
