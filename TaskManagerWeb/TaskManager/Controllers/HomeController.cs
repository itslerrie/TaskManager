using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SQLDataAccess.Service;
using TaskManager.Models;
using SQLDataAccess.Repository;
using SQLDataAccess.Enteties;
using System.Web.Hosting;
using TaskManager.ViewModels.Home;
using TaskManager.Filters;
using TaskManager.ViewModels.UserVM;

namespace TaskManager.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            CreateUserVM model = new CreateUserVM();

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CreateUserVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            UserRepo repo = new UserRepo();
            User newItem = new User();

            newItem.Username = model.Username;
            newItem.Password = model.Password;
            newItem.IsAdmin = false;
            newItem.Fullname = model.FullName;
            newItem.Email = model.Email;

            repo.Create(newItem);

            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult LogIn()
        {
            return View();

        }

        [HttpPost]
        public ActionResult LogIn(LogInVM login)
        {
            AuthenticationManager.Authenticate(login.Username, login.Password);

            if (AuthenticationManager.LoggedUser == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            else
            {
                  return RedirectToAction("index", "TaskManagement");
            }
        }

        public ActionResult LogOut()
        {
            if (AuthenticationManager.LoggedUser == null)
            {
                return RedirectToAction("Login", "Home");
            }
            AuthenticationManager.Logout();

            return RedirectToAction("Login", "Home");
        }

        [AuthenticationFilter]
        [HttpGet]
        public ActionResult Details()
        {
            User user = AuthenticationManager.LoggedUser;

            UserDetailsVM details = new UserDetailsVM();

            details.Id = user.Id;
            details.Username = user.Username;
            details.Password = user.Password;
            details.Email = user.Email;
            details.FullName = user.Fullname;
            details.IsAdmin = user.IsAdmin;

            return View(details);
        }

        [AuthenticationFilter]
        [HttpGet]
        public ActionResult Edit()
        {
            User user = AuthenticationManager.LoggedUser;

            EditLoggedVM edit = new EditLoggedVM();

            edit.Id = user.Id;
            edit.Username = user.Username;
            edit.Email = user.Email;
            edit.Password = user.Password;
            edit.FullName = user.Fullname;

            return View(edit);
        }

        [HttpPost]
        public ActionResult Edit(EditLoggedVM edit)
        {
            if (!this.ModelState.IsValid)
            {
                return View(edit);
            }

            UserRepo repo = new UserRepo();
            User user = new User();

            user.Id = edit.Id;
            user.Username = edit.Username;
            user.Password = edit.Password;
            user.Fullname = edit.FullName;
            user.IsAdmin = AuthenticationManager.LoggedUser.IsAdmin;
           
            repo.Edit(user);
            AuthenticationManager.Authenticate(AuthenticationManager.LoggedUser.Username, AuthenticationManager.LoggedUser.Password);
            return RedirectToAction("Details", "Home");
        }
    }
}