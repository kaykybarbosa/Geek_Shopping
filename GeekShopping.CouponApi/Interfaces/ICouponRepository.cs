using GeekShopping.CouponApi.Model;

namespace GeekShopping.CouponApi.Interfaces
{
    public interface ICouponRepository
    {
        Task<Coupon> FindCouponByCouponCode(string couponCode);
    }
}