using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        private string BasePath = "api/v1/cart/";

        public CartService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += $"find-cart/{userId}";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<CartViewModel>();
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += "add-cart";
            var response = await _client.PostAsJson(BasePath, cart);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartViewModel>();
            }

            throw new Exception("Something went wrong when calling API.");
        }

        public async Task<CartViewModel> UpdateItemToCart(CartViewModel cart, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += "update-cart";
            var response = await _client.PutAsJson(BasePath, cart);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartViewModel>();
            }

            throw new Exception("Something went wrong when calling API.");
        }
        
        public async Task<bool> RemoveFromCart(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += $"remove-cart/{userId}";
            var response = await _client.DeleteAsync($"{BasePath}/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }

            throw new Exception("Something went wrong when calling API.");
        }
        
        //
        public Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }
        
        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }
    }
}