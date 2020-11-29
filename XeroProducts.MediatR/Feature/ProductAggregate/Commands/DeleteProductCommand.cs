using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; }

        /// <summary>
        /// Constructor command to delete product.
        /// </summary>
        /// <param name="model" cref="Product">The object of product class</param>
        public DeleteProductCommand(Guid id)
        {
            Id = id;

        }
    }

    /// <summary>
    /// Product handler <c>DeleteProductCommandHandler</c> class.
    /// Contains delegate to excute the command to delete <c>Product</c>.
    /// </summary>
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the delete command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute query to delete product.
        /// </summary>
        /// <param name="request" cref="DeleteProductCommand">The object of DeleteProductCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.ProductRepository.DeleteAsync((_unitOfWork.ProductRepository.GetByKey(request.Id)));
            _unitOfWork.ProductRepository.SaveAsync();
            return Task.FromResult(true);
        }
    }
}

