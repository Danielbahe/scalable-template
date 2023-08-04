using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using Scalable.Stock.Products.Create;
using Scalable.Stock.Products.Domain;
using Scalable.Stock.Tests.Factories;

namespace Scalable.Stock.Tests.Products.Create
{
    public class CreateProductCommandHandlerShould
    {
        [Fact]
        public async void ShouldBeSuccessful()
        {
            var unitOfWorkMock = new Mock<IStockUnitOfWork>();
            unitOfWorkMock.Setup( u => u.ProductRepository.AddAsync(It.IsAny<Product>()))
                .Returns(Task.FromResult(Result.Success<Product>()));
            unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            var logger = LoggerMockFactory.GetLoggerMock();

            var command = new CreateProductCommand("validName");

            var handler = new CreateProductCommandHandler(unitOfWorkMock.Object, logger.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldBeFailure()
        {
            var unitOfWorkMock = new Mock<IStockUnitOfWork>();
            unitOfWorkMock.Setup( u => u.ProductRepository.AddAsync(It.IsAny<Product>()))
                .Returns(Task.FromResult(Result.Success<Product>()));
            unitOfWorkMock.Setup(u => u.SaveChangesAsync());

            var logger = LoggerMockFactory.GetLoggerMock();

            var command = new CreateProductCommand(string.Empty);

            var handler = new CreateProductCommandHandler(unitOfWorkMock.Object, logger.Object);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();
        }
    }
}
