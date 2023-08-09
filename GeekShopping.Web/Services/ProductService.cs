using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;
using System.Net.Http.Headers;

namespace GeekShopping.Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public string BasePath = "api/v1/Product";

        public ProductService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<ProductModel>> FindAllProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath = $"{BasePath}/all-products";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<IEnumerable<ProductModel>>();
        }
        
        public async Task<ProductModel> FindProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath = $"{BasePath}/find-product/{id}";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<ProductModel>();
        }
        
        public async Task<ProductModel> CreateProduct(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath = $"{BasePath}/create-product";
            var response = await _client.PostAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath = $"{BasePath}/update-product";
            var response = await _client.PutAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
 
        public async Task<bool> DeleteProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            BasePath = $"{BasePath}/delete-product/ {id}";
            var response = await _client.DeleteAsync(BasePath);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
    }
}