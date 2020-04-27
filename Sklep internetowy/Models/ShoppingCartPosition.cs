using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{    
    public class ShoppingCartPosition
    { 
        public ProductVariant ProductVariant { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get { return Quantity * ProductVariant.Product.Price; } set { value = Quantity * ProductVariant.Product.Price; } }
    }
}