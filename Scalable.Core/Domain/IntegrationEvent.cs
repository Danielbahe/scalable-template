namespace Scalable.Core.Domain
{
    public enum EventStatus
    {
        None = 0,
        Pending = 1,
        Processed = 2,
        Canceled = 3,
    }

    public class IntegrationEvent : Entity
    {
        public string EventType { get; set; } = string.Empty;
        public string AssemblyName { get; set; } = string.Empty;
        public string Data { get; } = string.Empty;
        public DateTime? Processed { get; private set; } = null;
        public EventStatus Status { get; private set; } = EventStatus.None;

        public IntegrationEvent()
        {
            Status = EventStatus.Pending;
            SetType();
        }

        public IntegrationEvent(string data)
        {
            Status = EventStatus.Pending;
            Data = data;
            SetType();
        }

        private void SetType() 
        {
            EventType = GetType().FullName ?? throw new InvalidIntegrationEventException();
            AssemblyName = GetType().Assembly.FullName ?? throw new InvalidIntegrationEventException();
        }

        public void MarkAsProcessed()
        {
            Processed = DateTime.UtcNow;
            Status = EventStatus.Processed;
        }

        public void MarkAsCanceled()
        {
            Processed = DateTime.UtcNow;
            Status = EventStatus.Canceled;
        }
    }
}
