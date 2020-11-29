﻿using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Data.UnitOfWork;

namespace XeroProducts.MediatR.Feature.ProductAggregate.Commands
{
    public class CreateProductCommand : IRequest<bool>
    {
        public Product Product { get; }

        /// <summary>
        /// Constructor command to create a new product.
        /// </summary>
        /// <param name="model" cref="Product">The object of product class</param>
        public CreateProductCommand(Product model)
        {
            Product = model;

        }
    }
    /// <summary>
    /// Product handler <c>CreateProductCommandHandler</c> class.
    /// Contains delegate to excute the command to create a new <c>Product</c>.
    /// </summary>
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to handle the create command.
        /// </summary>
        /// <param name="unitOfWork" cref="IUnitOfWork">The generic repository reference for Product model.</param>
        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Delegate to execute create product.
        /// </summary>
        /// <param name="request" cref="CreateMatchCommand">The object of CreateMatchCommand class.</param>
        /// <param name="cancellationToken" cref="CancellationToken" >The cancellation token.</param>
        /// <returns>Returns a unit value.</returns>
        public Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _unitOfWork.ProductRepository.AddAsync(request.Product);
            _unitOfWork.ProductRepository.SaveAsync();
            return Task.FromResult(true);
        }
    }
}
