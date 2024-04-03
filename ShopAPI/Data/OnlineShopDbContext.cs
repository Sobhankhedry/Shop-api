using Microsoft.EntityFrameworkCore;
using ShopAPI.Model;

namespace ShopAPI.NewFolder
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options)
        {

        }
        public DbSet<Cart> carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
