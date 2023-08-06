using MediatR;
using Scalable.Stock.Products.Domain;
using Serilog;

namespace Scalable.Stock.Products.Get
{
    public record GetAllProductsPaginatedQuery(int PageSize = 5, int Page = 0) : IRequest<IEnumerable<Product>>;
    internal class GetAllPaginated : IRequestHandler<GetAllProductsPaginatedQuery, IEnumerable<Product>>
    {
        private readonly ILogger logger;
        private readonly IStockUnitOfWork unitOfWork;

        public GetAllPaginated(ILogger logger, IStockUnitOfWork unitOfWork)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        public async Task<IEnumerable<Product>> Handle(GetAllProductsPaginatedQuery query, CancellationToken cancellationToken)
        {
            logger.Information("Retrieving {pageSize} products from page {Page}", query.PageSize, query.Page);

            var products = await unitOfWork.ProductRepository.GetAllProductsPaginated(query.PageSize, query.Page);
            logger.Information("Retrieved {Count} products", products.Count());

            return products;
        }
    }
}
