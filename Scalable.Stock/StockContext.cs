using Scalable.Stock.Products.Domain;
using Microsoft.EntityFrameworkCore;
using Scalable.Core.Infrastructure;

namespace Scalable.Stock
{
    public class StockContext : DbContextBase
    {
        public DbSet<Product> Products { get; set; }

        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {
        }
    }
}