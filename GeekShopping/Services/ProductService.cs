using AutoMapper;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
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

        public async Task<ProductResponse> FindProductById(long id)
        {
            Product product = await _productRepository.FindById(id);
            
            if (product != null)
            {
                ProductResponse productMapper = _mapper.Map<ProductResponse>(product);

                return productMapper;
            }

            return null;
        }

        public async Task<IEnumerable<ProductResponse>> FindAllProducts()
        {
            IEnumerable<Product> products = await _productRepository.FindAll();
           
            return _mapper.Map<IEnumerable<ProductResponse>>(products);
   
        }

        public async Task<ProductResponse> CreateProduct(ProductRequest request)
        {
            try
            {
                if (request == null)
                {
                    return null;
                }

                Product product = _mapper.Map<Product>(request);
                await _productRepository.Create(product);

                return _mapper.Map<ProductResponse>(product);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProductResponse> UpdateProduct(ProductRequestUpdate requestUpdate)
        {
            try
            {
                Product product = _mapper.Map<Product>(requestUpdate);
                await _productRepository.Update(product);

                return _mapper.Map<ProductResponse>(product);

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProduct(long id)
        {
            try
            {
                bool result = await _productRepository.Delete(id);

                if (!result)
                    return false;

                return true;
            
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}