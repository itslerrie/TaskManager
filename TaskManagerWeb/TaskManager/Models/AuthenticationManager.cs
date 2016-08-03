using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLDataAccess.Service;
using SQLDataAccess.Enteties;


namespace TaskManager.Models
{
    public class AuthenticationManager
    {
        public static User LoggedUser
        {
            get
            {
                Authorise authorise = null;

                if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
                {
                    HttpContext.Current.Session["LoggedUser"] = new Authorise();
                }

                authorise = (Authorise)HttpContext.Current.Session["LoggedUser"];
                return authorise.LoggedUser;
            }
        }

        public static void Authenticate(string username, string password)
        {
            Authorise authorise = null;

            if (HttpContext.Current != null && HttpContext.Current.Session["LoggedUser"] == null)
            {
                HttpContext.Current.Session["LoggedUser"] = new Authorise();
            }

            authorise = (Authorise)HttpContext.Current.Session["LoggedUser"];
            authorise.Authenticate(username, password);
        }
        public static void Logout()
        {
            HttpContext.Current.Session["LoggedUser"] = null;
        }
    }
}