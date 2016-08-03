using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.TaskVM
{
    public class SetHoursVM : BaseVMid
    {
        [Required(ErrorMessage = "Please enter Hours")]
        [Display(Name = "Working Hours:")]
        public int SetHours { get; set; }
    }
}