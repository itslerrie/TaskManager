using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.CommentVM
{
    public class CreateCommentVM : BaseVMid
    {
        [Required(ErrorMessage = "Please enter Comment Tittle")]
        public string Tittle { get; set; }

        [Required(ErrorMessage = "Please enter Comment Content")]
        public string Content { get; set; }

        public int TaskId { get; set; }
    }
}