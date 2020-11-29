using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.MediatR.Feature.ProductAggregate.Commands;
using XeroProducts.MediatR.Feature.ProductAggregate.Queries;

namespace XeroProducts.Controllers
{
    /// <summary>
    /// Controller for product endpoints
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        /// <summary>
        /// Constructor of <c>productsController</c> class.
        /// </summary>
        /// <param name="logger" cref="ILogger">Instance reference of <c>ILogger</c></param>
        /// <param name="mediator" cref="IMediator">Instance reference of <c>IMediator</c></param>
        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;

        }
       
        /// <summary>
        ///  GET: api/Products
        /// </summary>
        /// <returns>return the list of <c>Products</c></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            _logger.LogInformation("Get Product list");
            var query = new GetAllProductsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
        /// <summary>
        ///  GET: api/products/name?name={name}
        /// </summary>
        /// <returns>return the products filtered by name</returns>
        [HttpGet("name")]
        public async Task<IActionResult> GetProducts(string name)
        {
            _logger.LogInformation("Get Product list by name");
            var query = new GetAllProductsByNameQuery(name);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  GET: api/products/name?name={id}
        /// </summary>
        /// <returns>return the products filtered by id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(Guid id)
        {
            _logger.LogInformation("Get Product list by id");
            var query = new GetAllProductsByIdQuery(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        ///  GET: api/product
        /// </summary>
        /// <returns>Add new product</returns>
        [HttpPost]
        public async Task<IActionResult> PostProducts([FromBody] Product product)
        {
            _logger.LogInformation("Add new Product");
            var query = new CreateProductCommand(product);
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// The <c>HttpPut</c> call update a <c>Product</c>.
        /// </summary>
        /// <param name="id">The id of the <c>Product</c> class</param>
        /// <param name="model" cref="Product">The object of <c>Product</c> class.</param>
        /// <returns>return Action Result</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Product model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var command = new UpdateProductCommand(model);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        /// <summary>
        ///  DELETE: api/Products/{id}
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            _logger.LogInformation($"Delete Product: {id}");
            var query = new DeleteProductCommand(id);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
      
    }
}
