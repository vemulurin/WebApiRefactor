
using XeroProducts.Data.Repository;
using XeroProducts.Data.Models;

namespace XeroProducts.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Products> ProductsRepository { get; }
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductOptions> ProductOptionsRepository { get; }
        IRepository<ErrorDetails> ErrorDetailsRepository { get; }
    }
}
