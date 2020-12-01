using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Queries
{
    public class GetAllProductsByIdQuery : IRequest<Product>
    {
        public Guid ProductId { get; set; }

        public GetAllProductsByIdQuery(Guid productId)
        {
           ProductId = productId;
        }
    }

    /// <summary>
    /// Product handler <c>GetAllProductsByIdHandler</c> class.
    /// Contains delegate to excute the query.
    /// </summary>
    public class GetAllProductsByIdHandler : IRequestHandler<GetAllProductsByIdQuery, Product>
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the query.
        /// </summary>
        /// <param name="unitofWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public GetAllProductsByIdHandler(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// Delegate to execute query to fetch list of Product.
        /// </summary>
        /// <param name="query" cref="GetAllProductOptionsByIdQuery">The object of GetAllProductsByIdQuery class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns null or the list of Product.</returns>
        public async Task<Product> Handle(GetAllProductsByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByKeyAsync(query.ProductId);

            if (product == null)
            {
                return null;
            }
            
            return product;
        }
    }
}
