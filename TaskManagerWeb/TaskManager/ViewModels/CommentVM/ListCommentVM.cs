using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.CommentVM
{
    public class ListCommentVM:BaseVMList<Comment, CommentFilterVM>
    {
    }
}