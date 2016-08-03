using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using TaskManager.Tools;

namespace TaskManager.ViewModels.UserVM
{
    public class UserFilterVM : FilterVM<User>
    {
        [FilterByAttribute(DisplayName = "Username:")]
        public string Username { get; set; }
        [FilterByAttribute(DisplayName = "Full Name:")]
        public string FullName { get; set; }
        [FilterByAttribute(DisplayName = "Email:")]
        public string Email { get; set; }

        public override Expression<Func<User, bool>> GenerateFilter()
        {
            return (u => (String.IsNullOrEmpty(Username) || u.Username.Contains(Username)) &&
                         (String.IsNullOrEmpty(FullName) || u.Fullname.Contains(FullName)) &&
                         (String.IsNullOrEmpty(Email) || u.Email.Contains(Email)));
        }
    }
}