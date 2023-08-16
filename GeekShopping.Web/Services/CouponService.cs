using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        public Task<CouponViewModel> GetCouponByCouponCode(string couponCode)
        {
            throw new NotImplementedException();
        }
    }
}
