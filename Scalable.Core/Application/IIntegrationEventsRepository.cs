using CSharpFunctionalExtensions;
using Scalable.Core.Domain;

namespace Scalable.Core.Application
{
    public interface IIntegrationEventsRepository
    {
        Task AddAsync(IntegrationEvent integrationEvent);
        Task<IEnumerable<IntegrationEvent>> GetAllPendingEvents();
        Task<Result> MarkEventAsProcessed(Guid id);
        Task<Result> MarkEventAsCancelled(Guid id);
    }
}
