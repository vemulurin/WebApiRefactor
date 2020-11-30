using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using XeroProducts.Data.Models;
using System.Threading.Tasks;
using XeroProducts.Data.UnitOfWork;
using FluentAssertions;
using XeroProducts.Data.Repository;
using XeroProducts.MediatR.Feature.ProductOptionsAggregate.Queries;
using System.Linq;
using XeroProducts.MediatR.Feature.ProductOptionsAggregate.Commands;

namespace XeroProducts.Tests.Fixtures
{
    public class ProductOptionTest
    {
        readonly Mock<IUnitOfWork> _unitOfWorkMock;
        readonly Mock<IRepository<ProductOption>> _productOptionRepositoryMock;
        readonly Mock<IMediator> _mediatorMock;

        public ProductOptionTest()
        {
            _productOptionRepositoryMock = new Mock<IRepository<ProductOption>>();
            _mediatorMock = new Mock<IMediator>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.SetupGet(s => s.ProductOptionRepository).Returns(_productOptionRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProductOptionsByProductIdTest()
        {
            // Arrange     
            var productId = new Guid("420BB34C-EF73-444F-B77F-4B31E8E2A0AF");
            IEnumerable<ProductOption> productOptionList = GetProductOptionMockData().Where(p => p.ProductId.Equals(productId)).ToList();
            ProductOptions productOptions = new ProductOptions
            {
                Items = productOptionList
            };

            _productOptionRepositoryMock.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(productOptionList));
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductOptionsByProductIdQuery>(), default));

            GetAllProductOptionsByProductIdQuery query = new GetAllProductOptionsByProductIdQuery(productId);
            GetAllProductOptionsByProductIdHandler handler = new GetAllProductOptionsByProductIdHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert           
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductOptions>();
            result.Should().BeEquivalentTo(productOptions);
            productOptions.Items.Should().HaveCount(result.Items.Count());

        }

        [Fact]
        public async Task GetProductOptionByProductIdAndOptionIdTest()
        {
            // Arrange          
            var productId = new Guid("A2BAD5C4-61F3-40D3-96A3-34638E424CA7");
            var productOptionId = new Guid("A9B2E6D2-CE5E-4E5C-97E0-9D723403EB68");
            IEnumerable<ProductOption> productOptionList = GetProductOptionMockData();
            ProductOption productOption = GetProductOptionMockData()
                                                .Where(p => p.Id.Equals(productOptionId) &&
                                                            p.ProductId.Equals(productId))
                                                .FirstOrDefault();

            _productOptionRepositoryMock.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(productOptionList));
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductOptionsByOptionIdQuery>(), default));

            GetProductOptionsByOptionIdQuery query = new GetProductOptionsByOptionIdQuery(productId, productOptionId);
            GetAllProductOptionsByOptionIdHandler handler = new GetAllProductOptionsByOptionIdHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert           
            result.Should().NotBeNull();
            result.Should().BeOfType<ProductOption>();
            result.Should().BeEquivalentTo(productOption);
        }

        [Fact]
        public async Task CreateProductOptionTest()
        {
            // Arrange          
            ProductOption productOption = new ProductOption()
            {
                Id = new Guid("DE4D4554-E1DA-43F2-BB0C-C56636A428D2"),
                Name = "iPhone 12",
                Description = "iPhone 12 Mobile",
                ProductId = new Guid("A2BAD5C4-61F3-40D3-96A3-34638E424CA7")
            };

            _productOptionRepositoryMock.Setup(a => a.AddAsync(productOption)).Returns(Task.FromResult(productOption));
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductOptionCommand>(), default));

            CreateProductOptionCommand command = new CreateProductOptionCommand(productOption);
            CreateProductOptionCommandHandler handler = new CreateProductOptionCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            // Arrange 
            IEnumerable<ProductOption> productOptionList = GetProductOptionMockData();
            ProductOption productOption = productOptionList.FirstOrDefault();
            productOption.Description = "Product option description has been modified...";
            productOption.Name = "Product option name has been modified...";

            _productOptionRepositoryMock.Setup(a => a.UpdateAsync(productOption)).Returns(Task.FromResult(productOption));
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductOptionCommand>(), default));

            UpdateProductOptionCommand command = new UpdateProductOptionCommand(productOption);
            UpdateProductOptionCommandHandler handler = new UpdateProductOptionCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProductTest()
        {
            // Arrange 
            IEnumerable<ProductOption> productOptionList = GetProductOptionMockData();
            ProductOption productOption = GetProductOptionMockData().FirstOrDefault();

            _productOptionRepositoryMock.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(productOptionList));
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductOptionCommand>(), default));

            DeleteProductOptionCommand command = new DeleteProductOptionCommand(productOption.ProductId, productOption.Id);
            DeleteProductOptionCommandHandler handler = new DeleteProductOptionCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().NotBeEmpty();
            result.Should().Be(productOption.Id);
            result.Should().NotBe(Guid.NewGuid());
        }

        private IEnumerable<ProductOption> GetProductOptionMockData()
        {
            var productOptions = new List<ProductOption>()
            {
                new ProductOption()
                {
                    Id = new Guid("724F8B9C-4733-4FA2-AC4E-DA2096BD3A93"),
                    Name = "Samsung Galaxy X",
                    Description = "Samsung Galaxy Z Description",
                    ProductId = new Guid("420BB34C-EF73-444F-B77F-4B31E8E2A0AF")
                },
                 new ProductOption()
                {
                    Id = new Guid("2B6CEEB8-125B-4449-B7DF-9873A9A05AC3"),
                    Name = "Samsung Galaxy Y",
                    Description = "Samsung Galaxy Y Description",
                    ProductId = new Guid("420BB34C-EF73-444F-B77F-4B31E8E2A0AF")
                },
                  new ProductOption()
                {
                    Id = new Guid("DC1D4863-59CC-485C-B694-0C1548CEF181"),
                    Name = "Samsung Galaxy Z",
                    Description = "Samsung Galaxy Z Description",
                    ProductId = new Guid("420BB34C-EF73-444F-B77F-4B31E8E2A0AF")
                },
                   new ProductOption()
                {
                    Id = new Guid("A9B2E6D2-CE5E-4E5C-97E0-9D723403EB68"),
                    Name = "iPhone X",
                    Description = "iPhone X Mobile",
                    ProductId = new Guid("A2BAD5C4-61F3-40D3-96A3-34638E424CA7")
                }
            };

            return productOptions;
        }
    }
}
