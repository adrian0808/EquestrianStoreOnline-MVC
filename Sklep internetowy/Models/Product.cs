using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; } 
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        [Required][StringLength(100)]
        public string Name { get; set; }
        [Required][StringLength(1000)]
        public string Description { get; set; }
        [Required]
        public bool isBestseller { get; set; }
        public bool isSize { get; set; }
        public bool isColor { get; set; }
        public DateTime AddingDate { get; set; }
        public decimal Price { get; set; }
        public string GraphicFileName { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual ICollection<ProductVariant> ProductsVariants { get; set; }
    }
}