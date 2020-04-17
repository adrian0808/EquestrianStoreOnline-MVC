using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.ViewModels
{
    public class ShoppingCartRemoveViewModel
    {
        public decimal ShoppingCartTotalPrice { get; set; }
        public int ShoppingCartPositionsQuantity { get; set; }
        public int RemovePositionQuantity { get; set; }
        public int RemovePositionId { get; set; }
    }
}