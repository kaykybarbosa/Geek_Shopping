using AutoMapper;
using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response;
using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductRequest, Product>();
                config.CreateMap<Product, ProductResponse>();

                config.CreateMap<CartDetailRequest, CartDetail>();
                config.CreateMap<CartDetail, CartDetailResponse>();

                config.CreateMap<CartHeaderRequest, CartHeader>();
                config.CreateMap<CartHeader, CartHeaderResponse>();

                config.CreateMap<CartRequest, Cart>();
                config.CreateMap<Cart, CartResponse>();
            });

            return mappingConfig;
        }
    }
}