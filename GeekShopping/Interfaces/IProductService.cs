using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;

namespace GeekShopping.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> FindProductById(long id);
        Task<IEnumerable<ProductResponse>> FindAllProducts();
        Task<ProductResponse> CreateProduct(ProductRequest productDto);
        Task<ProductResponse> UpdateProduct(ProductRequestUpdate productDto);
        Task<bool> DeleteProduct(long id);
    }
}