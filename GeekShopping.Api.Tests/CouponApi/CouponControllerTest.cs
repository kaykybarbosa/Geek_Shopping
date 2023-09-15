using FakeItEasy;
using FluentAssertions;
using GeekShopping.CouponApi.Controllers;
using GeekShopping.CouponApi.Dtos.Response;
using GeekShopping.CouponApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Tests.CouponApi
{
    public  class CouponControllerTest
    {
        private readonly ICouponService _service;
        private readonly CouponController _controller;

        public CouponControllerTest()
        {
            _service = A.Fake<ICouponService>();
            _controller = new CouponController(_service);
        }

        [Fact]
        public async Task CouponController_GetCouponByCouponCode_ReturnOk()
        {
            //Arrange
            string couponValid = "couponValid";

            CouponResponse response = A.Fake<CouponResponse>();

            A.CallTo(() => _service.GetCouponByCouponCode(couponValid)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.GetCouponByCouponCode(couponValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CouponController_GetCouponByCouponCode_ReturnNotFound()
        {
            //Arrange
            var couponInvalid = "couponInvalid";

            CouponResponse? response = null;

            A.CallTo(() => _service.GetCouponByCouponCode(couponInvalid)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.GetCouponByCouponCode(couponInvalid);

            //Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }
    }
}