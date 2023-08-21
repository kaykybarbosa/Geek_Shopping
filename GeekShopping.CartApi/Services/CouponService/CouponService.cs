using GeekShopping.CartApi.Dtos.Response.Coupon;
using GeekShopping.CartApi.Interfaces.IServices;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.CartApi.Services.CouponService
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;

        public CouponService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponResponse> GetCouponByCouponCode(string couponCode, string token)
        {
            var BasePath = $"api/v1/coupon/find-coupon-code/{couponCode}";

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            var response = await _client.GetAsync(BasePath);
            var context = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) return new CouponResponse();

            return JsonSerializer.Deserialize<CouponResponse>(context, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
    }
}