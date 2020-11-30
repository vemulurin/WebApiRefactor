
using XeroProducts.Data.Repository;
using XeroProducts.Data.Models;

namespace XeroProducts.Data.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<ProductOption> ProductOptionRepository { get; }
        IRepository<ErrorDetails> ErrorDetailsRepository { get; }
    }
}
