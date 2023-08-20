using GeekShopping.CartApi.Interfaces.IRepositories;

namespace GeekShopping.CartApi.Repository.CouponRepository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySqlContextCoupon _context;

        public CouponRepository(MySqlContextCoupon context)
        {
            _context = context;
        }

        public async Task<Coupon> FindCouponByCouponCode(string couponCode, string token)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        }
    }
}
