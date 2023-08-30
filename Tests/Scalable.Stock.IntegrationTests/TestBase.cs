using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Scalable.Stock.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly ISender Sender;

        protected BaseIntegrationTest(ApiWebApplicationFactory factory)
        {
            _scope = factory.Services.CreateScope();

            Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        }
    }
}
