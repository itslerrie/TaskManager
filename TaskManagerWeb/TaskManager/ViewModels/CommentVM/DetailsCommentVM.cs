using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.CommentVM
{
    public class DetailsCommentVM:BaseVMid
    {
        [Display(Name = "Tittle:")]
        public string Tittle { get; set; }

        [Display(Name = "Content:")]
        public string Content { get; set; }

        [Display(Name = "Creator:")]
        public int CreatorId { get; set; }

        [Display(Name = "Date Created:")]
        public DateTime Date { get; set; }

        public int TaskId { get; set; }

        public List<User> users { get; set; }
    }
}