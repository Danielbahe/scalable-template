using CSharpFunctionalExtensions;
using Scalable.Stock.Products.Domain;
using MediatR;
using Serilog;

namespace Scalable.Stock.Products.Create
{
    public record CreateProductCommand(string Name) : IRequest<Result<Product>>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private readonly ILogger logger;
        public IStockUnitOfWork UnitOfWork { get; }

        public CreateProductCommandHandler(IStockUnitOfWork unitOfWork, ILogger logger)
        {
            UnitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = Product.Create(request);
            if (product.IsFailure)
            {
                logger.Warning("Error creating new Product: {@Error}", product.Error);
                return product;
            }

            await UnitOfWork.ProductRepository.AddAsync(product.Value);
            await UnitOfWork.SaveChangesAsync();

            logger.Debug("Product created: {@Value}", product.Value);

            return product;
        }
    }
}
