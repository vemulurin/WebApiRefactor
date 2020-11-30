﻿using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductOptionsAggregate.Commands
{
    public class UpdateProductOptionCommand : IRequest<bool>
    {
        public ProductOption ProductOption { get; }
        /// <summary>
        /// Constructor command to update product.
        /// </summary>
        /// <param name="model" cref="Product">The object of product class</param>
        /// /// <param name="id" </param>
        public UpdateProductOptionCommand(ProductOption model)
        {
            ProductOption = model;
        }
    }

    /// <summary>
    /// Product handler <c>UpdateProductCommandHandler</c> class.
    /// Contains delegate to excute the command to update <c>Product</c>.
    /// </summary>
    public class UpdateProductOptionCommandHandler : IRequestHandler<UpdateProductOptionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the update command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public UpdateProductOptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute query to update product.
        /// </summary>
        /// <param name="request" cref="UpdateProductOptionCommand">The object of UpdateProductCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public async Task<bool> Handle(UpdateProductOptionCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductOptionRepository.UpdateAsync(request.ProductOption);
            await _unitOfWork.ProductOptionRepository.SaveAsync();
            return await Task.FromResult(true);
        }
    }
}