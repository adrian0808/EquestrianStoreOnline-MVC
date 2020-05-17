using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.ViewModels
{
    public class EditProductViewModel
    {
        public ProductVariant ProductVariant { get; set; }
        public IEnumerable<Product> Product { get; set; }
        public IEnumerable<Size> Size { get; set; }
        public IEnumerable<Color> Color { get; set; }
        public bool ? Confirm { get; set; }
   
    }
}