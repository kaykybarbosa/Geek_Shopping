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
        public async Task CartService_ApplyCoupon_UserNonExistent_ReturnFalse()
        {
            //Arrange
            string userIdInvalid = "INVALID";
            string couponCode = "VALID";

            CartHeader? headerNull = null;

            A.CallTo(() => _repository.FindCartHeader(userIdInvalid)).Returns(Task.FromResult(headerNull));

            //Act
            var result = await _service.ApplyCoupon(userIdInvalid, couponCode);

            //Asseret
            result.Should().BeFalse();
            result.Should().NotBe(true);
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
        public async Task CartService_RemoveCoupon_ReturnFalse()
        {
            //Arrange
            string userIdNonExistent = "NONEXISTENT";

            CartHeader? headerNull = null;

            A.CallTo(() => _repository.FindCartHeader(userIdNonExistent)).Returns(Task.FromResult(headerNull));

            //Act
            var result = await _service.RemoveCoupon(userIdNonExistent);

            //Assert
            result.Should().BeFalse();
            result.Should().NotBe(true);
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
        public async Task CartService_SaveOrUpdateCart_SecondIf_ReturnCartResponse()
        {
            //Arrange
            long idNonExistent = 1L;
            string? userIdNonExistent = null;
            
            Product? productNull = null;
            CartHeader? headerNull = null;
            
            Product product = A.Fake<Product>();
            CartHeader header = A.Fake<CartHeader>();
            CartDetail detail = A.Fake<CartDetail>();
            IEnumerable<CartDetail> details = new List<CartDetail>()
            {
                new CartDetail()
                {
                    ProductId = 1
                }
            };
            Cart cart = new() { CartHeader = header, CartDetails = details};

            IEnumerable<CartDetailResponse> detailResponse = A.Fake<IEnumerable<CartDetailResponse>>();
            CartHeaderResponse headerResponse = A.Fake<CartHeaderResponse>();
            CartResponse response = new() {CartHeader = headerResponse, CartDetails = detailResponse};
            
            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest request = new() { CartHeader = headerRequest, CartDetails = detailRequest };

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
            result.CartDetails.Should().BeEquivalentTo(detailResponse);
            result.CartHeader.Should().BeEquivalentTo(headerResponse);
        }

        [Fact]
        public async Task CartService_SaveOrUpdateCart_FirstElse_If_ReturnCartResponse()
        {
            //Arrange
            long idNonExistent = 0L;
            string userIdExistent = Guid.NewGuid().ToString();
            Product? productNull = null;
            CartDetail? detailNull = null;

            Product product = A.Fake<Product>();
            CartHeader header = A.Fake<CartHeader>();
            CartDetail detail = A.Fake<CartDetail>();
            IEnumerable<CartDetail> details = new List<CartDetail>() 
            { 
                new CartDetail() 
                { 
                    ProductId = 0
                }
            };
            Cart cart = new() { CartHeader = header, CartDetails = details };

            IEnumerable<CartDetailResponse> detailResponse = A.Fake<IEnumerable<CartDetailResponse>>();
            CartHeaderResponse headerResponse = A.Fake<CartHeaderResponse>();
            CartResponse response = new() { CartHeader = headerResponse, CartDetails = detailResponse };

            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest request = new() { CartHeader = headerRequest, CartDetails = detailRequest };

            A.CallTo(() => _mapper.Map<Cart>(request)).Returns(cart);
            A.CallTo(() => _repository.FindProductById(idNonExistent)).Returns(Task.FromResult(productNull));
            A.CallTo(() => _repository.CreateProduct(product)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.FindCartHeaderNoTracking(userIdExistent)).Returns(Task.FromResult(header));
            A.CallTo(() => _repository.FindCartDetailNoTracking(idNonExistent, idNonExistent)).Returns(Task.FromResult(detailNull));
            A.CallTo(() => _repository.CreateCartDetails(detail)).Returns(Task.CompletedTask);
            A.CallTo(() => _mapper.Map<CartResponse>(cart)).Returns(response);

            //Act
            var result = await _service.SaveOrUpdateCart(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response);
            result.CartDetails.Should().NotBeNull();
            result.CartHeader.Should().NotBeNull();
            result.CartDetails.Should().BeEquivalentTo(detailResponse);
            result.CartHeader.Should().BeEquivalentTo(headerResponse);
        }

        [Fact]
        public async Task CartService_SaveOrUpdateCart_FirstElse_Else_ReturnCartResponse()
        {
            //Arrange
            long idNonExistent = 0L;
            long idExistent = 10L;
            Product? productNull = null;

            Product product = A.Fake<Product>();
            CartHeader header = A.Fake<CartHeader>();
            CartDetail detail = A.Fake<CartDetail>();
            IEnumerable<CartDetail> details = new List<CartDetail>()
            {
                new CartDetail()
                {
                    ProductId = 0
                }
            };
            Cart cart = new() { CartHeader = header, CartDetails = details };

            IEnumerable<CartDetailResponse> detailResponse = A.Fake<IEnumerable<CartDetailResponse>>();
            CartHeaderResponse headerResponse = A.Fake<CartHeaderResponse>();
            CartResponse response = new() { CartHeader = headerResponse, CartDetails = detailResponse };

            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest request = new() { CartHeader = headerRequest, CartDetails = detailRequest };

            A.CallTo(() => _mapper.Map<Cart>(request)).Returns(cart);
            A.CallTo(() => _repository.FindProductById(idNonExistent)).Returns(Task.FromResult(productNull));
            A.CallTo(() => _repository.CreateProduct(product)).Returns(Task.CompletedTask);
            A.CallTo(() => _repository.FindCartDetailNoTracking(idExistent, idExistent)).Returns(Task.FromResult(detail));
            A.CallTo(() => _repository.UpdateCartDetail(detail)).Returns(Task.CompletedTask);
            A.CallTo(() => _mapper.Map<CartResponse>(cart)).Returns(response);

            //Act
            var result = await _service.SaveOrUpdateCart(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response);
            result.CartDetails.Should().NotBeNull();
            result.CartDetails.Should().BeEquivalentTo(detailResponse);
            result.CartHeader.Should().NotBeNull();
            result.CartHeader.Should().BeEquivalentTo(headerResponse);
        }

        [Fact]
        public async Task CartService_RemoveFromCart_ReturnTrue()
        {
            //Arrange
            long idDetail = 1L;

            CartHeader header = A.Fake<CartHeader>();
            CartDetail detail = A.Fake<CartDetail>();
            CartDetail detail2 = A.Fake<CartDetail>();
            IEnumerable<CartDetail> details = new List<CartDetail>() { detail, detail2 };

            A.CallTo(() => _repository.FindCartDetail(idDetail)).Returns(Task.FromResult(detail));
            A.CallTo(() => _repository.FindCartDetails(idDetail)).Returns(Task.FromResult(details));
            A.CallTo(() => _repository.RemoveCartDetail(detail)).Returns(Task.CompletedTask);

            //Act
            var result = await _service.RemoveFromCart(idDetail);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
        }
    }
}
