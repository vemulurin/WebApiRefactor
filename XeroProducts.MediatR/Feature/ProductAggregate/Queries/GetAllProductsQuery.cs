﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Queries
{
    /// <summary>
    /// The class <c>GetAllProductsQuery</c> class.
    /// Contains the query to get the list of <c>Match</c>.
    /// </summary>
    public class GetAllProductsQuery : IRequest<IEnumerable<Products>> { }

    /// <summary>
    /// Match handler <c>GetAllProductsHandler</c> class.
    /// Contains delegate to excute the query.
    /// </summary>
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Products>>
    {

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the query.
        /// </summary>
        /// <param name="unitofWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public GetAllProductsHandler(IUnitOfWork unitofWork)
        {
            _unitOfWork = unitofWork;
        }

        /// <summary>
        /// Delegate to execute query to fetch list of products.
        /// </summary>
        /// <param name="query" cref="GetAllProductsQuery">The object of GetAllProductsQuery class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns null or the Product list.</returns>
        public async Task<IEnumerable<Products>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            var productList = await _unitOfWork.ProductsRepository.GetAllAsync();

            if (productList == null)
            {
                return null;
            }

            return productList;
        }
    }
}
