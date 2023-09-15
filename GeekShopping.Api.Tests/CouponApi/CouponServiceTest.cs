using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using GeekShopping.CouponApi.Dtos.Response;
using GeekShopping.CouponApi.Interfaces;
using GeekShopping.CouponApi.Model;
using GeekShopping.CouponApi.Service;

namespace GeekShopping.Api.Tests.CouponApi
{
    public class CouponServiceTest
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;
        private readonly CouponService _service;

        public CouponServiceTest()
        {
            _repository = A.Fake<ICouponRepository>();
            _mapper = A.Fake<IMapper>();
            _service = new CouponService(_repository, _mapper);
        }

        [Fact]
        public async Task CouponService_GetCouponByCouponCode_ReturnCouponResponse()
        {
            //Arrange
            string couponValid = "couponValid";

            Coupon couponModel = A.Fake<Coupon>(); 
            CouponResponse response = A.Fake<CouponResponse>();

            A.CallTo(() => _repository.FindCouponByCouponCode(couponValid)).Returns(Task.FromResult(couponModel));
            A.CallTo(() => _mapper.Map<CouponResponse>(couponModel)).Returns(response);

            //Act
            var result = await _service.GetCouponByCouponCode(couponValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(response);
        }
    }
}
