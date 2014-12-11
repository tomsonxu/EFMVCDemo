using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFMVCDemo.DAL
{
    public class MyContext : DbContext
    {
        public MyContext() : base("EFMVCDemoDB") { }
        public DbSet<Product> Products { get; set; }
    }
}