using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Scalable.Stock.Products.Domain;

namespace Scalable.Stock.Products.Infrastructure
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockContext context;
        public ProductRepository(StockContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsPaginated(int pageSize, int page)
        {
            return await context.Products
                .OrderBy(p => p.Created)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Result<Product>> GetById(Guid id)
        {
            var product = await context.Products.FirstOrDefaultAsync(p=> p.Id == id);
            return product == null ? Result.Failure<Product>("ss") : Result.Success(product);
        }
    }
}
