using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Stocks")]
    public class Stock
    {
        [Key][ForeignKey("ProductVariant")]
        public int ProductVariantId { get; set; }
        public int CountOfStock { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }
    }
}