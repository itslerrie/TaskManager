using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using TaskManager.Tools;

namespace TaskManager.ViewModels.CommentVM
{
    public class LogWorkFilterVM : FilterVM<LogWork>
    {
        [FilterByAttribute(DisplayName = "Username:")]
        public string Username { get; set; }
        [FilterByAttribute(DisplayName = "E-mail:")]
        public string Email { get; set; }


        public override Expression<Func<LogWork, bool>> GenerateFilter()
        {
            return (w => (String.IsNullOrEmpty(Username) || w.Username.Contains(Username)) &&
                    (String.IsNullOrEmpty(Email) || w.Email.Contains(Email)));
        }
    }
}