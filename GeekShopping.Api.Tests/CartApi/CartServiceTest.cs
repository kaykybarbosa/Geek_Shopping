using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response;
using GeekShopping.CartApi.Interfaces.IRepositories;
using GeekShopping.CartApi.Model;
using GeekShopping.CartApi.Services.CartService;

namespace GeekShopping.Api.Tests.CartApi
{
    public class CartServiceTest
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;
        private readonly CartService _service;

        public CartServiceTest()
        {
            _repository = A.Fake<ICartRepository>();
            _mapper = A.Fake<IMapper>();
            _service = new CartService(_repository, _mapper);
        }

        [Fact]
        public async Task CartService_ApplyCoupon_ReturnTrue()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();
            string couponCode = Guid.NewGuid().ToString();

            CartHeader header = A.Fake<CartHeader>();

            A.CallTo(() => _repository.FindCartHeader(userId)).Returns(Task.FromResult(header));
            A.CallTo(() => _repository.UpdateHeader(header)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.ApplyCoupon(userId, couponCode);

            //Assert
            result.Should().BeTrue();  
            result.Should().NotBe(false);
        }

        [Fact]
        public async Task CartService_RemoveCoupon_ReturnTrue()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();

            CartHeader header = A.Fake<CartHeader>();

            A.CallTo(() => _repository.FindCartHeader(userId)).Returns(Task.FromResult(header));
            A.CallTo(() => _repository.UpdateHeader(header)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.RemoveCoupon(userId);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
        }

        [Fact]
        public async Task CartService_ClearCart_ReturnTrue()
        {
            //Arrange
            string userId = Guid.NewGuid().ToString();

            CartHeader header = A.Fake<CartHeader>();

            A.CallTo(() => _repository.FindCartHeader(userId)).Returns(Task.FromResult(header));
            A.CallTo(() => _repository.RemoveCartDetailRange(header.Id)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.RemoveCartHeader(header)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.ClearCart(userId);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
        }

        [Fact]
        public async Task CartService_FindCartByUserId_ReturnCartResponse()
        {
            //Arrange
            string userId = new Guid().ToString();

            CartResponse response = A.Fake<CartResponse>();

            CartHeader header = A.Fake<CartHeader>();
            IEnumerable<CartDetail> detail = A.Fake<IEnumerable<CartDetail>>();
            Cart cart = A.Fake<Cart>();


            A.CallTo(() => _repository.FindCartHeader(userId)).Returns(Task.FromResult(header));
            A.CallTo(() => _repository.FindCartDetails(header.Id)).Returns(Task.FromResult(detail));
            A.CallTo(() => _mapper.Map<CartResponse>(cart)).Returns(response);

            //Act
            var result = await _service.FindCartByUserId(userId);

            //Assert
            result.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CartService_SaveOrUpdateCart_ReturnCartResponse()
        {
            //Arrange
            long idNonExistent = 1L;
            string? userIdNonExistent = null;
            
            Product? productNull = null;
            CartHeader? headerNull = null;
            
            Product product = A.Fake<Product>();
            CartHeader header = A.Fake<CartHeader>();
            CartDetail detail = A.Fake<CartDetail>();
            IEnumerable<CartDetail> details = new List<CartDetail>() { new CartDetail() {ProductId = 1} };

            Cart cart = new Cart() { CartHeader = header, CartDetails = details};
            CartResponse response = A.Fake<CartResponse>();
            
            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest request = new CartRequest() { CartHeader = headerRequest, CartDetails = detailRequest };

            A.CallTo(() => _mapper.Map<Cart>(request)).Returns(cart);
            A.CallTo(() => _repository.FindProductById(idNonExistent)).Returns(Task.FromResult(productNull));
            A.CallTo(() => _repository.CreateProduct(product)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.FindCartHeaderNoTracking(userIdNonExistent)).Returns(Task.FromResult(headerNull));
            A.CallTo(() => _repository.CreateCartHeader(header)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.CreateCartDetails(detail)).Returns(Task.CompletedTask);
            A.CallTo(() => _mapper.Map<CartResponse>(cart)).Returns(response);

            //Act
            var result = await _service.SaveOrUpdateCart(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response);
        }
    }
}
