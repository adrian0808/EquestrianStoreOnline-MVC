using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.View
{
    public class ProductViewModel
    {
        public IEnumerable<ProductVariant> productVariants { get; set; }
        public Product Product { get; set; }
      

    }
}