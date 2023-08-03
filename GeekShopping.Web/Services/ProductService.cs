using GeekShopping.Web.Interfaces;
using GeekShopping.Web.Models;
using GeekShopping.Web.Utils;

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

        public async Task<IEnumerable<ProductModel>> FindAllProducts()
        {
            BasePath = $"{BasePath}/all-products";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<IEnumerable<ProductModel>>();
        }
        
        public async Task<ProductModel> FindProductById(long id)
        {
            BasePath = $"{BasePath}/find-product/{id}";
            var response = await _client.GetAsync(BasePath);

            return await response.ReadContentAs<ProductModel>();
        }
        
        public async Task<ProductModel> CreateProduct(ProductModel product)
        {
            BasePath = $"{BasePath}/create-product";
            var response = await _client.PostAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }

        public async Task<ProductModel> UpdateProduct(ProductModel product)
        {
            BasePath = $"{BasePath}/update-product";
            var response = await _client.PutAsJson(BasePath, product);

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<ProductModel>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
 
        public async Task<bool> DeleteProductById(long id)
        {
            BasePath = $"{BasePath}/delete-product";
            var response = await _client.DeleteAsync($"{BasePath}/ {id}");

            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<bool>();

            else throw new Exception($"Something went wrong when calling API: {response.ReasonPhrase}");
        }
    }
}