using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroProducts.Data.Models;
using XeroProducts.Repositories;
using XeroProducts.Services;

namespace XeroProductsTests.Services_Tests
{
    /// <summary>
    /// Unit tests for Product options service layer
    /// </summary>
    [TestClass]
    public class ProductOptionsService_Tests
    {
        [TestMethod]
        public void ProductOptionService_Should_return_Data_Get()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            List<ProductOption> options = new List<ProductOption>();
            options.Add(mockProductOption);
            ProductOptions listProd = new ProductOptions();
            listProd.Items = options;
            mockProductOptionsRepository.Setup(a => a.GetProductOptions()).Returns(Task.FromResult(listProd.Items));
            var actualResult = dataService.GetProductOptions();
            Assert.AreEqual(listProd.Items.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductOptionServices_Should_return_Ddata_ByProductId_Get()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            List<ProductOption> options = new List<ProductOption>();
            options.Add(mockProductOption);
            ProductOptions listProd = new ProductOptions();
            listProd.Items = options;
            mockProductOptionsRepository.Setup(a => a.ProductOptionsExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.GetProductOptions()).Returns(Task.FromResult(listProd.Items));
            var actualResult = dataService.GetProductOptions();
            Assert.AreEqual(listProd.Items.GetType(), actualResult.Result.GetType());
        }


        [TestMethod]
        public void ProductOptionServices_Should_return_Ddata_ByOptionId_Get()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            ProductOption options = new ProductOption();
            options = mockProductOption;
            mockProductRepository.Setup(a => a.ProductExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.ProductOptionsExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.GetProductOptionsByOptionId(mockProductOption.Id, mockProductOption.Id)).Returns(Task.FromResult(options));
            var actualResult = dataService.GetProductOptionsByOptionId(mockProductOption.Id, mockProductOption.Id);
            Assert.AreEqual(options.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(System.AggregateException))]
        public void ProductOptionServices_Should_Save_New_Option_Same_ID()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            ProductOption options = new ProductOption();
            options = mockProductOption;
            mockProductRepository.Setup(a => a.ProductExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.ProductOptionsExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.PostProductOptions(mockProductOption)).Returns(Task.FromResult(options));
            var actualResult = dataService.PostProductOptions(mockProductOption.Id, mockProductOption);
            Assert.AreEqual(options.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductOptionServices_Should_Save_New_Option()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            ProductOption options = new ProductOption();
            options = mockProductOption;
            mockProductRepository.Setup(a => a.ProductExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.ProductOptionsExists(mockProductOption.Id)).Returns(false);
            mockProductOptionsRepository.Setup(a => a.PostProductOptions(mockProductOption)).Returns(Task.FromResult(options));
            var actualResult = dataService.PostProductOptions(mockProductOption.Id, mockProductOption);
            Assert.AreEqual(options.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductOptionServices_Should_Edit_Option()
        {
            var mockProductOptionsRepository = new Mock<IProductOptionsRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            IProductOptionsService dataService = new ProductOptionService(mockProductOptionsRepository.Object, mockProductRepository.Object);
            var mockProductOption = new ProductOption();
            mockProductOption.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Name = "qwert";
            mockProductOption.ProductId = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProductOption.Description = "asdf";
            ProductOption options = new ProductOption();
            options = mockProductOption;
            mockProductRepository.Setup(a => a.ProductExists(mockProductOption.Id)).Returns(true);
            mockProductOptionsRepository.Setup(a => a.ProductOptionsExists(mockProductOption.Id)).Returns(false);
            mockProductOptionsRepository.Setup(a => a.PutProductOptions(mockProductOption)).Returns(Task.FromResult(options));
            var actualResult = dataService.PutProductOptions(mockProductOption.Id, mockProductOption.Id, mockProductOption);
            Assert.AreEqual(options.GetType(), actualResult.Result.GetType());
        }
    }
}


