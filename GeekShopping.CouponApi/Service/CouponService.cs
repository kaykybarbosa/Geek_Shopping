using AutoMapper;
using GeekShopping.CouponApi.Dtos.Response;
using GeekShopping.CouponApi.Interfaces;

namespace GeekShopping.CouponApi.Service
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

        public async Task<CouponResponse> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _couponRepository.FindCouponByCouponCode(couponCode);

            return _mapper.Map<CouponResponse>(coupon);
        }
    }
}