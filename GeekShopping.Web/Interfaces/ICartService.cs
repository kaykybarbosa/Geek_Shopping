using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface ICartService
    {
        Task<CartViewModel> FindCartByUserId(string userId, string token);
        Task<CartViewModel> AddItemToCart(CartViewModel cart, string token);
        Task<CartViewModel> UpdateItemToCart(CartViewModel cart, string token);
        Task<bool> RemoveFromCart(long userId, string token);

        Task<bool> ApplyCoupon(CartViewModel cart, string token);
        Task<bool> RemoveCoupon(string userId, string token);
        Task<bool> ClearCart(string userId, string token);

        Task<object> Checkout(CartHeaderViewModel cartHeader, string token);
    }
}