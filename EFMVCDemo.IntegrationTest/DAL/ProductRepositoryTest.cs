using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using EFMVCDemo.DAL;

namespace EFMVCDemo.IntegrationTest.DAL
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestInitialize]
        public void setup()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
        }

        [TestMethod]
        public void Test_GetAddProduct()
        {
            MyContext context = new MyContext();
            ProductRepository productRepository = new ProductRepository(context);
            productRepository.addProduct(new Product { Name = "p3", Description = "desc3" });
            Assert.AreEqual(3, productRepository.getProducts().Count);
        }

        [TestMethod]
        public void Test_GetProducts()
        {
            MyContext context = new MyContext();
            ProductRepository productRepository = new ProductRepository(context);
            Assert.AreEqual(2, productRepository.getProducts().Count);
        }

    }
}
