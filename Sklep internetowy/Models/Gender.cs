using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    [Table("Genders")]
    public class Gender
    {
        public int GenderId { get; set; }
        [Required][StringLength(100)]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}