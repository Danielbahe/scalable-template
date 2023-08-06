using CSharpFunctionalExtensions;

namespace Scalable.Stock.Products.Domain
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsPaginated(int pageSize, int page);
        Task<Result<Product>> GetById(Guid id);
    }
}
