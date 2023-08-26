using CSharpFunctionalExtensions;
using MassTransit;
using Scalable.Core.Application;
using Scalable.Core.Domain;
using Scalable.Core.Infrastructure;
using ILogger = Serilog.ILogger;

namespace Scalable.Api
{
    public class IntegrationEventPublisher : BackgroundService
    {
        private readonly ILogger logger;
        private const int TIMEOUT_MILISECONDS = 5000;
        private readonly IServiceProvider serviceProvider;

        public IntegrationEventPublisher(ILogger logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.Information("Checking for integration events every {timeout}ms", TIMEOUT_MILISECONDS);
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TIMEOUT_MILISECONDS, stoppingToken);

                var integrationEvents = await GetOrderedIntegrationEvents();

                foreach (var integrationEvent in integrationEvents)
                {
                    await PublishIntegrationEventAsync(integrationEvent);
                }
            }
        }

        private async Task<IEnumerable<IntegrationEvent>> GetOrderedIntegrationEvents()
        {
            IEnumerable<IntegrationEvent> integrationEvents = new List<IntegrationEvent>();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IIntegrationEventsUnitOfWork<CoreContext>>();

                integrationEvents = await unitOfWork.IntegrationEventsRepository.GetAllPendingEvents();
            }

            logger.Information("Retrieved {count} events from outbox", integrationEvents.Count());

            _ = integrationEvents.OrderBy(e => e.Created);

            return integrationEvents;
        }

        private async Task<Result> PublishIntegrationEventAsync(IntegrationEvent integrationEvent)
        {
            try
            {
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IIntegrationEventsUnitOfWork<CoreContext>>();
                    var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                    var type = Type.GetType(integrationEvent.EventType + ", " + integrationEvent.AssemblyName);
                    if (type == null)
                    {
                        await MarkAsCanceled(unitOfWork, integrationEvent);
                        return Result.Failure("Can't create integration event");
                    }

                    var integrationEventTyped = Activator.CreateInstance(type, integrationEvent.Data);
                    if (integrationEventTyped == null)
                    {
                        await MarkAsCanceled(unitOfWork, integrationEvent);
                        return Result.Failure("Can't create integration event");
                    }

                    await publishEndpoint.Publish(integrationEventTyped);

                    await unitOfWork.IntegrationEventsRepository.MarkEventAsProcessed(integrationEvent.Id);
                    await unitOfWork.SaveChangesAsync();
                }

                return Result.Success();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error publishing outbox event - {Message}", ex.Message);
                using (IServiceScope scope = serviceProvider.CreateScope())
                {
                    var unitOfWork = scope.ServiceProvider.GetRequiredService<IIntegrationEventsUnitOfWork<CoreContext>>();
                    await MarkAsCanceled(unitOfWork, integrationEvent);
                }
                return Result.Failure($"Error publishing outbox event - {ex.Message}");
            }
        }

        private async Task MarkAsCanceled(IIntegrationEventsUnitOfWork<CoreContext> unitOfWork, IntegrationEvent integrationEvent)
        {
            logger.Error("Can't create integration event: {@EventType}", integrationEvent.EventType);

            await unitOfWork.IntegrationEventsRepository.MarkEventAsCancelled(integrationEvent.Id);
            await unitOfWork.SaveChangesAsync();
        }
    }
}