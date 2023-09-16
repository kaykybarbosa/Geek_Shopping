using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
using GeekShopping.Interfaces;
using GeekShopping.Model;
using GeekShopping.Services;

namespace GeekShopping.Api.Tests.ProductApi
{
    public class ProductServiceTest
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _repository = A.Fake<IProductRepository>();
            _mapper = A.Fake<IMapper>();
            _service = new ProductService(_repository, _mapper);
        }

        [Fact]
        public async Task ProductService_FindProductById_ReturnProductResponse()
        {
            //Arrange
            long idValid = 1L;

            Product productModel = A.Fake<Product>();
            ProductResponse response = A.Fake<ProductResponse>();

            A.CallTo(() => _repository.FindById(idValid)).Returns(Task.FromResult(productModel));
            A.CallTo(() => _mapper.Map<ProductResponse>(productModel)).Returns(response);

            //Act
            var result = await _service.FindProductById(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductService_FindProductById_ReturnNull()
        {
            //Arrange
            var idInvalid = 0L;
            Product? productNull = null;

            A.CallTo(() => _repository.FindById(idInvalid)).Returns(Task.FromResult(productNull));

            //Act
            var result = await _service.FindProductById(idInvalid);

            //Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task ProductService_FindAllProducts_ReturnListProductResponse()
        {
            //Arrange
            IEnumerable<Product> products = A.Fake<IEnumerable<Product>>();
            IEnumerable<ProductResponse> response = A.Fake<IEnumerable<ProductResponse>>();

            A.CallTo(() => _repository.FindAll()).Returns(Task.FromResult(products));
            A.CallTo(() => _mapper.Map<IEnumerable<ProductResponse>>(products)).Returns(response);

            //Act
            var result = await _service.FindAllProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProducService_CreateProduct_ReturnProductResponse()
        {
            //Arrange
            ProductRequest request = A.Fake<ProductRequest>();
            ProductResponse response = A.Fake<ProductResponse>();
            Product product = A.Fake<Product>();

            A.CallTo(() => _mapper.Map<Product>(request)).Returns(product);
            A.CallTo(() => _repository.Create(product)).Returns(Task.FromResult(product));
            A.CallTo(() => _mapper.Map<ProductResponse>(product)).Returns(response);

            //Act
            var result = await _service.CreateProduct(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeEquivalentTo(response);    
        }
        
        [Fact]
        public async Task ProductService_UpdateProduct_ReturnProductResponse()
        {
            //Arrange
            ProductRequestUpdate request = A.Fake<ProductRequestUpdate>();
            ProductResponse response = A.Fake<ProductResponse>();
            Product product = A.Fake<Product>();

            A.CallTo(() => _mapper.Map<Product>(response)).Returns(product);
            A.CallTo(() => _repository.Update(product)).Returns(Task.FromResult(product));
            A.CallTo(() => _mapper.Map<ProductResponse>(product)).Returns(response);

            //Act
            var result = await _service.UpdateProduct(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductService_DeleteProduct_ReturnTrue()
        {
            //Arrange
            long idValid = 1L;
            A.CallTo(() => _repository.Delete(idValid)).Returns(Task.FromResult(true));

            //Act
            var result = await _service.DeleteProduct(idValid);

            //Assert
            result.Should().NotBe(false);
            result.Should().BeTrue(); 
        }

        [Fact]
        public async Task ProductService_DeleteProduct_ReturnFalse()
        {
            //Arrange
            long idInvalid = 0L;
            A.CallTo(() => _repository.Delete(idInvalid)).Returns(Task.FromResult(false));

            //Act
            var result = await _service.DeleteProduct(idInvalid);

            //Assert
            result.Should().NotBe(true);
            result.Should().Be(false); 
        }
    }
}
