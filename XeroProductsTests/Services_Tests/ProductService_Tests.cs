using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XeroProducts.Models;
using XeroProducts.Repositories;
using XeroProducts.Services;

namespace XeroProductsTests
{
    /// <summary>
    /// Unit tests for Product  service layer
    /// </summary>
    [TestClass]
    public class ProductService_Tests
    {
        [TestMethod]
        public void ProductService_Should_return_Data_Get()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            List<Product> listProd = new List<Product>();
            listProd.Add(mockProduct);
            Products products = new Products();
            products.Items = listProd;
            mockProductRepository.Setup(a=>a.GetProducts()).Returns(Task.FromResult(products));
            var actualResult = dataService.GetProducts();
            Assert.AreEqual(products.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductService_Should_return_Data_GetById_Exists()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.ProductExists(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"))).Returns(true);
            mockProductRepository.Setup(a => a.GetProducts(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"))).Returns(Task.FromResult(product));
            var actualResult = dataService.GetProducts(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"));
            Assert.AreEqual(mockProduct.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProductService_Should_return_Data_GetById_Not_Exits()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.ProductExists(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"))).Returns(false);
            mockProductRepository.Setup(a => a.GetProducts(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"))).Returns(Task.FromResult(product));
            var actualResult = dataService.GetProducts(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"));
        }


        [TestMethod]
        public void ProductService_Should_return_Data_GetByName()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.GetProductByName("Apple iPhone 6S")).Returns(Task.FromResult(product));
            var actualResult = dataService.GetProductByName("Apple iPhone 6S");
            Assert.AreEqual(mockProduct.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ProductService_Edit_Product_Should_return_Invalid_Product_Exception()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.PutProduct(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"), product)).Returns(Task.FromResult(product));
            var actualResult = dataService.PutProduct(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"), product);
        }

        [TestMethod]
        public void ProductService_Edit_Product_Should_return_Success()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.ProductExists(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"))).Returns(true);
            mockProductRepository.Setup(a => a.PutProduct(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"), product)).Returns(Task.FromResult(product));
            var actualResult = dataService.PutProduct(new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3"), product);
            Assert.AreEqual(mockProduct.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductService_Add_Product_Should_return_Success()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.ProductExists(product.Id)).Returns(false);
            mockProductRepository.Setup(a => a.PostProduct(product)).Returns(Task.FromResult(product));
            var actualResult = dataService.PostProduct( product);
            Assert.AreEqual(mockProduct.GetType(), actualResult.Result.GetType());
        }

        [TestMethod]
        public void ProductService_Delete_Product_Should_return_Success()
        {
            var mockProductRepository = new Mock<IProductRepository>();
            IProductService dataService = new ProductService(mockProductRepository.Object);
            var mockProduct = new Product();
            mockProduct.Id = new Guid("DE1287C0-4B15-4A7B-9D8A-DD21B3CAFEC3");
            mockProduct.Name = "qwert";
            mockProduct.Price = 123;
            mockProduct.Description = "assdf";
            mockProduct.DeliveryPrice = 123;
            Product product = mockProduct;
            mockProductRepository.Setup(a => a.ProductExists(product.Id)).Returns(true);
            mockProductRepository.Setup(a => a.DeleteProduct(product.Id)).Returns(Task.FromResult(product.Id));
            var actualResult = dataService.DeleteProduct(product.Id);
            Assert.AreEqual(mockProduct.Id.GetType(), actualResult.Result.GetType());
        }
    }
}
