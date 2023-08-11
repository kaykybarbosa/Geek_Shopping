using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Model;

namespace GeekShopping.CartApi.Interfaces
{
    public interface ICartRepository
    {
        Task<Product> FindProductById(long id);
        Task<CartHeader> FindCartHeader(string userId);
        Task<CartDetail> FindCartDetail(long cartDetailId, long cartHeaderId);
        Task CreateProduct(Product product);
        Task CreateCartHeader(CartHeader cartHeader);
        Task CreateCartDetails(CartDetail cartDetail);
        Task UpdateCartDetail(CartDetail cartDetails);


        Task<Cart> FindCartByUserId(string userId);
        Task<Cart> SaveOrDeleteCart(Cart cart);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}