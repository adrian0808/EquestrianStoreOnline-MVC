using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    public enum StateOfOrder
    {
        InProgress,
        Completed
    }
    
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        //[ForeignKey("User")]
        //public string UserId { get; set; }
        
        [Required(ErrorMessage = "Enter firstname")]
        [StringLength(50)]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "Enter lastname")]
        [StringLength(50)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "Enter adress")]
        [StringLength(100)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Enter city")]
        [StringLength(50)]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter zip code")]
        [StringLength(6)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter phone number")]
        [StringLength(20)]
        [RegularExpression(@"^(\+[0-9]{9})$", ErrorMessage = "Phone number format is invalid!")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Enter email")]
        [EmailAddress(ErrorMessage = "Email format is invalid!")]
        public string Email { get; set; }

        [StringLength(250)]
        public string Comment { get; set; }

        public DateTime AddingDate { get; set; }

        public StateOfOrder StateOfOrder { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<OrderPosition> OrderPositions { get; set; }

       
    }
}