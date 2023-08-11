using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CartApi.Model.Context
{
    public class MySqlContextCart : DbContext
    {
        public MySqlContextCart() { }
        public MySqlContextCart(DbContextOptions<MySqlContextCart> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
        public DbSet<CartHeader> CartHeaders { get; set; }
    }
}