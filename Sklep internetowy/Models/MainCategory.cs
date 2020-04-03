using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    public class MainCategory
    {
        [Key]
        public int MainCategoryId { get; set; }
        [Required][StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Category>Categories { get; set; }
    }
}