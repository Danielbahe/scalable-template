using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Scalable.Core.Application;
using Scalable.Core.Domain;

namespace Scalable.Core.Infrastructure
{
    public class IntegrationEventsRepository : IIntegrationEventsRepository
    {
        private readonly DbContextBase context;

        public IntegrationEventsRepository(DbContextBase context)
        {
            this.context = context;
        }

        public async Task AddAsync(IntegrationEvent integrationEvent)
        {
            await context.IntegrationEvents.AddAsync(integrationEvent);
        }

        public async Task<IEnumerable<IntegrationEvent>> GetAllPendingEvents()
        {
            return await context.IntegrationEvents.Where(e => e.Status == EventStatus.Pending).ToListAsync();
        }

        public async Task<Result> MarkEventAsCancelled(Guid id)
        {
            var integrationEvent = await context.IntegrationEvents.SingleOrDefaultAsync(e => e.Id == id);
            if (integrationEvent == null) return Result.Failure("Element not Found");

            integrationEvent.MarkAsCanceled();

            context.IntegrationEvents.Update(integrationEvent);

            return Result.Success(integrationEvent);
        }

        public async Task<Result> MarkEventAsProcessed(Guid id)
        {
            var integrationEvent = await context.IntegrationEvents.SingleOrDefaultAsync(e => e.Id == id);
            if(integrationEvent == null) return Result.Failure("Element not Found");

            integrationEvent.MarkAsProcessed();

            context.IntegrationEvents.Update(integrationEvent);

            return Result.Success(integrationEvent);
        }
    }
}
