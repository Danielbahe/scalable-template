using FluentAssertions;
using Scalable.Stock.Products.Domain;
using System.Net.Http.Json;

namespace Scalable.Stock.IntegrationTests
{
    public class ProductsShould : TestBase
    {
        [Fact]
        public async void BeSuccessfullyRetrieved()
        {
            var client = GetClient();

            var result = await client.GetFromJsonAsync<List<Product>>("/products");
            
            result.Should().NotBeNull();
            
        }
    }
}