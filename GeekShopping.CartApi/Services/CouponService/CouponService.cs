using GeekShopping.CartApi.Dtos.Response.Coupon;
using GeekShopping.CartApi.Interfaces.IRepositories;
using GeekShopping.CartApi.Interfaces.IServices;

namespace GeekShopping.CartApi.Services.CouponService
{
    public class CouponService : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private IMapper _mapper;

        public CouponService(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        public async Task<CouponResponse> GetCouponByCouponCode(string couponCode, string token)
        {
            var coupon = await _couponRepository.FindCouponByCouponCode(couponCode, token);

            return _mapper.Map<CouponResponse>(coupon);
        }
    }
}