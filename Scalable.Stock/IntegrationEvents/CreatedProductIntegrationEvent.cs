using Scalable.Core.Domain;

namespace Scalable.Stock.IntegrationEvents
{
    public class CreatedProductIntegrationEvent : IntegrationEvent
    {
        public CreatedProductIntegrationEvent(string data) : base(data)
        {

        }
    }
}
