using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PagedList;
using TaskManager.ViewModels.CommentVM;

namespace TaskManager.ViewModels.TaskVM
{
    public class DetailsTaskVM : BaseVMid
    {
        [Display(Name = "Tittle:")]
        public string Tittle { get; set; }

        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Display(Name = "Assignee:")]
        public int AssigneeId { get; set; }

        [Display(Name = "Creator:")]
        public int CreatorId { get; set; }

        [Display(Name = "Created On:")]
        public DateTime CreateOn { get; set; }

        [Display(Name = "Edited By:")]
        public int EditedBy { get; set; }

        [Display(Name = "Edited On:")]
        public DateTime EditedOn { get; set; }

        [Display(Name = "Working Hours:")]
        public int WorkingHours { get; set; }

        [Display(Name = "Status:")]
        public bool Status { get; set; }

        public List<User> users { get; set; }

        public ListCommentVM CommentsVM { get; set; }
        public ListLogVM LoggedWorkVM { get; set; }
    }
}