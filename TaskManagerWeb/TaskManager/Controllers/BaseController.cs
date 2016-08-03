using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Filters;
using TaskManager.Models;
using TaskManager.ViewModels;
using PagedList;
using System.Reflection;
using SQLDataAccess.Repository;
using System.Linq.Expressions;
using TaskManager.ViewModels.UserVM;

namespace TaskManager.Controllers
{
    [AuthenticationFilter]
    public abstract class BaseController<T, L, C, E, D, F> : Controller
        where T : BaseEntety, new()
        where C : BaseVMid, new()
        where E : BaseVMid, new()
        where D : BaseVMid, new()
        where L : BaseVMList<T, F>, new()
        where F : FilterVM<T>, new()
    {
        public abstract BaseRepo<T> SetRepo();
        public abstract void PopulateItem(T item, C model);
        public abstract void PopulateModel(T item, D model);
        public abstract void PopulateItemEdit(T item, E model);
        public abstract void PopulateModelEdit(T item, E model);
        public abstract void ExtraDelete(T item);

        public virtual void PopulateModel(L model)
        {
            BaseRepo<T> repo = SetRepo();

            TryUpdateModel(model);

            Expression<Func<T, bool>> filter = model.Filter.GenerateFilter();
            model.Items = repo.GetAll(filter, model.Pager.CurrentPage, model.Pager.PageSize).ToList();

            int resultCount = repo.Count(filter);
            model.Pager.PagesCount = (int)Math.Ceiling(resultCount / (double)model.Pager.PageSize);
        }

        public virtual void FillList(C model)
        {
        }

        public ActionResult Index()
        {
            L model = new L();
            model.Pager = new PagerVM();
            model.Filter = new F();

            model.Pager.Prefix = "Pager.";
            model.Filter.Prefix = "Filter.";
            model.Filter.Pager = model.Pager;

            PopulateModel(model);

            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
           C model = new C();

            FillList(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(C model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            BaseRepo<T> repo = SetRepo();
            T newItem = new T();

            PopulateItem(newItem,model);

            repo.Create(newItem);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            BaseRepo<T> repo = SetRepo();
            T item = repo.GetById(id);

            D model =  new D();

            PopulateModel(item,model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(D model)
        {
            BaseRepo<T> repo = SetRepo();
            T deletedItem = repo.GetById(model.Id);

            repo.Delete(deletedItem);

            ExtraDelete(deletedItem);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            E model = new E();

            BaseRepo<T> repo = SetRepo();
            T item = repo.GetById(id);

            PopulateModelEdit(item,model);

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(E model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            BaseRepo<T> repo = SetRepo();
            T item = repo.GetById(model.Id); ;

            PopulateItemEdit(item,model);

            repo.Edit(item);

            return RedirectToAction("Index");
        }
    }
}