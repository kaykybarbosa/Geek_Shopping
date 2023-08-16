using AutoMapper;
using GeekShopping.CouponApi.Dtos.Request;
using GeekShopping.CouponApi.Dtos.Response;
using GeekShopping.CouponApi.Model;

namespace GeekShopping.CouponApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponRequest, Coupon>();
                config.CreateMap<Coupon, CouponResponse>();
            });

            return mappingConfig;
        }
    }
}