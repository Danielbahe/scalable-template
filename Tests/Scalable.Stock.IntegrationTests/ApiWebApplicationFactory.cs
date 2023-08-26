using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Scalable.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Scalable.Stock.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<ApiAssembly>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer dbContainer = new PostgreSqlBuilder()
                        .WithImage("testcontainers/helloworld:1.1.0")
                        .WithDatabase("scalable")
                        .WithUsername("sa")
                        .WithPassword("sa")
                        .Build();

        public Task InitializeAsync()
        {
            return dbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            base.ConfigureWebHost(builder);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<StockContext>(dbContextOptions => 
                dbContextOptions
                .UseNpgsql(dbContainer.GetConnectionString()));
            });

            return base.CreateHost(builder);
        }

        public new Task DisposeAsync()
        {
            return dbContainer.StopAsync();
        }
    }
}
