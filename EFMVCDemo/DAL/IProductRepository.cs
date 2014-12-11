using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFMVCDemo.DAL
{
    public interface IProductRepository
    {
        void addProduct(Product product);
        List<Product> getProducts();
    }
}
