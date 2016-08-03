using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.ValidationAttributes;

namespace TaskManager.ViewModels.UserVM
{
    public class EditUserVM : BaseVMid
    {
        [Required(ErrorMessage = "Please enter a Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Repeat Password")]
        [Display(Name = "Repeat Password:")]
        [MatchValue("Password")]
        public string rePassword { get; set; }

        [Required(ErrorMessage = "Please enter a Full Name")]
        [Display(Name = "Full Name:")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter a Email Name")]
        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter an Admin Status")]
        [Display(Name = "Is Admin:")]
        public bool IsAdmin { get; set; }
    }
}