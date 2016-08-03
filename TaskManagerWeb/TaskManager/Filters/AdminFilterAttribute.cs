using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Models;

namespace TaskManager.Filters
{
    public class AdminFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthenticationManager.LoggedUser.IsAdmin != true)
            {
                filterContext.Result = new RedirectResult("/TaskManagement/Index");
                return;
            }
        }
    }
}