using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EFMVCDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MyContext context = new MyContext();

            Product product = new Product { Name = "product 1" };
            context.Products.Add(product);
            context.SaveChanges();

            return View();
        }
    }

    public class MyContext : DbContext
    {
        public MyContext() : base("EFMVCDemoDB") { }
        public DbSet<Product> Products { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseAlways<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            var products = new List<Product>
            {
                new Product{Name="p1", Description="desc1"},
                new Product{Name="p2", Description="desc2"}
            };
            products.ForEach(s => context.Products.Add(s));
            //context.SaveChanges();
            base.Seed(context);
        }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    } 

}