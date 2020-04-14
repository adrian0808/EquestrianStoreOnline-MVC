using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<ShoppingCartPosition> ShoppingCartPositions { get; set; }
        public decimal TotalPrice { get; set; }

    }
}