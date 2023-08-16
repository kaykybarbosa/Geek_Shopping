using GeekShopping.CouponApi.Interfaces;
using GeekShopping.CouponApi.Model;
using GeekShopping.CouponApi.Model.Context;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.CouponApi.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MySqlContextCoupon _context;

        public CouponRepository(MySqlContextCoupon context)
        {
            _context = context;
        }

        public async Task<Coupon> FindCouponByCouponCode(string couponCode)
        {
            return await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
        }
    }
}