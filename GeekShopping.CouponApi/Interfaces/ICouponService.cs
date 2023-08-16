using GeekShopping.CouponApi.Dtos.Response;

namespace GeekShopping.CouponApi.Interfaces
{
    public interface ICouponService
    {
        Task<CouponResponse> GetCouponByCouponCode(string couponCode);
    }
}