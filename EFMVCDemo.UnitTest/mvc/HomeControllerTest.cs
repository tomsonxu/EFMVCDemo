using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFMVCDemo.Controllers;
using EFMVCDemo.DAL;
using Moq;
using System.Web.Mvc;
using System.Collections.Generic;

namespace EFMVCDemo.UnitTest.mvc
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Test_IndexAction()
        {
            var mock = new Mock<IProductRepository>();

            //Verifiable() to mark this expectation, when call mock.Verity(), it will check if this expectation is met
            //for this case, it set "getProducts()" must be called.
            mock.Setup(c => c.getProducts()).Returns(new List<Product> { new Product { Name = "p1", Description = "p1" } }).Verifiable();

            HomeController controller = new HomeController(mock.Object);
            var viewResult = (ViewResult)controller.Index();

            Assert.AreEqual("Index", viewResult.ViewName);

            List<Product> products = (List<Product>)viewResult.ViewData.Model;
            Assert.AreEqual(products.Count, 1);

            //verify that all "Virifiable" expectations have been met.
            mock.Verify();
        }

        [TestMethod]
        public void Test_RedirectDemo_RedirectTo_AnotherAction()
        {
            HomeController controller = new HomeController(null);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.RedirectDemo(0);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
