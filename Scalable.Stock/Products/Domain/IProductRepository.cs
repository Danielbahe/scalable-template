using CSharpFunctionalExtensions;

namespace Scalable.Stock.Products.Domain
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<Result<Product>> GetById(Guid id);
    }
}
