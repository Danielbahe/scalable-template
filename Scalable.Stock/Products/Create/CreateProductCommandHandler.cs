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
        private readonly IStockUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IStockUnitOfWork unitOfWork, ILogger logger)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = Product.Create(command);
            if (product.IsFailure)
            {
                logger.Warning("Error creating new Product: {@Error}", product.Error);
                return product;
            }

            await unitOfWork.ProductRepository.AddAsync(product.Value);
            await unitOfWork.SaveChangesAsync();

            logger.Debug("Product created: {@Value}", product.Value);

            return product;
        }
    }
}
