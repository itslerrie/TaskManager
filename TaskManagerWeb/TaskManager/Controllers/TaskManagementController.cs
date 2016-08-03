using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLDataAccess.Repository;
using SQLDataAccess.Enteties;
using TaskManager.Models;
using System.Web.Hosting;
using TaskManager.ViewModels.TaskVM;
using TaskManager.Filters;
using TaskManager.ViewModels;
using TaskManager.ViewModels.CommentVM;
using TaskManager.ViewModels.UserVM;
using System.Linq.Expressions;

namespace TaskManager.Controllers
{
    [AuthenticationFilter]
    public class TaskManagementController : BaseController<Task,ListTaskVM,CreateTaskVM,EditTaskVM,DeleteTaskVM, TaskFilterVM>
    {

        [HttpGet]
        public ActionResult Status(int id)
        {

            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(id);

            if (task.assignetId == AuthenticationManager.LoggedUser.Id || task.creatorId == AuthenticationManager.LoggedUser.Id)
            {
                if (task.finnished == true)
                {
                    task.finnished = false;
                }
                else
                {
                    task.finnished = true;
                }

                repo.Edit(task);

                return RedirectToAction("Index", "TaskManagement");
            }
            else
            {
                return RedirectToAction("Index", "TaskManagement");
            }
           
        }

        [HttpGet]
        public ActionResult SetHours(int id)
        {

            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(id);

            SetHoursVM set = new SetHoursVM();
            set.SetHours = 0;
            set.Id = task.Id;

            if (task.assignetId == AuthenticationManager.LoggedUser.Id || task.creatorId == AuthenticationManager.LoggedUser.Id)
            {
                return View(set);
            }
            else
            {
                return RedirectToAction("Index", "TaskManagement");
            }

        }

        [HttpPost]
        public ActionResult SetHours(SetHoursVM set)
        {
            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(set.Id);
            task.editedBY = AuthenticationManager.LoggedUser.Id;
            task.editedON = DateTime.Now;
            task.estimate = task.estimate + set.SetHours;

            repo.Edit(task);

            LogWorkRepo repoWork = new LogWorkRepo();
            LogWork work = new LogWork();
            work.TaskId = task.Id;
            work.Username = AuthenticationManager.LoggedUser.Username;
            work.Email = AuthenticationManager.LoggedUser.Email;
            work.Time = set.SetHours;
            work.Date = DateTime.Now;

            repoWork.Create(work);

            return RedirectToAction("CreateComment", "CommentManagement", new {id = task.Id });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
          
            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(id);

            if (task.assignetId != AuthenticationManager.LoggedUser.Id && task.creatorId != AuthenticationManager.LoggedUser.Id)
                return RedirectToAction("Index", "TaskManagement");

            DetailsTaskVM model = new DetailsTaskVM();
            model.CommentsVM = new ListCommentVM();
            model.LoggedWorkVM = new ListLogVM();

            UserRepo repoUser = new UserRepo();
            List<User> users = repoUser.GetAll().ToList();

            CommentRepo repoComment = new CommentRepo();
            List<Comment> comments = repoComment.GetAll(c => c.taskId == id).ToList();

            LogWorkRepo repoWork = new LogWorkRepo();
            List<LogWork> works = repoWork.GetAll(w => w.TaskId == id).ToList();

            model.Id = task.Id;
            model.Tittle = task.tittle;
            model.Content = task.content;
            model.WorkingHours = task.estimate;
            model.AssigneeId = task.assignetId;
            model.CreatorId = task.creatorId;
            model.CreateOn = task.createdON;
            model.EditedBy = task.editedBY;
            model.EditedOn = task.editedON;
            model.Status = task.finnished;

            model.users = new List<User>();
            foreach (User item in users)
                model.users.Add(item);

            model.CommentsVM.Items = new List<Comment>();
            foreach (Comment item in comments)
                model.CommentsVM.Items.Add(item);

            model.LoggedWorkVM.Items = new List<LogWork>();
            foreach (LogWork item in works)
                model.LoggedWorkVM.Items.Add(item);

            model.CommentsVM.Pager = new PagerVM();
            model.CommentsVM.Filter = new CommentFilterVM();
            model.CommentsVM.Pager.Prefix = "CommentsVM.Pager.";
            model.CommentsVM.Filter.Prefix = "CommentsVM.Filter.";
            model.CommentsVM.Filter.Pager = model.CommentsVM.Pager;

            model.LoggedWorkVM.Pager = new PagerVM();
            model.LoggedWorkVM.Filter = new LogWorkFilterVM();
            model.LoggedWorkVM.Pager.Prefix = "LoggedWorkVM.Pager.";
            model.LoggedWorkVM.Filter.Prefix = "LoggedWorkVM.Filter.";
            model.LoggedWorkVM.Filter.Pager = model.LoggedWorkVM.Pager;

            TryUpdateModel(model);

            Expression<Func<Comment, bool>> commentFilter = model.CommentsVM.Filter.GenerateFilter();
            model.CommentsVM.Items = repoComment.GetAll(commentFilter, model.CommentsVM.Pager.CurrentPage, model.CommentsVM.Pager.PageSize).ToList();

            Expression<Func<LogWork, bool>> workFilter = model.LoggedWorkVM.Filter.GenerateFilter();
            model.LoggedWorkVM.Items = repoWork.GetAll(workFilter, model.LoggedWorkVM.Pager.CurrentPage, model.LoggedWorkVM.Pager.PageSize).ToList();

            model.CommentsVM.Pager.PagesCount = (int)Math.Ceiling(model.CommentsVM.Items.Count / (double)model.CommentsVM.Pager.PageSize);
            model.CommentsVM.Items = model.CommentsVM.Items.Skip(model.CommentsVM.Pager.PageSize * (model.CommentsVM.Pager.CurrentPage - 1)).Take(model.CommentsVM.Pager.PageSize).ToList();

            model.LoggedWorkVM.Pager.PagesCount = (int)Math.Ceiling(model.LoggedWorkVM.Items.Count / (double)model.LoggedWorkVM.Pager.PageSize);
            model.LoggedWorkVM.Items = model.LoggedWorkVM.Items.Skip(model.LoggedWorkVM.Pager.PageSize * (model.LoggedWorkVM.Pager.CurrentPage - 1)).Take(model.LoggedWorkVM.Pager.PageSize).ToList();

            return View(model);
        }

        public override BaseRepo<Task> SetRepo()
        {
            return new TaskRepo();
        }

        public override void FillList(CreateTaskVM model)
        {
            UserRepo repoUser = new UserRepo();
            List<User> result = repoUser.GetAll().ToList();

            model.ListAssignee = new List<SelectListItem>();
            foreach (var item in result)
            {
                model.ListAssignee.Add(new SelectListItem()
                {
                    Text = item.Username,
                    Value = item.Id.ToString()
                });
            }

            model.ListAssignee[0].Selected = true;
        }

        public override void PopulateItem(Task item, CreateTaskVM model)
        {
            item.assignetId = model.AssigneeId;
            item.tittle = model.Tittle;
            item.content = model.Content;
            item.estimate = model.WorkingHours;
            item.creatorId = AuthenticationManager.LoggedUser.Id;
            item.createdON = DateTime.Now;
            item.editedBY = AuthenticationManager.LoggedUser.Id;
            item.editedON = DateTime.Now;
            item.finnished = false;
        }

        public override void PopulateModel(Task item, DeleteTaskVM model)
        {
            UserRepo repoUser = new UserRepo();
            List<User> users = repoUser.GetAll().ToList();

            model.Id = item.Id;
            model.Tittle = item.tittle;
            model.Content = item.content;
            model.WorkingHours = item.estimate;
            model.AssigneeId = item.assignetId;
            model.CreatorId = item.creatorId;
            model.CreateOn = item.createdON;
            model.EditedBy = item.editedBY;
            model.EditedOn = item.editedON;
            model.Status = item.finnished;

            model.users = new List<User>();
            foreach (User user in users)
            {
                model.users.Add(user);
            }

        }

        public override void PopulateItemEdit(Task item, EditTaskVM model)
        {
            item.tittle = model.Tittle;
            item.content = model.Content;
            item.assignetId = model.AssigneeId;
            item.editedBY = AuthenticationManager.LoggedUser.Id;
            item.editedON = DateTime.Now;
            item.Id = model.Id;
            item.estimate = model.WorkingHours;
        }

        public override void PopulateModelEdit(Task item, EditTaskVM model)
        {
            UserRepo repoUser = new UserRepo();
            List<User> result = repoUser.GetAll().ToList();

            model.Id = item.Id;
            model.AssigneeId = item.assignetId;
            model.Tittle = item.tittle;
            model.Content = item.content;
            model.WorkingHours = item.estimate;
            model.CreatorId = item.creatorId;

            model.ListAssignee = new List<SelectListItem>();
            foreach (var user in result)
            {
                model.ListAssignee.Add(new SelectListItem()
                {
                    Text = user.Username,
                    Value = user.Id.ToString()
                });
            }

            int selected = 0;

            foreach (var user in result)
            {
                if (item.assignetId == user.Id)
                {
                    break;
                }

                selected++;
            }

            model.ListAssignee[selected].Selected = true;
        }

        public override void ExtraDelete(Task task)
        {
            LogWorkRepo repoWork = new LogWorkRepo();
            CommentRepo repoComment = new CommentRepo();

            List<Comment> comments = repoComment.GetAll().ToList();
            List<LogWork> work = repoWork.GetAll().ToList();

            foreach (Comment item in comments)
            {
                if (item.taskId == task.Id)
                {
                    repoComment.Delete(item);
                }
            }


            foreach (LogWork item in work)
            {
                if (item.TaskId == task.Id)
                {
                    repoWork.Delete(item);
                }
            }
        }
    }
}