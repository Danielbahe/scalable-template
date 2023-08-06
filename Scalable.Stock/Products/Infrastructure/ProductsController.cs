using Scalable.Stock.Products.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Scalable.Stock.Products.Get;

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
        public async Task<IActionResult> AddProduct(CreateProductCommand command)
        {
            logger.Debug("Procesing Create Product Request: {@request}", command);
            var result = await mediator.Send(command);

            if (result.IsFailure)
            {
                logger.Warning("Product creation unsuccessfull: {@request}", result.Error);
                return BadRequest(result);
            }
            
            logger.Information("Product created successfully");
            return StatusCode(201);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductsPaginated([FromQuery] GetAllProductsPaginatedQuery query)
        {
            logger.Debug("Procesing Get all products paginated query: {@query}", query);
            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}