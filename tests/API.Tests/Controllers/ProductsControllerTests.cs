using API.Controllers;
using Application.Common.Models;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.DeleteProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.DTOs;
using Application.Products.Queries.GetProductsWithPagination;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace API.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private ProductsController CreateControllerWithMediator(Mock<IMediator> mediatorMock)
        {
            var controller = new ProductsController();

            var services = new ServiceCollection()
                .AddSingleton<IMediator>(mediatorMock.Object)
                .BuildServiceProvider();

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    RequestServices = services
                }
            };

            return controller;
        }

        [Fact]
        public async Task GetProductsWithPagination_ReturnsPaginatedList()
        {
            var mediatorMock = new Mock<IMediator>();
            var expected = new PaginatedList<ProductDto>(new List<ProductDto>(), 1, 1, 10);
            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetProductsWithPaginationQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var controller = CreateControllerWithMediator(mediatorMock);

            var actionResult = await controller.GetProductsWithPagination(new GetProductsWithPaginationQuery());

            Assert.Null(actionResult.Result);
            Assert.Same(expected, actionResult.Value);
        }

        [Fact]
        public async Task Create_ReturnsCreatedId()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(42L);

            var controller = CreateControllerWithMediator(mediatorMock);

            var actionResult = await controller.Create(new CreateProductCommand());

            Assert.Null(actionResult.Result);
            Assert.Equal(42L, actionResult.Value);
        }

        [Fact]
        public async Task Update_WhenIdMismatch_ReturnsBadRequest()
        {
            var mediatorMock = new Mock<IMediator>();
            var controller = CreateControllerWithMediator(mediatorMock);
            var cmd = new UpdateProductCommand { Id = 1 };

            var result = await controller.Update(999, cmd);

            var badRequest = Assert.IsType<BadRequestResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public async Task Update_WhenIdMatch_ReturnsNoContent()
        {
            var mediatorMock = new Mock<IMediator>();
            // Setup Send to return a completed Task for commands that do not return a value
            mediatorMock
                .Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var controller = CreateControllerWithMediator(mediatorMock);
            var cmd = new UpdateProductCommand { Id = 5 };

            var result = await controller.Update(5, cmd);

            var noContent = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContent.StatusCode);
            mediatorMock.Verify(m => m.Send(cmd, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Always_ReturnsNoContent()
        {
            var mediatorMock = new Mock<IMediator>();
            // Setup Send to return a completed Task for commands that do not return a value
            mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var controller = CreateControllerWithMediator(mediatorMock);

            var result = await controller.Delete(123);

            var noContent = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContent.StatusCode);
            mediatorMock.Verify(m =>
                m.Send(It.Is<DeleteProductCommand>(c => c.Id == 123), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }

}
