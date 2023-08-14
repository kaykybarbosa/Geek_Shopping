using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public string BasePath = "api/v1/Product/";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += "all-products";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<IEnumerable<ProductViewModel>>();
        }
        
        public async Task<ProductViewModel> FindProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += $"find-product/{id}";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<ProductViewModel>();
        }
        
        public async Task<ProductViewModel> CreateProduct(ProductViewModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += "create-product";
            var response = await _client.PostAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductViewModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += "update-product";
            var response = await _client.PutAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductViewModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
 
        public async Task<bool> DeleteProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath += $"delete-product/ {id}";
            var response = await _client.DeleteAsync(BasePath);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
    }
}