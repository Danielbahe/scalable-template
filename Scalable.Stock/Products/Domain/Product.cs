using CSharpFunctionalExtensions;
using Scalable.Core.Domain;
using Scalable.Core.ResultHelpers;
using Scalable.Stock.Products.Create;

namespace Scalable.Stock.Products.Domain
{
    public class Product : AggregateRoot
    {
        private const int miniumNameLength = 2;
        public string Name { get; private set; } = string.Empty;

        private Product()
        {
        }

        public static Result<Product> Create(CreateProductCommand command)
        {
            var product = new Product();

            var result = Constraints
                .AddResult(product.SetName(command.Name))
                .CombineIn(product);

            if(result.IsSuccess) product.AddDomainEvent(new CreatedProductEvent(product));

            return result;
        }

        private Result<Product> SetName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < miniumNameLength) return Result.Failure<Product>("Invalid name");

            Name = name;

            return Result.Success(this);
        }
    }
}