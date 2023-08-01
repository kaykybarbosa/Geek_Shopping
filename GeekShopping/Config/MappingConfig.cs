using AutoMapper;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
using GeekShopping.Model;

namespace GeekShopping.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductRequest, Product>();
                config.CreateMap<Product, ProductResponse>();
                config.CreateMap<ProductRequestUpdate, Product>();
                //config.CreateMap<IEnumerable<Product>, AllProductsResponse>();
            });
            return mappingConfig;
        }
    }
}