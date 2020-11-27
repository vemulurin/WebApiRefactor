using System;
using System.Threading.Tasks;
using XeroProducts.Models;
using XeroProducts.Repositories;

namespace XeroProducts.Services
{
    /// <summary>
    /// Product  service ,implements business logic for product  actions
    /// </summary
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository ProductRepository)
        {
            _productRepository = ProductRepository;
        }

        public async Task<Products> GetProducts()
        {
            return await _productRepository.GetProducts();
        }

        public Task<Product> GetProducts(Guid id)
        {
            if (!ProductExists(id))
            {
                //this message is logged in the logs folder 
                throw new Exception($"Product Id {id} does not exist");
            }
            else 
            {
                return _productRepository.GetProducts(id);
            }
            
        }

        public Task<Product> GetProductByName(string name)
        {
            var product = _productRepository.GetProductByName(name);

            if (product.Result.Id==null)
            {
                throw new Exception($"Product with Name {name} does not exist");
            }
            else
            {
                return product;
            }
        }

        public Task<Product> PostProduct(Product product)
        {
            if (ProductExists(product.Id))
            {
                throw new Exception($"Product Id {product.Id} already  exists");
            }
            else
            {
                product.Id = new Guid();
                return _productRepository.PostProduct(product);
            }
           
        }

        public bool ProductExists(Guid id)
        {
            return _productRepository.ProductExists(id);
        }

        public Task<Product> PutProduct(Guid id, Product product)
        {
            Product refproduct = new Product();
            if (!ProductExists(id))
            {
                throw new Exception($"Product Id {id} does  not exist");
            }
           
            else
            {
                return _productRepository.PutProduct(id, product);
            }
            
        }

        public Task<Guid> DeleteProduct(Guid id)
        {
            if (ProductExists(id))
            {
                return _productRepository.DeleteProduct(id);
            }
            else
            {
                throw new Exception($"Product Id {id} does  not exist");
            }
        }
    }
}
