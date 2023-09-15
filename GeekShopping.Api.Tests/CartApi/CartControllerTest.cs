using FakeItEasy;
using FluentAssertions;
using GeekShopping.CartApi.Controllers;
using GeekShopping.CartApi.Dtos.Request;
using GeekShopping.CartApi.Dtos.Response;
using GeekShopping.CartApi.Interfaces;
using GeekShopping.CartApi.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Tests.CartApi
{
    public class CartControllerTest
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
        private readonly IRabbitMQMessageSender _senderService;
        private readonly CartController _controller;

        public CartControllerTest()
        {
            _cartService = A.Fake<ICartService>();  
            _couponService = A.Fake<ICouponService>();
            _senderService = A.Fake<IRabbitMQMessageSender>();
            _controller = new CartController(_cartService, _couponService, _senderService);
        }

        [Fact]
        public async Task CartController_FindCartById_ReturnOk()
        {
            //Arrange
            string idValid = "idValid";

            CartResponse response = A.Fake<CartResponse>();

            A.CallTo(() => _cartService.FindCartByUserId(idValid)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.FindCartById(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CartController_FindCartById_ReturnBadRequest()
        {
            //Arrange
            string idInvalid = "idInvalid";

            _controller.ModelState.AddModelError("keyError","modeState is invalid");
            //Act
            var result = await _controller.FindCartById(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400) ;
        }

        [Fact]
        public async Task CartController_CreateCart_ReturnOk()
        {
            //Arrange
            CartRequest request = A.Fake<CartRequest>();
            CartResponse response = A.Fake<CartResponse>();

            A.CallTo(() => _cartService.SaveOrUpdateCart(request)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.CreateCart(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CartController_CreateAndUpdateCart_ReturnBadRequest()
        {
            //Arrange
            CartRequest request = A.Fake<CartRequest>();
            _controller.ModelState.AddModelError("keyError", "modeState is invalid");

            //Act
            var result = await _controller.CreateCart(request);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CartController_UpdateCart_ReturnOk()
        {
            //Arrange 
            CartRequest request = A.Fake<CartRequest>();
            CartResponse response = A.Fake<CartResponse>();

            A.CallTo(() => _cartService.SaveOrUpdateCart(request)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.UpdateCart(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();   
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CartController_RemoveCart_ReturnOk()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idValid = random.NextInt64();

            A.CallTo(() => _cartService.RemoveFromCart(idValid)).Returns(Task.FromResult(true));

            //Act
            var result = await _controller.RemoveCart(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CartController_RemoveCart_ReturnBadRequestFound()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idInvalid = random.NextInt64();

            A.CallTo(() => _cartService.RemoveFromCart(idInvalid)).Returns(Task.FromResult(false));

            //Act
            var result = await _controller.RemoveCart(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CartController_RemoveCart_ReturnBadRequestModelState()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idInvalid = random.NextInt64();

            _controller.ModelState.AddModelError("keyError", "modeState is invalid");

            //Act
            var result = await _controller.RemoveCart(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task CartController_ApplyCoupon_ReturnOk()
        {
            //Arrange
            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest cartRequest = new CartRequest() { CartDetails = detailRequest, CartHeader = headerRequest};

            A.CallTo(() => _cartService.ApplyCoupon(headerRequest.UserId, headerRequest.CouponCode)).Returns(Task.FromResult(true));

            //Act
            var result = await _controller.ApplyCoupon(cartRequest);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task CartController_ApplyCoupon_ReturnNotFound()
        {
            //Arrange
            CartHeaderRequest headerRequest = A.Fake<CartHeaderRequest>();
            IEnumerable<CartDetailRequest> detailRequest = A.Fake<IEnumerable<CartDetailRequest>>();
            CartRequest request = new CartRequest() { CartHeader = headerRequest, CartDetails = detailRequest };

            A.CallTo(() => _cartService.ApplyCoupon(headerRequest.UserId, headerRequest.CouponCode)).Returns(Task.FromResult(false));

            //Act
            var result = await _controller.ApplyCoupon(request);

            //Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CartController_RemoveCoupon_ReturnOk()
        {
            //Arrange
            string idValid = "idValid";

            A.CallTo(() => _cartService.RemoveCoupon(idValid)).Returns(Task.FromResult(true));

            //Act
            var result = await _controller.RemoveCoupon(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }
        
        [Fact]
        public async Task CartController_RemoveCoupon_ReturnNotFound()
        {
            //Arrange
            string idValid = "idValid";

            A.CallTo(() => _cartService.RemoveCoupon(idValid)).Returns(Task.FromResult(false));

            //Act
            var result = await _controller.RemoveCoupon(idValid);

            //Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }
    }
}
