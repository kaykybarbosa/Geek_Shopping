using Microsoft.EntityFrameworkCore;

namespace GeekShopping.OrderApi.Model.Context
{
    public class MySqlContextOrder : DbContext
    {
        public MySqlContextOrder() {}
        public MySqlContextOrder(DbContextOptions<MySqlContextOrder> options) : base(options) {}
        public DbSet<OrderDetail> Details { get; set; }   
        public DbSet<OrderHeader> Headers { get; set; }   
    }
}