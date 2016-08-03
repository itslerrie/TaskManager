using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TaskManager.ViewModels.TaskVM
{
    public class EditTaskVM : BaseVMid
    {
        [Required(ErrorMessage = "Please enter Task Tittle")]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Please enter Task Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please enter Working Hours")]
        [Display(Name = " Working Hours")]
        public int WorkingHours { get; set; }

        [Required(ErrorMessage = "Please enter an Assignee")]
        [Display(Name = "Assignee")]
        public int AssigneeId { get; set; }

        public int CreatorId { get; set; }
        public List<SelectListItem> ListAssignee { get; set; }
    }
}