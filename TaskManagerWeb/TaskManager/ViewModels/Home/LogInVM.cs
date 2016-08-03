using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.Home
{
    public class LogInVM
    {
        [Required(ErrorMessage = "Please enter a Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter a Password")]
        public string Password { get; set; }
    }
}