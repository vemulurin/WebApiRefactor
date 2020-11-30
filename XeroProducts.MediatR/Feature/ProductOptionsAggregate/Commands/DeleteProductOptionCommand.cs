using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;
using System.Linq;

namespace XeroProducts.MediatR.Feature.ProductOptionsAggregate.Commands
{
    public class DeleteProductOptionCommand : IRequest<Guid>
    {
        public Guid ProductId { get; }
        public Guid ProductOptionId { get; }

        /// <summary>
        /// Constructor command to delete product.
        /// </summary>
        /// <param name="model" cref="Product">The object of product class</param>
        public DeleteProductOptionCommand(Guid productId, Guid productOptionId)
        {
            ProductId = productId;
            ProductOptionId = productOptionId;
        }
    }

    /// <summary>
    /// Product handler <c>DeleteProductCommandHandler</c> class.
    /// Contains delegate to excute the command to delete <c>Product</c>.
    /// </summary>
    public class DeleteProductOptionCommandHandler : IRequestHandler<DeleteProductOptionCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the delete command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public DeleteProductOptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute query to delete product.
        /// </summary>
        /// <param name="request" cref="DeleteProductOptionCommand">The object of DeleteProductCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public async Task<Guid> Handle(DeleteProductOptionCommand request, CancellationToken cancellationToken)
        {
            var productOptionList = await _unitOfWork.ProductOptionRepository.GetAllAsync();
            var productOption = productOptionList
                                            .Where(p => p.ProductId.Equals(request.ProductId) &&
                                                        p.Id.Equals(request.ProductOptionId))
                                            .FirstOrDefault();
            await _unitOfWork.ProductOptionRepository.DeleteAsync(productOption);
            await _unitOfWork.ProductOptionRepository.SaveAsync();
            return await Task.FromResult(productOption.Id);
        }
    }
}

