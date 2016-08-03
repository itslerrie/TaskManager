using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.UserVM
{
    public class DeleteUserVM : BaseVMid
    {
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Display(Name = "Full Name:")]
        public string FullName { get; set; }

        [Display(Name = "E-mail:")]
        public string Email { get; set; }

        [Display(Name = "Is Admin:")]
        public bool IsAdmin { get; set; }
    }
}