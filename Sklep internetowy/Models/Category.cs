using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Moq.EntityFrameworkCore;

namespace Sklep_internetowy.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; } //primary key
        [ForeignKey("MainCategory")]
        public int MainCategoryId { get; set; }
        [Required][StringLength(100)]
        public string Name { get; set; }
        [Required][StringLength(300)]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
        public virtual MainCategory MainCategory { get; set; }

    }
}