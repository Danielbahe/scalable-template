using System.Collections.Concurrent;

namespace Scalable.Core.Domain
{
    public abstract class AggregateRoot : Entity
    {
        private readonly ConcurrentQueue<IDomainEvent> _domainEvents = new();
        public ConcurrentQueue<IDomainEvent> DomainEvents => _domainEvents;
        protected void AddDomainEvent(IDomainEvent domainEvent)
        {
            if (!DomainEvents.Any(x => Equals(x.EventId, domainEvent.EventId)))
            {
                DomainEvents.Enqueue(domainEvent);
            }
        }
    }
}