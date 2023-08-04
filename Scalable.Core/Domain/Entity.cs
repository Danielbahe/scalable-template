namespace Scalable.Core.Domain
{
    public abstract class Entity
    {
        public Guid Id { get; protected init; } = Guid.NewGuid();
        public DateTime Created { get; private set; } = DateTime.UtcNow;
        public DateTime Modified { get; private set; } = DateTime.UtcNow;
    }
}