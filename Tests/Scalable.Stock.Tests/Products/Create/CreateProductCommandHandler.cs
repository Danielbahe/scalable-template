using CSharpFunctionalExtensions;
using FluentAssertions;
using NSubstitute;
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
            var unitOfWorkMock = Substitute.For<IStockUnitOfWork>();
            unitOfWorkMock.ProductRepository.AddAsync(Arg.Any<Product>()).Returns(Task.FromResult(Result.Success<Product>()));

            var logger = LoggerMockFactory.GetLoggerMock();

            var command = new CreateProductCommand("validName");

            var handler = new CreateProductCommandHandler(unitOfWorkMock, logger);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
        }

        [Fact]
        public async void ShouldBeFailure()
        {
            var unitOfWorkMock = Substitute.For<IStockUnitOfWork>();
            unitOfWorkMock.ProductRepository.AddAsync(Arg.Any<Product>()).Returns(Task.FromResult(Result.Success<Product>()));

            var logger = LoggerMockFactory.GetLoggerMock();

            var command = new CreateProductCommand(string.Empty);

            var handler = new CreateProductCommandHandler(unitOfWorkMock, logger);

            var result = await handler.Handle(command, CancellationToken.None);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNullOrEmpty();
        }
    }
}
