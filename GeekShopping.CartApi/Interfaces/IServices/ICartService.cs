using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response;

namespace GeekShopping.CartApi.Interfaces.IServices
{
    public interface ICartService
    {
        Task<CartResponse> FindCartByUserId(string userId);
        Task<CartResponse> SaveOrUpdateCart(CartRequest cart);
        Task<bool> RemoveFromCart(long cartDetailsId);
        Task<bool> ApplyCoupon(string userId, string couponCode);
        Task<bool> RemoveCoupon(string userId);
        Task<bool> ClearCart(string userId);
    }
}