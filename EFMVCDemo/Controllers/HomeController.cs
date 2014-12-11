using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EFMVCDemo.DAL;

namespace EFMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        IProductRepository productRepository;
        public HomeController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ActionResult Index()
        {
            List<Product> products = productRepository.getProducts();
            return View("Index", products);  //if not explicit view name, can't unit test it!!
        }

        public ActionResult RedirectDemo(int Id)
        {
            if (Id < 1)
                return RedirectToAction("Index");

            return View("About");
        }
    }
}