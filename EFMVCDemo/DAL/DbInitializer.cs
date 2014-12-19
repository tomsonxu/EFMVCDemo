using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFMVCDemo.DAL
{
    public class DbInitializer : DropCreateDatabaseAlways<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            var products = new List<Product>
            {
                new Product{Name="p1", Description="desc1", LogTime=DateTime.Now},
                new Product{Name="p2", Description="desc2", LogTime=DateTime.Now}
            };
            products.ForEach(s => context.Products.Add(s));
            base.Seed(context);
        }
    }
}