using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Routing;
using MvcContrib.TestHelper.Fakes;
using System.Web;
using System.Web.Mvc;
using MvcContrib.TestHelper;
using EFMVCDemo.Controllers;

namespace EFMVCDemo.UnitTest.mvc
{
    [TestClass]
    public class RouteConfigTest
    {
        [TestInitialize]
        public void setup()
        {
            RouteTable.Routes.Clear();
        }

        /*
         * Use "ShouldMapTo" to test route is strong type tested. 
         * If there is no existing controller/action mapped to this route or parameter validation failed, throw exception.
         * ref link: http://blog.miniasp.com/post/2010/09/23/ASPNET-MVC-Unit-Testing-Part-06-Routing.aspx
         * 
         * Other test methods use weak type tested, they will not throw exception even if no mapped controller/action exists.
         */
        [TestMethod]
        public void Test_RegisterRoutes_Home_Index_StrongTyped()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            try
            {
                "~/Home/Index".WithMethod(HttpVerbs.Get).ShouldMapTo<HomeController>(x => x.Index());
            }
            catch (AssertionException ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Test_RegisterRoutes_Home_Index_Without_Id()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HttpContextBase httpContext = new FakeHttpContext("~/", "GET");
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Index", routeData.Values["action"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_RegisterRoutes_Home_Detail_With_Id()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HttpContextBase httpContext = new FakeHttpContext("~/Home/Detail/1", "GET");
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            Assert.AreEqual("Home", routeData.Values["controller"]);
            Assert.AreEqual("Detail", routeData.Values["action"]);
            Assert.AreEqual("1", routeData.Values["id"]);
        }

        [TestMethod]
        public void Test_RegisterRoutes_sso_route()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            HttpContextBase httpContext = new FakeHttpContext("~/studentlan/bansso/hello", "GET");
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            Assert.AreEqual("SsoAims", routeData.Values["controller"]);
            Assert.AreEqual("sso", routeData.Values["action"]);
            Assert.AreEqual("hello", routeData.Values["moduleCode"]);
            Assert.AreEqual(UrlParameter.Optional, routeData.Values["moduleParams"]);
        }
    }
}
