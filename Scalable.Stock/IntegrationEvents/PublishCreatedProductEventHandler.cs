using MediatR;
using Scalable.Core.Application;
using Scalable.Stock.Products.Create;
using Serilog;
using System.Text.Json;

namespace Scalable.Stock.IntegrationEvents
{
    public class PublishCreatedProductEventHandler : INotificationHandler<CreatedProductEvent>
    {
        private readonly ILogger logger;
        private readonly IIntegrationEventsUnitOfWork<StockContext> unitOfWork;

        public PublishCreatedProductEventHandler(ILogger logger, IIntegrationEventsUnitOfWork<StockContext> unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        public async Task Handle(CreatedProductEvent notification, CancellationToken cancellationToken)
        {
            var integrationEvent = new CreatedProductIntegrationEvent(JsonSerializer.Serialize(notification.Product));
            await unitOfWork.IntegrationEventsRepository.AddAsync(integrationEvent);

            logger.Information("Stored Created Product Integration Event: {@integrationEvent}", integrationEvent);
        }
    }
}