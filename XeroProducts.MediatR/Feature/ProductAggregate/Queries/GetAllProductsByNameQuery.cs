using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Queries
{
    public class GetAllProductsByNameQuery : IRequest<Products>
    {
        public string ProductName { get; set; }

        public GetAllProductsByNameQuery(string productName)
        {
            ProductName = productName;

        }
    }

    /// <summary>
    /// Product handler <c>GetAllProductsByNameHandler</c> class.
    /// Contains delegate to excute the query.
    /// </summary>
    public class GetAllProductsByNameHandler : IRequestHandler<GetAllProductsByNameQuery, Products>
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the query.
        /// </summary>
        /// <param name="unitofWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public GetAllProductsByNameHandler(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// Delegate to execute query to fetch list of Product.
        /// </summary>
        /// <param name="query" cref="GetAllProductsByNameQuery">The object of GetAllProductsByNameQuery class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns null or the list of Product.</returns>
        public async Task<Products> Handle(GetAllProductsByNameQuery query, CancellationToken cancellationToken)
        {
            var productList = await _unitOfWork.ProductRepository.GetAllAsync();

            if (productList == null)
            {
                return null;
            }
            Products products = new Products()
            {
                Items = productList.Where(p => p.Name.Contains(query.ProductName)).ToList()
            };

            return products;
        }
    }
}
