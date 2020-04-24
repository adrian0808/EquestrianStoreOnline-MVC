using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Wprowadź email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wprowadź hasło")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }

    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "{0} musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "hasło")]
        public string Password{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasło i potwierdzenie hasła nie są takie same")]
        public string ConfirmPassword { get; set; }



    }
}