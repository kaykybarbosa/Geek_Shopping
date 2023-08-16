using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCouponByCouponCode(string couponCode);
    }
}