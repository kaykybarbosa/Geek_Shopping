using FakeItEasy;
using FluentAssertions;
using GeekShopping.Controllers;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
using GeekShopping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Tests.ProductApi
{
    public class ProductControllerTest
    {
        private readonly ProductController _controller;
        private readonly IProductService _service;

        public ProductControllerTest()
        {
            _service = A.Fake<IProductService>();
            _controller = new ProductController(_service);
        }

        [Fact]
        public async Task ProductController_FindAll_ReturnOk()
        {
            //Arrage
            IEnumerable<ProductResponse> products = A.Fake<IEnumerable<ProductResponse>>();

            A.CallTo(() => _service.FindAllProducts()).Returns(Task.FromResult(products));

            //Act
            var result = await _controller.FindAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task ProductController_FindById_ReturnOk()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idValid = random.NextInt64(0, 50);

            ProductResponse response = A.Fake<ProductResponse>();

            A.CallTo(() => _service.FindProductById(A<long>.Ignored)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.FindById(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_FindById_ReturnNotFound()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idInvalid = random.NextInt64(0, 100);

            ProductResponse? response = null;

            A.CallTo(() => _service.FindProductById(idInvalid)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.FindById(idInvalid);

            //Assert
            result.Should().BeOfType<NotFoundResult>().Which.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task ProductController_Create_ReturnOk()
        {
            //Arrange
            ProductRequest request = A.Fake<ProductRequest>();
            ProductResponse response = A.Fake<ProductResponse>();

            A.CallTo(() => _service.CreateProduct(request)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.Create(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_Create_ReturnBadRequest()
        {
            //Arrange
            ProductResponse? response = null;
            ProductRequest requestInvalid = A.Fake<ProductRequest>();

            A.CallTo(() => _service.CreateProduct(requestInvalid)).Returns(Task.FromResult(response));
            
            //Act
            var result = await _controller.Create(requestInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ProductController_Update_ReturnOk()
        {
            //Arrange
            ProductRequestUpdate request = A.Fake<ProductRequestUpdate>();
            ProductResponse response = A.Fake<ProductResponse>();

            A.CallTo(() => _service.UpdateProduct(request)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.Update(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task ProductController_Update_ReturnBadRequest()
        {
            //Arrange
            ProductResponse? response = null;
            ProductRequestUpdate requestInvalid = A.Fake<ProductRequestUpdate>();

            A.CallTo(() => _service.UpdateProduct(requestInvalid)).Returns(Task.FromResult(response));

            //Act
            var result = await _controller.Update(requestInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task ProductController_Delete_ReturnOk()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idValid = random.NextInt64(0, 50);

            A.CallTo(() => _service.DeleteProduct(idValid)).Returns(Task.FromResult(true));

            //Act
            var result = await _controller.Delete(idValid);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(true);
        }

        [Fact]
        public async Task ProductController_Delete_ReturnBadRequest()
        {
            //Arrange
            Random random = A.Fake<Random>();
            long idInvalid = random.NextInt64(0, 100);

            A.CallTo(() => _service.DeleteProduct(idInvalid)).Returns(Task.FromResult(false));

            //Act
            var result = await _controller.Delete(idInvalid);

            //Assert
            result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }
    }
}