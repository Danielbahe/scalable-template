using MediatR;

namespace Scalable.Core.Domain;

public interface IDomainEvent : INotification
{

    /// <summary>
    /// Gets the event identifier.
    /// </summary>
    Guid EventId { get; }

    /// <summary>
    /// Gets the event/aggregate root version.
    /// </summary>
    long EventVersion { get; }

    /// <summary>
    /// Gets the date the <see cref="IEvent"/> occurred on.
    /// </summary>
    DateTime OccurredOn { get; }

    DateTimeOffset TimeStamp { get; }

    /// <summary>
    /// Id of the aggregate
    /// </summary>
    Guid AggregateId { get; }

    /// <summary>
    /// Gets type of this event.
    /// </summary>
    public string EventType { get; }
}

public abstract record DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public long EventVersion => 1;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public DateTimeOffset TimeStamp { get; } = DateTimeOffset.UtcNow;
    public string EventType => GetType().Name;
    public Guid AggregateId { get; private set; } = default!;

    public DomainEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}