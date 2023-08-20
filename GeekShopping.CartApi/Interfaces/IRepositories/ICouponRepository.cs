namespace GeekShopping.CartApi.Interfaces.IRepositories
{
    public interface ICouponRepository
    {
        Task<Coupon> FindCouponByCouponCode(string couponCode, string token);
    }
}