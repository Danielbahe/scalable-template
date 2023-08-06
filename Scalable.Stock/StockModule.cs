using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scalable.Core.Application;
using Scalable.Core.Infrastructure;
using System.Reflection;

namespace Scalable.Stock
{
    public static class StockModule
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        public static void RegisterStockModuleServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IStockUnitOfWork, StockUnitOfWork>();
            serviceCollection.AddScoped<IIntegrationEventsUnitOfWork<StockContext>, IntegrationEventsUnitOfWork<StockContext>>();
        }

        public static void RegisterStockModuleDbContexts(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresSqlDatabase");

            serviceCollection.AddDbContext<StockContext>(
                dbContextOptions => dbContextOptions
                .UseNpgsql(connectionString));
        }
    }
}
