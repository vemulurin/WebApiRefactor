using System;
using System.Threading.Tasks;
using XeroProducts.Data.Models;

namespace XeroProducts.Services
{
    public interface IProductService
    {
        Task<Product> GetProducts(Guid id);
        Task<Product> GetProductByName(string name);
        Task<Product> PostProduct(Product product);
        Task<Product> PutProduct(Guid id, Product product);
        bool ProductExists(Guid id);
        Task<Guid> DeleteProduct(Guid id);
        Task<Products> GetProducts();
    }
}
