using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.MediatR.Feature.ProductOptionsAggregate.Commands;
using XeroProducts.MediatR.Feature.ProductOptionsAggregate.Queries;

namespace XeroProducts.Controllers
{
    /// <summary>
    /// Controller for product options endpoints
    /// </summary>

    [Route("api/Products/{productId}/options")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
        private readonly ILogger<ProductOptionsController> _logger;
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor of <c>ProductOptionsController</c> class.
        /// </summary>
        /// <param name="logger" cref="ILogger">Instance reference of <c>ILogger</c></param>
        /// <param name="mediator" cref="IMediator">Instance reference of <c>IMediator</c></param> 
        public ProductOptionsController(ILogger<ProductOptionsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        ///  GET: api/Products/{productId}/options
        /// </summary>
        /// <returns>return the list of <c>ProductOptions</c></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductOptionsByProductId(Guid productId)
        {
            _logger.LogInformation("Get ProductOptions list");
            var query = new GetAllProductOptionsByProductIdQuery(productId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  GET: api/Products/{productId}/options/{productOptionId}
        /// </summary>
        /// <returns>return the <c>ProductOption</c> by OptionId for a Product</returns>
        [HttpGet("{productOptionId}")]
        public async Task<IActionResult> GetProductOptionsByOptionId(Guid productId, Guid productOptionId)
        {
            _logger.LogInformation("Get ProductOption by OptionId for a product");
            var query = new GetProductOptionsByOptionIdQuery(productId, productOptionId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  GET: api/products/{productId}/options
        /// </summary>
        /// <param name="productOption">The  <c>ProductOption</c> class</param>
        /// <returns>Add new ProductOption for a Product</returns>

        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] ProductOption productOption)
        {
            _logger.LogInformation($"Add new ProductOption to {productOption?.Id}");
            var query = new CreateProductOptionCommand(productOption);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  PUT: api/Products/{productId}/options/{optionId}
        ///</summary>
        /// <param name="productId">The productId of the <c>Product</c> class</param>
        /// <param name="productOption">The  <c>ProductOption</c> class</param>
        /// <returns>return Action Result</returns>

        [HttpPut("{productOptionId}")]
        public async Task<IActionResult> Put(Guid productId, [FromBody] ProductOption productOption)
        {
            if (productId != productOption.ProductId)
            {
                return BadRequest();
            }

            var command = new UpdateProductOptionCommand(productOption);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        ///  DELETE: api/Products/{productId}/options/{optionId}
        /// </summary>
        /// <param name="productId">The productId of the <c>Product</c> class</param>
        /// <param name="productOptionId">The productOptionId of the <c>ProductOption</c> class.</param>

        [HttpDelete("{productOptionId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId, Guid productOptionId)
        {
            if (string.IsNullOrEmpty(productOptionId.ToString()) && string.IsNullOrEmpty(productId.ToString()))
            {
                return BadRequest();
            }

            _logger.LogInformation($"Delete ProductOption: {productOptionId}");
            var query = new DeleteProductOptionCommand(productId, productOptionId);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
