using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sklep_internetowy.Models;

namespace Sklep_internetowy.View
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Bestsellers { get; set; }
        public IEnumerable<Product> New { get; set; }
    }
}