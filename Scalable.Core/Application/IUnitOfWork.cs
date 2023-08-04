using CSharpFunctionalExtensions;

namespace Scalable.Core.Application
{
    public interface IUnitOfWork
    {
        Task<Result> SaveChangesAsync();
    }
}
