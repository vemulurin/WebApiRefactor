using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using XeroProducts.Data.Models;

namespace XeroProducts.Controllers
{
    /// <summary>
    /// Controller for product options endpoints
    /// </summary>

    [Route("api/Products/{id}/options")]
    [ApiController]
    public class ProductOptionsController : ControllerBase
    {
    //    private readonly ILogger<ProductOptionsController> _logger;
    //    public ProductOptionsController(IProductOptionsService productOptionsService, ILogger<ProductOptionsController> logger)
    //    {
    //        _productOptionsService = productOptionsService;
    //        _logger = logger;
    //    }

    //    // GET: api/Products/5/options
    //    [HttpGet]
    //    public async Task<ProductOptions> GetProductOptionsByProductId(Guid id)
    //    {
    //        _logger.LogInformation($"get product options with product id {id}");
    //        var productOptions = await _productOptionsService.GetProductOptions(id);
    //        return productOptions;
    //    }

    //    // GET: api/Products/5/options
    //    [HttpGet("{optionId}")]
    //    public async Task<ProductOption> GetProductOptionsByOptionId(Guid id,Guid optionId)
    //    {
    //        _logger.LogInformation($"get product options with product id {id} and option is {optionId}");
    //        var productOption = await _productOptionsService.GetProductOptionsByOptionId(id, optionId);
    //        return productOption;
    //    }


    //    // PUT: api/ProductOptions/5
    //    [HttpPut("{optionId}")]
    //    public async Task<ProductOption> PutProductOptions(Guid id, Guid optionId,[FromBody] ProductOption productOptions)
    //    {
    //        _logger.LogInformation($"edit product options with product id {id} and option is {optionId}");
    //        return await _productOptionsService.PutProductOptions(id, optionId, productOptions);
    //    }

    //    // POST: api/ProductOptions
    //    [HttpPost]
    //    public async Task<ProductOption> PostProductOptions(Guid id, [FromBody]  ProductOption productOption)
    //    {
    //        _logger.LogInformation($"add product options with product id {id} ");
    //        return await _productOptionsService.PostProductOptions(id, productOption);
    //    }

    //    // DELETE: api/ProductOptions/5
    //    [HttpDelete("{optionId}")]
    //    public async Task<Guid> DeleteProductOption(Guid optionId)
    //    {
    //        _logger.LogInformation($"delete product options with  option id {optionId}");
    //        return await _productOptionsService.DeleteProductOptions(optionId);
    //    }
   }
}
