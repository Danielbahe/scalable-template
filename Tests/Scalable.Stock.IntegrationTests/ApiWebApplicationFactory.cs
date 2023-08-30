using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Microsoft.AspNetCore.TestHost;
using Scalable.Core.Infrastructure;

namespace Scalable.Stock.IntegrationTests
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer dbContainer = new PostgreSqlBuilder()
                        .WithImage("postgres")
                        .WithDatabase("database-sql")
                        .WithUsername("postgres")
                        .WithPassword("miau1234")
                        .Build();

        public Task InitializeAsync()
        {
            return dbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var descriptor = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<StockContext>));
                var descriptor2 = services.SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<CoreContext>));

                if (descriptor is not null)
                {
                    services.Remove(descriptor);
                }

                if (descriptor2 is not null)
                {
                    services.Remove(descriptor2);
                }

                services.AddDbContext<StockContext>(dbContextOptions =>
                    dbContextOptions
                        .UseNpgsql(dbContainer.GetConnectionString()));

                services.AddDbContext<CoreContext>(dbContextOptions =>
                    dbContextOptions
                        .UseNpgsql(dbContainer.GetConnectionString()));
            });

            base.ConfigureWebHost(builder);
        }

        public new Task DisposeAsync()
        {
            return dbContainer.StopAsync();
        }
    }
}
