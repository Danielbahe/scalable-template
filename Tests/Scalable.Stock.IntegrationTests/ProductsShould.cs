using FluentAssertions;
using Scalable.Stock.Products.Get;

namespace Scalable.Stock.IntegrationTests
{
    public class ProductsShould : BaseIntegrationTest
    {
        public ProductsShould(ApiWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        public async void BeSuccessfullyRetrieved()
        {
            var result = await Sender.Send(new GetAllProductsPaginatedQuery());

            result.Should().NotBeNull();            
        }
    }
}