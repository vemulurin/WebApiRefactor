using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
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
            _logger.LogInformation("Get Product List");
            var query = new GetAllProductsQuery();
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        //// GET: api/products/name?name={name}
        //[HttpGet("name")]
        //public async Task<Product>
        //    Get([FromQuery][Required(ErrorMessage = "product name cannot be empty",
        //    AllowEmptyStrings =false
        //    )]string name)
        //{
        //    _logger.LogInformation($"received request for api/products/ with parameters: {name}");
        //    var product = await _productService.GetProductByName(name);
        //    return product;
        //}
        //// GET: api/Products/5
        //[HttpGet("{id}")]
        //public async Task<Product> GetProducts(Guid id)
        //{
        //    _logger.LogInformation($"get product for {id}");
        //    var products = await _productService.GetProducts(id);
        //    return products;
        //}

        //// PUT: api/Products/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProducts(Guid id, [FromBody] Product product)
        //{
        //    _logger.LogInformation($"add product with id {id}");
        //    return Ok(await _productService.PutProduct(id, product));
        //}

        //// POST: api/Products
        //[HttpPost]
        //public async Task<Product> PostProducts([FromBody] Product product)
        //{
        //    _logger.LogInformation($"edit product for {product.Id}");
        //    return await _productService.PostProduct(product);
        //}

        //// DELETE: api/Products/5
        //[HttpDelete("{id}")]
        //public async Task<Guid> DeleteProduct(Guid id)
        //{
        //    _logger.LogInformation($"delete product with id {id}");
        //    return await _productService.DeleteProduct(id);
        //}
    }
}
