using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Brands")]
    public class Brand
    {
        public int BrandId { get; set; }
        public string brand { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}