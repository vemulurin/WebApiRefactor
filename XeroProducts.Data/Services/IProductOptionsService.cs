using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroProducts.Data.Models;

namespace XeroProducts.Services
{
    public interface IProductOptionsService
    {
        Task<Guid> DeleteProductOptions(Guid id);
        Task<IEnumerable<ProductOption>> GetProductOptions();
        Task<ProductOption> GetProductOptionsByOptionId(Guid id, Guid OptionId);
        Task<ProductOptions> GetProductOptions(Guid id);
        Task<ProductOption> PostProductOptions(Guid id, ProductOption productOption);
        Task<ProductOption> PutProductOptions(Guid id,Guid optionId, ProductOption productOption);
        bool ProductOptionsExists(Guid id);
    }
}
