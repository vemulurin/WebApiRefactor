using MediatR;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductOptionsAggregate.Commands
{
    public class CreateProductOptionCommand : IRequest<bool>
    {
        public ProductOption ProductOption { get; }

        /// <summary>
        /// Constructor command to create a new ProductOption.
        /// </summary>
        /// <param name="model" cref="Product">The object of ProductOption class</param>
        public CreateProductOptionCommand(ProductOption model)
        {
            ProductOption = model;

        }
    }
    /// <summary>
    /// Product handler <c>CreateProductOptionCommandHandler</c> class.
    /// Contains delegate to excute the command to create a new <c>ProductOption</c>.
    /// </summary>
    public class CreateProductOptionCommandHandler : IRequestHandler<CreateProductOptionCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the create command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for ProductOption model.</param>
        public CreateProductOptionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute create product option.
        /// </summary>
        /// <param name="request" cref="CreateProductOptionCommand">The object of CreateProductOptionCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public async Task<bool> Handle(CreateProductOptionCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.ProductOptionRepository.AddAsync(request.ProductOption);
            await _unitOfWork.ProductOptionRepository.SaveAsync();
            return await Task.FromResult(true);
        }
    }
}
