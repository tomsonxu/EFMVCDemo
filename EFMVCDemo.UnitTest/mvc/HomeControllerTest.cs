using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFMVCDemo.Controllers;
using EFMVCDemo.DAL;
using Moq;
using System.Web.Mvc;
using System.Collections.Generic;
using MvcContrib.TestHelper;
using System.Web;
using System.Web.Routing;
using System.Collections.Specialized;

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
        public void Test_ViewBagDemo()
        {
            HomeController controller = new HomeController(null);
            var viewResult = (ViewResult)controller.ViewBagDemo();

            Assert.AreEqual("abc", viewResult.ViewBag.msg);
        }

        [TestMethod]
        public void Test_RedirectDemo_RedirectTo_AnotherAction()
        {
            HomeController controller = new HomeController(null);
            RedirectToRouteResult result = (RedirectToRouteResult)controller.RedirectDemo(0);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_Session_With_NullValue()
        {
            HomeController controller = new HomeController(null);

            //set http context stub to controller by using mvccontrib testhelper!!
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.InitializeController(controller);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.SessionDemo();
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_Session_With_NonNullValue()
        {
            HomeController controller = new HomeController(null);

            //set http context stub to controller by using mvccontrib testhelper!!
            TestControllerBuilder builder = new TestControllerBuilder();
            builder.Session["testSession"] = "abc";
            builder.InitializeController(controller);

            var viewResult = (ViewResult)controller.SessionDemo();
            Assert.AreEqual("About", viewResult.ViewName);
        }

        [TestMethod]
        public void Test_RequestServerVariablesDemo_With_NullValue()
        {
            HomeController controller = new HomeController(null);

            var serverVars = new NameValueCollection();
            var context = new Mock<HttpContextBase>();
            context.Setup(x => x.Request.ServerVariables).Returns(serverVars).Verifiable();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.RequestServerVariablesDemo();
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_RequestServerVariablesDemo_With_NonNullValue()
        {
            HomeController controller = new HomeController(null);

            var serverVars = new NameValueCollection();
            serverVars.Add("REMOTE_ADDR", "abc");
            var context = new Mock<HttpContextBase>();
            context.Setup(x => x.Request.ServerVariables).Returns(serverVars).Verifiable();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = (ViewResult)controller.RequestServerVariablesDemo();
            Assert.AreEqual("About", viewResult.ViewName);
        }

        [TestMethod]
        public void Test_RequestCookiesDemo_With_NullValue()
        {
            HomeController controller = new HomeController(null);

            var cookies = new HttpCookieCollection();
            var context = new Mock<HttpContextBase>();
            context.Setup(x => x.Request.Cookies).Returns(cookies).Verifiable();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            RedirectToRouteResult result = (RedirectToRouteResult)controller.RequestCookiesDemo();
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Test_RequestCookiesDemo_With_NonNullValue()
        {
            HomeController controller = new HomeController(null);

            var cookies = new HttpCookieCollection();
            cookies.Add(new HttpCookie("testCookie", "abc"));
            var context = new Mock<HttpContextBase>();
            context.Setup(x => x.Request.Cookies).Returns(cookies).Verifiable();
            controller.ControllerContext = new ControllerContext(context.Object, new RouteData(), controller);

            var viewResult = (ViewResult)controller.RequestCookiesDemo();
            Assert.AreEqual("About", viewResult.ViewName);
        }
    }

}
