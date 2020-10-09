using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layout.Models
{
    public class ProductsListVm
    {
        public ProductsListVm()
        {
            this.Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
    }
}
