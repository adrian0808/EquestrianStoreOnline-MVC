using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Sizes")]
    public class Size
    {
        public int SizeId { get; set; }
        public string size { get; set; }
        public virtual ICollection<ProductVariant> ProductsVariant { get; set; }

    }
}