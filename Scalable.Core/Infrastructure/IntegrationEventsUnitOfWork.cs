using MediatR;
using Microsoft.EntityFrameworkCore;
using Scalable.Core.Application;

namespace Scalable.Core.Infrastructure
{

    public class IntegrationEventsUnitOfWork<T> : UnitOfWorkBase, IIntegrationEventsUnitOfWork<T> where T : DbContextBase
    {
        public IIntegrationEventsRepository IntegrationEventsRepository => new IntegrationEventsRepository(context);

        public IntegrationEventsUnitOfWork(T context, IMediator mediator) : base(context, mediator)
        {
        }
    }

    public class CoreContext : DbContextBase
    {
        public CoreContext(DbContextOptions<CoreContext> options) : base(options)
        {
        }
    }
}
