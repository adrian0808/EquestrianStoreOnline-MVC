using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("ProductsVariant")]
    public class ProductVariant
    {
        [Key]
        public int ProductVariantId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Size")]
        public int SizeId { get; set; }
        [ForeignKey("Color")]
        public int ColorId { get; set; }
        
        
        
        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }

    }
}