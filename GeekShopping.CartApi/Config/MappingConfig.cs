using AutoMapper;

namespace GeekShopping.CartApi.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductRequest, Product>();
                //config.CreateMap<Product, ProductResponse>();
                //config.CreateMap<ProductRequestUpdate, Product>();
            });
            return mappingConfig;
        }
    }
}