using GeekShopping.Dtos;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;

namespace GeekShopping.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> FindById(long id);
        Task<AllProductsResponse> FindAll();
        Task<ProductResponse> Create(ProductRequest productDto);
        Task<ProductResponse> Update(ProductRequestUpdate productDto);
        Task<BaseResponse> Delete(long id);
    }
}