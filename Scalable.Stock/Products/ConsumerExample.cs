using MassTransit;
using Scalable.Stock.IntegrationEvents;
using Serilog;

namespace Scalable.Stock.Products
{
    public class CreatedProductConsumer : IConsumer<CreatedProductIntegrationEvent>
    {
        private readonly ILogger logger = default!;

        public CreatedProductConsumer(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<CreatedProductIntegrationEvent> context)
        {
            logger.Debug("Consuming Integration Event data: @{Message}", context.Message);

            await Task.FromResult(0);
        }
    }
}
