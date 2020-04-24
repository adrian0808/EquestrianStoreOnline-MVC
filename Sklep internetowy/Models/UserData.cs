using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Models
{
    public class UserData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        [StringLength(6, ErrorMessage = "Kod pocztowy musi mieć {2} znaków.", MinimumLength = 6)]
        public string ZipCode { get; set; }      
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }

    }
}