using GeekShopping.CartApi.Dtos.Response.Coupon;

namespace GeekShopping.CartApi.Interfaces.IServices
{
    public interface ICouponService
    {
        Task<CouponResponse> GetCouponByCouponCode(string couponCode, string token);
    }
}