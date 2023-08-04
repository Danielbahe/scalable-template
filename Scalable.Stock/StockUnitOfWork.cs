using Scalable.Core.Application;
using Scalable.Core.Infrastructure;
using Scalable.Stock.Products.Domain;
using Scalable.Stock.Products.Infrastructure;
using MediatR;

namespace Scalable.Stock
{
    public interface IStockUnitOfWork : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
    }

    public class StockUnitOfWork : UnitOfWorkBase, IStockUnitOfWork
    {
        public StockUnitOfWork(StockContext context, IMediator mediator) : base(context, mediator)
        {
        }

        public IProductRepository ProductRepository => new ProductRepository((StockContext)context);
    }
}
