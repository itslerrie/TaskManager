using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLDataAccess.Repository;
using SQLDataAccess.Enteties;
using TaskManager.Models;
using System.Web.Hosting;
using TaskManager.ViewModels.UserVM;
using TaskManager.Filters;

namespace TaskManager.Controllers
{
    [AdminFilter]
    public class UserManagementController : BaseController<User, ListUserVM, CreateUserVM, EditUserVM, DeleteUserVM, UserFilterVM>
    {
        // GET: UserManagement
        public override BaseRepo<User> SetRepo()
        {
            return new UserRepo();
        }

        public override void PopulateItem(User item, CreateUserVM model)
        {
            item.Username = model.Username;
            item.Password = model.Password;
            item.IsAdmin = model.IsAdmin;
            item.Fullname = model.FullName;
            item.Email = model.Email;
        }

        public override void PopulateModel(User item, DeleteUserVM model)
        {
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FullName = item.Fullname;
            model.Email = item.Email;
            model.IsAdmin = item.IsAdmin;
        }

        public override void PopulateItemEdit(User item, EditUserVM model)
        {
            item.Username = model.Username;
            item.Password = model.Password;
            item.IsAdmin = model.IsAdmin;
            item.Email = model.Email;
            item.Fullname = model.FullName;
            item.Id = model.Id;

        }

        public override void PopulateModelEdit(User item, EditUserVM model)
        {
            model.Username = item.Username;
            model.Password = item.Password;
            model.FullName = item.Fullname;
            model.IsAdmin = item.IsAdmin;
            model.Email = item.Email;
            model.Id = item.Id;
        }

        public override void ExtraDelete(User user)
        {
            TaskRepo repoTask = new TaskRepo();
            LogWorkRepo repoWork = new LogWorkRepo();
            CommentRepo repoComment = new CommentRepo();

            List<Task> tasks = repoTask.GetAll(t => t.creatorId == user.Id).ToList();
            List<Comment> comments = repoComment.GetAll().ToList();
            List<LogWork> works = repoWork.GetAll().ToList();

            foreach (Task item in tasks)
            {
                foreach (Comment comment in comments)
                {
                    if (comment.taskId == item.Id)
                    {
                        repoComment.Delete(comment);
                    }
                }

                foreach (LogWork work in works)
                {
                    if (work.TaskId == item.Id)
                    {
                        repoWork.Delete(work);
                    }
                }

                repoTask.Delete(item);
            }
        }
    }
}