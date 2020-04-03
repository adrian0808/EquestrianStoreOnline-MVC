using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Colors")]
    public class Color
    {
        public int ColorId { get; set; }
        public string color { get; set; }
        public virtual ICollection<Product> ProductVariants { get; set; }
    }

}