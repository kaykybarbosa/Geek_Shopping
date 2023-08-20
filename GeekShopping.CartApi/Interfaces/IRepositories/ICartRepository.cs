using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Interfaces.IRepositories
{
    public interface ICartRepository
    {
        Task<Product> FindProductById(long id);

        Task<CartHeader> FindCartHeader(string userId);
        Task<CartHeader> FindCartHeader(long cartHeaderId);
        Task<CartHeader> FindCartHeaderNoTracking(string userId);

        Task<CartDetail> FindCartDetail(long cartDetailId);
        Task<IEnumerable<CartDetail>> FindCartDetails(long cartHeaderId);
        Task<CartDetail> FindCartDetailNoTracking(long cartDetailProductId, long cartHeaderId);

        Task CreateProduct(Product product);
        Task CreateCartHeader(CartHeader cartHeader);
        Task CreateCartDetails(CartDetail cartDetail);

        Task UpdateHeader(CartHeader cartHeader);
        Task UpdateCartDetail(CartDetail cartDetails);

        Task RemoveCartDetail(CartDetail cartDetail);
        Task RemoveCartHeader(CartHeader cartHeader);
        Task RemoveCartDetailRange(long cartHeaderId);
    }
}