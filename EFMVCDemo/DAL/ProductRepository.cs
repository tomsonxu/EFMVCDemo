using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EFMVCDemo.DAL
{
    public class ProductRepository: IProductRepository
    {
        MyContext context;
        public ProductRepository(MyContext context)
        {
            this.context = context;
        }

        public void addProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }

        public List<Product> getProducts()
        {
            return context.Products.ToList();
        }


    }
}