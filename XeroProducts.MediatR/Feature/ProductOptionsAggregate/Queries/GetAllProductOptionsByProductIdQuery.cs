using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;
using System.Linq;

namespace XeroProducts.MediatR.Feature.ProductOptionsAggregate.Queries
{
    /// <summary>
    /// The class <c>GetAllProductOptionsByProductIdQuery</c> class.
    /// Contains the query to get the <c>ProductOptions</c>.
    /// </summary>
    public class GetAllProductOptionsByProductIdQuery : IRequest<ProductOptions>
    {
        public Guid ProductId { get; set; }
        public GetAllProductOptionsByProductIdQuery(Guid productId)
        {
            ProductId = productId;
        }
    }

    /// <summary>
    /// Product handler <c>GetAllProductOptionsHandler</c> class.
    /// Contains delegate to excute the query.
    /// </summary>
    public class GetAllProductOptionsByProductIdHandler : IRequestHandler<GetAllProductOptionsByProductIdQuery, ProductOptions>
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the query.
        /// </summary>
        /// <param name="unitofWork" cref="IUnitOfWork">The generic repository reference for ProductOptions model.</param>
        public GetAllProductOptionsByProductIdHandler(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// Delegate to execute query to fetch list of ProductOptions for a Product.
        /// </summary>
        /// <param name="query" cref="GetAllProductOptionsByProductIdQuery">The object of GetAllProductOptionsByProductIdQuery class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns null or the ProductOptions list.</returns>
        public async Task<ProductOptions> Handle(GetAllProductOptionsByProductIdQuery query, CancellationToken cancellationToken)
        {
            var productOptionsList = await _unitOfWork.ProductOptionRepository.GetAllAsync();

            if (productOptionsList == null)
            {
                return null;
            }

            ProductOptions productOptions = new ProductOptions
            {
                Items = productOptionsList.Where(p => p.ProductId.Equals(query.ProductId)).ToList()
            };
            return productOptions;
        }
    }
}
