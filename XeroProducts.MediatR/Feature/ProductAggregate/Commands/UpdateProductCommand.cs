using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Commands
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public Product Product { get; }
        /// <summary>
        /// Constructor command to update product.
        /// </summary>
        /// <param name="model" cref="Product">The object of product class</param>
        /// /// <param name="id" </param>
        public UpdateProductCommand(Product model)
        {
            Product = model;
        }
    }

    /// <summary>
    /// Product handler <c>UpdateProductCommandHandler</c> class.
    /// Contains delegate to excute the command to update <c>Product</c>.
    /// </summary>
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the update command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute query to update product.
        /// </summary>
        /// <param name="request" cref="UpdateProductCommand">The object of UpdateProductCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductRepository.UpdateAsync(request.Product);
            await _unitOfWork.ProductRepository.SaveAsync();
            return await Task.FromResult(true);
        }
    }
}
