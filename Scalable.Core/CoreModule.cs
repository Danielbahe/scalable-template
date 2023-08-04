using Microsoft.Extensions.DependencyInjection;
using Scalable.Core.Application;
using Scalable.Core.Infrastructure;
using System.Reflection;

namespace Scalable.Core
{
    public static class CoreModule
    {
        public static Assembly GetAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        public static void RegisterCoreModuleServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IIntegrationEventsUnitOfWork<CoreContext>, IntegrationEventsUnitOfWork<CoreContext>>();
        }
    }
}
