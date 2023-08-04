namespace Scalable.Core.Application
{
    public interface IIntegrationEventsUnitOfWork<T> : IUnitOfWork
    {
        public IIntegrationEventsRepository IntegrationEventsRepository { get; }
    }
}
