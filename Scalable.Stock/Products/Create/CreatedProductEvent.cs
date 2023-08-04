using Scalable.Core.Domain;
using Scalable.Stock.Products.Domain;

namespace Scalable.Stock.Products.Create
{
    public record CreatedProductEvent : DomainEvent
    {
        public Product Product { get; }

        public CreatedProductEvent(Product Product) : base(Product.Id)
        {
            this.Product = Product;
        }
    }
}
