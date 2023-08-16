using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICouponService
    {
        Task<CouponViewModel> GetCoupon(string code, string token);
    }
}