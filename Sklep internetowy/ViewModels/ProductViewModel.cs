using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.View
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public OptionalAttributes OptionalAttributes { get; set; }
    }
}