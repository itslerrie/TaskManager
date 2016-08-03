using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLDataAccess.Repository;
using SQLDataAccess.Enteties;
using TaskManager.Models;
using System.Web.Hosting;
using TaskManager.ViewModels.CommentVM;
using TaskManager.Filters;

namespace TaskManager.Controllers
{
    [AuthenticationFilter]
    public class CommentManagementController : Controller
    {
        [HttpGet]
        public ActionResult CreateComment(int id)
        {

            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(id);

            CreateCommentVM create = new CreateCommentVM();

            create.TaskId = task.Id;

            if (task.assignetId == AuthenticationManager.LoggedUser.Id || task.creatorId == AuthenticationManager.LoggedUser.Id)
            {
               
                return View(create);
            }
            else
            {
                return RedirectToAction("Index", "TaskManagement");
            }

           
        }

        [HttpPost]
        public ActionResult CreateComment(CreateCommentVM create)
        {

            if (!this.ModelState.IsValid)
            {
                return View(create);
            }

            CommentRepo commentRepo = new CommentRepo();
            Comment newComment = new Comment();
            newComment.commentTittle = create.Tittle;
            newComment.commentContent = create.Content;
            newComment.Date = DateTime.Now;
            newComment.commentCreatorId = AuthenticationManager.LoggedUser.Id;
            newComment.taskId = create.TaskId;

            commentRepo.Create(newComment);

            return RedirectToAction("Details", "TaskManagement", new { id = create.TaskId });
        }

        [HttpGet]
        public ActionResult CommentDetails(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
            {
                return RedirectToAction("Login", "Home");
            }

            CommentRepo repoComment = new CommentRepo();
            Comment comment = repoComment.GetById(id);

            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(comment.taskId);

            if (task.assignetId != AuthenticationManager.LoggedUser.Id || task.creatorId != AuthenticationManager.LoggedUser.Id)
            {
                UserRepo repoUser = new UserRepo();
                List<User> users = repoUser.GetAll().ToList();

                DetailsCommentVM details = new DetailsCommentVM();
                details.users = new List<User>();

                details.Id = comment.Id;
                details.Tittle = comment.commentTittle;
                details.Content = comment.commentContent;
                details.CreatorId = comment.commentCreatorId;
                details.TaskId = comment.taskId;
                details.Date = comment.Date;

                foreach (var item in users)
                {
                    details.users.Add(item);
                }

                return View(details);
            }
            else
            {
                return RedirectToAction("Index", "TaskManagement");
            }

           
        }

        [HttpGet]
        public ActionResult DeleteComment(int id)
        {
            if (AuthenticationManager.LoggedUser == null)
            {
                return RedirectToAction("Login", "Home");
            }

            CommentRepo repoComment = new CommentRepo();
            Comment comment = repoComment.GetById(id);

            TaskRepo repo = new TaskRepo();
            Task task = repo.GetById(comment.taskId);


            if (comment.commentCreatorId == AuthenticationManager.LoggedUser.Id || task.creatorId == AuthenticationManager.LoggedUser.Id)
            {
                UserRepo repoUser = new UserRepo();
                List<User> users = repoUser.GetAll().ToList();

                DeleteCommentVM delete = new DeleteCommentVM();
                delete.users = new List<User>();

                delete.Id = comment.Id;
                delete.Tittle = comment.commentTittle;
                delete.Content = comment.commentContent;
                delete.CreatorId = comment.commentCreatorId;
                delete.TaskId = comment.taskId;
                delete.Date = comment.Date;

                foreach (var item in users)
                {
                    delete.users.Add(item);
                }

                return View(delete);
            }
            else
            {
                return RedirectToAction("Details", "TaskManagement", new { id = comment.taskId });
            }
          
        }

        [HttpPost]
        public ActionResult DeleteComment(DeleteCommentVM delete)
        {
            CommentRepo repo = new CommentRepo();
            Comment deletedComment = repo.GetById(Convert.ToInt32(delete.Id));
            repo.Delete(deletedComment);

            return RedirectToAction("Details", "TaskManagement", new { id = Convert.ToInt32(deletedComment.taskId) });
        }

    }
}