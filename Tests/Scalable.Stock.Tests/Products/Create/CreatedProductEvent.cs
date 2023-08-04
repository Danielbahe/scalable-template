using FluentAssertions;
using Scalable.Stock.Products.Create;
using Scalable.Stock.Products.Domain;

namespace Scalable.Stock.Tests.Products.Create
{
    public class CreatedProductEventShould
    {
        [Fact]
        public void CreatedProductEventShouldBeCreated()
        {
            var product = Product.Create(new CreateProductCommand("validName"));

            var createdEvent = new CreatedProductEvent(product.Value);

            createdEvent.AggregateId.Should().NotBe(Guid.Empty);
            createdEvent.EventId.Should().NotBe(Guid.Empty);
            createdEvent.Product.Should().NotBeNull();
            createdEvent.EventVersion.Should().Be(1);
            createdEvent.EventType.Should().Be(typeof(CreatedProductEvent).Name);
            createdEvent.OccurredOn.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
            createdEvent.TimeStamp.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }
    }
}
