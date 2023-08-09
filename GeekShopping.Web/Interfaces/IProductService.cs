﻿using GeekShopping.Web.Models;

namespace GeekShopping.Web.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> FindAllProducts(string token);
        Task<ProductModel> FindProductById(long id, string token);
        Task<ProductModel> CreateProduct(ProductModel product, string token);
        Task<ProductModel> UpdateProduct(ProductModel product, string token);
        Task<bool> DeleteProductById(long id, string token);
    }
}