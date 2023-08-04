namespace Scalable.Core
{
    public class InvalidIntegrationEventException : Exception
    {
        public InvalidIntegrationEventException() : base("Can't create a generic integration event") { }
    }
}
