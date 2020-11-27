using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.Repositories;

namespace XeroProducts.Services
{
    /// <summary>
    /// Product option service ,implements business logic for products option actions
    /// </summary>
    public class ProductOptionService : IProductOptionsService
    {
        private readonly IProductOptionsRepository _productOptions;
        private readonly IProductRepository _product;

        public ProductOptionService(IProductOptionsRepository productOptions, IProductRepository product)
        {
            _productOptions = productOptions;
            _product = product;
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptions()
        {
            return await _productOptions.GetProductOptions();
        }

        public Task<ProductOptions> GetProductOptions(Guid productId)
        {
            if (!_product.ProductExists(productId))
            {
                throw new Exception($"Product {productId} does  not exist");
            }
            else
            {
                return _productOptions.GetProductOptions(productId);
            }

        }

        public async Task<ProductOption> GetProductOptionsByOptionId(Guid id, Guid OptionId)
        {
            if (!_product.ProductExists(id))
            {
                throw new Exception($"Product Id {id} does  not exist");
            }
            else if (!_productOptions.ProductOptionsExists(OptionId))
            {
                throw new Exception($"Product Option {OptionId} does  not exist");
            }
            else
            {
                return await  _productOptions.GetProductOptionsByOptionId(id, OptionId);
            }
        }

        public async Task<ProductOption> PostProductOptions(Guid id, ProductOption productOption)
        {
            if (!_product.ProductExists(id))
            {
                throw new Exception($"Product Id {id} does  not exist");
            }
            else if (_productOptions.ProductOptionsExists(productOption.Id))
            {
                throw new Exception($"Product Option {productOption.Id} already exists");
            }
            else
            {
                productOption.ProductId = id;
                productOption.Id = new Guid();
                return await _productOptions.PostProductOptions(productOption);
            }
        }

        public bool ProductOptionsExists(Guid id)
        {
            return _productOptions.ProductOptionsExists(id);
        }

        public async Task<ProductOption> PutProductOptions(Guid id,Guid optionId, ProductOption productOption)
        {
            productOption.ProductId = id;
            if (!_product.ProductExists(productOption.ProductId))
            {
                throw new Exception($"Invalid Product  {productOption.ProductId} does not exist in Products");
            }
            else if (optionId!= productOption.Id) 
            {
                throw new Exception($"Invalid Product Option  {optionId} does not match URL value {productOption.Id}");
            }
            else
            {

                return await _productOptions.PutProductOptions(productOption);
            }
            
        }
        public async Task<Guid> DeleteProductOptions(Guid id)
        {
            if (!ProductOptionsExists(id))
            {
                throw new Exception($"Product Option {id} does  not exist");
            }
            else
            {
                return await _productOptions.DeleteProductOptionsByProductId(id); 
            }
        }
    }
}
