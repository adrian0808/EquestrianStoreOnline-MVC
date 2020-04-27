using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Sklep_internetowy.Controllers.ManageController;

namespace Sklep_internetowy.ViewModels
{

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Aktualne hasło")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "{0} musi mieć co najmniej {2} znaków.", MinimumLength = 6)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasło i potwierdzenie hasła nie są takie same")]
        public string ConfirmPassword { get; set; }
    }

    public class ManageCredentialViewModel
    {
        public ChangePasswordViewModel ChangePasswordViewModel { get; set; }
        public UserData UserData { get; set; }
        public ManageMessageId? Message  { get; set; }
    }



}