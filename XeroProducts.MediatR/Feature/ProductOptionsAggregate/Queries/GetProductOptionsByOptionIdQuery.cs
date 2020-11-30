using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductOptionsAggregate.Queries
{
    /// <summary>
    /// The class <c>GetAllProductOptionsByOptionIdQuery</c> class.
    /// Contains the query to get the <c>ProductOptions</c>.
    /// </summary>
    public class GetProductOptionsByOptionIdQuery : IRequest<ProductOption>
    {
        public Guid ProductId { get; set; }
        public Guid OptionId { get; set; }

        public GetProductOptionsByOptionIdQuery(Guid productId, Guid optionId)
        {
            ProductId = productId;
            OptionId = optionId;
        }
    }
    /// Product handler <c>GetAllProductOptionsHandler</c> class.
    /// Contains delegate to excute the query.
    /// </summary>
    public class GetAllProductOptionsByOptionIdHandler : IRequestHandler<GetProductOptionsByOptionIdQuery, ProductOption>
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the query.
        /// </summary>
        /// <param name="unitofWork" cref="IUnitOfWork">The generic repository reference for ProductOptions model.</param>
        public GetAllProductOptionsByOptionIdHandler(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// Delegate to execute query to fetch list of ProductOptions.
        /// </summary>
        /// <param name="query" cref="GetProductOptionsByOptionIdQuery">The object of GetProductOptionsByOptionIdQuery class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns null or the ProductOptions list.</returns>
        public async Task<ProductOption> Handle(GetProductOptionsByOptionIdQuery query, CancellationToken cancellationToken)
        {
            var productOptionsList = await _unitOfWork.ProductOptionRepository.GetAllAsync();

            if (productOptionsList == null)
            {
                return null;
            }

            ProductOption productOption = productOptionsList
                                                        .Where(p => p.ProductId.Equals(query.ProductId) &&
                                                                    p.Id.Equals(query.OptionId))
                                                        .FirstOrDefault();
            return productOption;
        }

    }
}
