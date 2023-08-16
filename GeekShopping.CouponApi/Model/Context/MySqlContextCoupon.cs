using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Model.Context
{
    public class MySqlContextCoupon : DbContext
    {
        public MySqlContextCoupon() { }
        public MySqlContextCoupon(DbContextOptions<MySqlContextCoupon> options) : base(options) { }
        
        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData( new Coupon
            {
                Id = 1,
                CouponCode = "KBULOSO_23_20",
                DiscountAmount = 20
            });

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "KBULOSO_23_30",
                DiscountAmount = 30
            });
        }
    }
}