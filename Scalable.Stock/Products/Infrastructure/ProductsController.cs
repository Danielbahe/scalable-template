using Scalable.Stock.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Scalable.Stock.Products.Infrastructure
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger logger;

        public ProductsController(IMediator mediator, ILogger logger)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpPut]
        public async Task<IActionResult> AddProduct(CreateProductCommand request)
        {
            logger.Debug("Procesing Create Product Request: {@request}", request);
            var result = await mediator.Send(request);

            if (result.IsSuccess)
            {
                logger.Information("Product created successfully");
                return StatusCode(201);
            }
            else
            {
                logger.Warning("Product creation unsuccessfull: {@request}", result.Error);
                return BadRequest(result);
            }
        }
    }
}