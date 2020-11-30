using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using XeroProducts.Controllers;
using Xunit;
using XeroProducts.Data.Models;
using System.Threading.Tasks;
using XeroProducts.Data.UnitOfWork;
using FluentAssertions;
using XeroProducts.Data.Repository;
using XeroProducts.MediatR.Feature.ProductAggregate.Queries;
using System.Linq;
using XeroProducts.MediatR.Feature.ProductAggregate.Commands;

namespace XeroProducts.Tests.Fixtures
{
    public class ProductTest
    {
        readonly Mock<IUnitOfWork> _unitOfWorkMock;
        readonly Mock<IRepository<Product>> _productRepositoryMock;
        readonly Mock<IMediator> _mediatorMock;

        public ProductTest()
        {
            _productRepositoryMock = new Mock<IRepository<Product>>();
            _mediatorMock = new Mock<IMediator>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.SetupGet(s => s.ProductRepository).Returns(_productRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProductsTest()
        {
            // Arrange          
            IEnumerable<Product> productList = GetProductMockData();
            Products products = new Products
            {
                Items = productList
            };

            _productRepositoryMock.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(productList));
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), default));

            GetAllProductsQuery query = new GetAllProductsQuery();
            GetAllProductsHandler handler = new GetAllProductsHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert           
            result.Should().NotBeNull();
            result.Should().BeOfType<Products>();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async Task GetProductByProductIdTest()
        {
            // Arrange          
            var productId = new Guid("A2BAD5C4-61F3-40D3-96A3-34638E424CA7");
            Product product = GetProductMockData().Where( p => p.Id.Equals(productId)).FirstOrDefault();

            _productRepositoryMock.Setup(a => a.GetByKeyAsync(productId)).Returns(Task.FromResult(product));
            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsByIdQuery>(), default));

            GetAllProductsByIdQuery query = new GetAllProductsByIdQuery(productId);
            GetAllProductsByIdHandler handler = new GetAllProductsByIdHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert           
            result.Should().NotBeNull();
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public async Task CreateProductTest()
        {
            // Arrange          
            Product product = new Product() 
            {
                DeliveryPrice = 66,
                Description = "Product Description 6",
                Name = "Product Mock 6",
                Price = 666,
                Id = new Guid("DAF291DD-ECED-4202-A465-346C9758D1E3")
            };
            

            _productRepositoryMock.Setup(a => a.AddAsync(product)).Returns(Task.FromResult(product));
            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default));

            CreateProductCommand command = new CreateProductCommand(product);
            CreateProductCommandHandler handler = new CreateProductCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            // Arrange 
            IEnumerable<Product> productList = GetProductMockData();
            Product product = productList.FirstOrDefault();
            product.Description = "Product description has been modified...";
            product.Name = "Product name has been modified...";

            _productRepositoryMock.Setup(a => a.UpdateAsync(product)).Returns(Task.FromResult(product));
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default));

            UpdateProductCommand command = new UpdateProductCommand(product);
            UpdateProductCommandHandler handler = new UpdateProductCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProductTest()
        {
            // Arrange 
            IEnumerable<Product> productList = GetProductMockData();
            Product product = productList.FirstOrDefault();

            _productRepositoryMock.Setup(a => a.DeleteAsync(product)).Returns(Task.FromResult(product));
            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default));

            DeleteProductCommand command = new DeleteProductCommand(product.Id);
            DeleteProductCommandHandler handler = new DeleteProductCommandHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(command, default);

            // Assert           
            result.Should().BeTrue();
        }

        [Fact]
        public async Task GetProductByProductNameTest()
        {
            // Arrange          
            var productName = "Product Mock 1";
            IEnumerable<Product> productList = GetProductMockData().Where(p => p.Name.Contains(productName)).ToList();
            Products products = new Products
            {
                Items = productList
            };

            _productRepositoryMock.Setup(a => a.GetAllAsync()).Returns(Task.FromResult(productList));

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllProductsByNameQuery>(), default));

            GetAllProductsByNameQuery query = new GetAllProductsByNameQuery(productName);
            GetAllProductsByNameHandler handler = new GetAllProductsByNameHandler(_unitOfWorkMock.Object);

            // Act
            var result = await handler.Handle(query, default);

            // Assert           
            result.Should().NotBeNull();
            result.Should().BeOfType<Products>();
            result.Should().BeEquivalentTo(products);
            products.Items.Should().HaveCount(result.Items.Count());
        }

        private  IEnumerable<Product> GetProductMockData()
        {
            var products = new List<Product>() {

                new Product()
                 {
                DeliveryPrice = 11,
                Description = "Product Description 1",
                Name = "Product Mock 1",
                Price = 111,
                Id = new Guid("420BB34C-EF73-444F-B77F-4B31E8E2A0AF")
            } ,
                 new Product()
                 {
                DeliveryPrice = 22,
                Description = "Product Description 2",
                Name = "Product Mock 2",
                Price = 222,
                Id = new Guid("A2BAD5C4-61F3-40D3-96A3-34638E424CA7") 
            },
                  new Product()
                 {
                DeliveryPrice = 33,
                Description = "Product Description 3",
                Name = "Product Mock 3",
                Price = 333,
                Id = new Guid("41CD98C7-E783-4FE1-BBE6-D5E8EBDF7D74") 
            }
            };

            return products;
        }
    }
}
