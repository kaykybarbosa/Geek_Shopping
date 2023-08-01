using AutoMapper;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Response;
using GeekShopping.Dtos;
using GeekShopping.Interfaces;
using GeekShopping.Model;

namespace GeekShopping.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> FindById(long id)
        {
            var product = await _productRepository.FindById(id);

            if (product == null)
                return new ProductResponse()
                {
                    Message = "Product with this Id not found!",
                    IsSuccess = false,
                    StatusCode = 204
                };

            ProductResponse response = new();
            response = _mapper.Map<ProductResponse>(product);
            response.Message = "Found successfully.";
            response.IsSuccess = true;
            response.StatusCode = 302;

            return response;
        }

        public async Task<AllProductsResponse> FindAll()
        {
            IEnumerable<Product> products = await _productRepository.FindAll();
            IEnumerable<ProductResponse> productsMapper = _mapper.Map<IEnumerable<ProductResponse>>(products);

            return new AllProductsResponse()
            {
                Products = productsMapper,
                Message = "Products found successfully!",
                IsSuccess = true,
                StatusCode = 302
            };
        }

        public async Task<ProductResponse> Create(ProductRequest request)
        {
            try
            {
                Product product = _mapper.Map<Product>(request);
                await _productRepository.Create(product);

                ProductResponse response = _mapper.Map<ProductResponse>(product);
                response.Message = "Product created successfully!";
                response.IsSuccess = true;
                response.StatusCode = 201;

                return response;

            }
            catch (Exception)
            {
                return new ProductResponse()
                {
                    Message = "Error while try save product.",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }
        }
        public async Task<ProductResponse> Update(ProductRequestUpdate requestUpdate)
        {
            try
            {
                Product product = _mapper.Map<Product>(requestUpdate);
                await _productRepository.Update(product);

                ProductResponse response = _mapper.Map<ProductResponse>(product);
                response.Message = "Product updated successfully!";
                response.IsSuccess = true;
                response.StatusCode = 200;

                return response;

            }
            catch (Exception)
            {
                return new ProductResponse()
                {
                    Message = "Error while try update product.",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }
        }

        public async Task<BaseResponse> Delete(long id)
        {
            try
            {
                bool result = await _productRepository.Delete(id);

                if (!result)
                    return new BaseResponse()
                    {
                        Message = "Product with this id not found.",
                        IsSuccess = false,
                        StatusCode = 204
                    };

                return new BaseResponse()
                {
                    Message = "Product deleted successfully!",
                    IsSuccess = true,
                    StatusCode = 200
                };
            }
            catch (Exception)
            {
                return new BaseResponse()
                {
                    Message = "Error while try delete product.",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }
        }
    }
}
