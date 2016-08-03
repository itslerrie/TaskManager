using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using TaskManager.Tools;

namespace TaskManager.ViewModels.CommentVM
{
    public class CommentFilterVM : FilterVM<Comment>
    {
        [FilterByAttribute(DisplayName = "Tittle:")]
        public string Tittle { get; set; }

        public override Expression<Func<Comment, bool>> GenerateFilter()
        {
            return (c => (String.IsNullOrEmpty(Tittle) || c.commentTittle.Contains(Tittle)));
        }
    }
}