using CSharpFunctionalExtensions;
using Scalable.Core.Application;
using Scalable.Core.Domain;
using MediatR;

namespace Scalable.Core.Infrastructure
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        protected readonly DbContextBase context;
        private readonly IMediator mediator;

        public UnitOfWorkBase(DbContextBase context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public async Task<Result> SaveChangesAsync()
        {
            var aggregatesWithDomainEvents = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
            .Select(e => e.Entity)
            .ToList();

            var domainEvents = aggregatesWithDomainEvents
            .SelectMany(x => x.DomainEvents)
            .ToList();

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

            var result = await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
