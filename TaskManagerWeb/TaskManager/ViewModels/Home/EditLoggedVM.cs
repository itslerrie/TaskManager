using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.ValidationAttributes;

namespace TaskManager.ViewModels.Home
{
    public class EditLoggedVM : BaseVMid
    {
        [Required(ErrorMessage = "Please enter Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please repeat Password")]
        [Display(Name = "Repeat Password:")]
        [MatchValue("Password")]
        public string rePassword { get; set; }

        [Required(ErrorMessage = "Please enter E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter Full Name")]
        [Display(Name = "Full Name:")]
        public string FullName { get; set; }
    }
}