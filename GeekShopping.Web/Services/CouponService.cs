using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        private string BasePath = "api/v1/coupon/";
        public CouponService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CouponViewModel> GetCoupon(string code, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += $"find-coupon-code/{code}";
            var response = await _client.GetAsync(BasePath);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await response.ReadContentAs<CouponViewModel>();
            }

            return new CouponViewModel();
        }
    }
}