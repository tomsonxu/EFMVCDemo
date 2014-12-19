using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using EFMVCDemo.DAL;
using System.Data.Entity;

namespace EFMVCDemo.IntegrationTest.DAL
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestInitialize]
        public void setup()
        {
            MyContext context = new MyContext();
            context.Database.Initialize(true);  //very important, it makes db initialize before every test!!
        }

        [TestMethod]
        public void Test_GetAddProduct()
        {
            MyContext context = new MyContext();
            ProductRepository productRepository = new ProductRepository(context);
            productRepository.addProduct(new Product { Name = "p3", Description = "desc3", LogTime=DateTime.Now });
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
