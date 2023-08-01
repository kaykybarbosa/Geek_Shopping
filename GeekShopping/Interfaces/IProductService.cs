using GeekShopping.Dtos;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;

namespace GeekShopping.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> FindProductById(long id);
        Task<AllProductsResponse> FindAllProducts();
        Task<ProductResponse> CreateProduct(ProductRequest productDto);
        Task<ProductResponse> UpdateProduct(ProductRequestUpdate productDto);
        Task<BaseResponse> DeleteProduct(long id);
    }
}