using CSharpFunctionalExtensions;
using FluentAssertions;
using Scalable.Stock.Products.Create;
using Scalable.Stock.Products.Domain;

namespace Scalable.Stock.Tests.Products.Domain
{
    public class ProductShould
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("a", false)]
        [InlineData("pa", true)]
        [InlineData("ValidName", true)]
        public void ProductShouldBeCreated(string name, bool isValid)
        {
            var command = new CreateProductCommand(name);

            Result<Product> productResult = Product.Create(command);

            productResult.IsSuccess.Should().Be(isValid);

            if (isValid)
            {
                productResult.Value.Name.Should().Be(name);
                productResult.Value.Id.Should().NotBeEmpty();
                productResult.Value.Modified.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
                productResult.Value.Created.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
                productResult.Value.DomainEvents.Should().HaveCountGreaterThanOrEqualTo(1);
            }
        }
    }
}
