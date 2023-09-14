using FakeItEasy;
using FluentAssertions;
using GeekShopping.Controllers;
using GeekShopping.Dtos.Request;
using GeekShopping.Dtos.Request.Update;
using GeekShopping.Dtos.Response;
using GeekShopping.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping.Api.Tests.Controllers
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
            var products = A.Fake<IEnumerable<ProductResponse>>();

            A.CallTo(() => _service.FindAllProducts()).Returns(Task.FromResult(products));

            //Act
            var result = await _controller.FindAll();

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeOfType<Exception>();
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task ProductController_FindById()
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
    }
}