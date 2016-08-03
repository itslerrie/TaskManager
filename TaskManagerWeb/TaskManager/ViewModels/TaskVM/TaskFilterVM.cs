using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using TaskManager.Tools;
using TaskManager.Models;

namespace TaskManager.ViewModels.TaskVM
{
    public class TaskFilterVM : FilterVM<Task>
    {
        [FilterByAttribute(DisplayName = "Tittle:")]
        public string Tittle { get; set; }

        public override Expression<Func<Task, bool>> GenerateFilter()
        {
            return (t => (t.assignetId == AuthenticationManager.LoggedUser.Id || t.creatorId == AuthenticationManager.LoggedUser.Id) &&
                         (String.IsNullOrEmpty(Tittle) || t.tittle.Contains(Tittle)));
        }
    }
}