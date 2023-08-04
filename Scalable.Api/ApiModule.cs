using Scalable.Stock;
using Scalable.Core;
using System.Reflection;
using MassTransit;

namespace Scalable.Api
{
    public static class ApiModule
    {
        public static Assembly[] GetAssemblies()
        {
            var assemblies = new List<Assembly>
            {
                StockModule.GetAssembly()
            };

            return assemblies.ToArray();
        }

        public static void RegisterAllModulesServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterCoreModuleServices();
            serviceCollection.RegisterStockModuleServices();
        }

        public static void RegisterDbContexts(this IServiceCollection serviceCollection)
        {
            serviceCollection.RegisterCoreModuleServices();
            serviceCollection.RegisterStockModuleServices();
        }

        public static void SetupIntegrationEvents(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMassTransit(x =>
             {
                 x.SetKebabCaseEndpointNameFormatter();
                 x.AddConsumers(GetAssemblies());
                 x.UsingRabbitMq((context, cfg) =>
                 {
                     cfg.Host("rabbitmq", "/", h =>
                     {
                         h.Username("guest");
                         h.Password("guest");
                     });
                     cfg.ConfigureEndpoints(context);

                 });
             });

            serviceCollection.AddHostedService<IntegrationEventPublisher>();
        }
    }
}
