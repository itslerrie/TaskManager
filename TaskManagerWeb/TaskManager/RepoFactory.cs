using DataAccess.Enteties;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TaskManager
{
    public static class RepoFactory
    {
        public static object CreateRepository(Type entityType)
        {
            if (entityType.Name == typeof(User).Name)
            {
                return new UserRepo(HostingEnvironment.MapPath(@"~/Files_Data/users.txt"));
            }
            else if (entityType.Name == typeof(Task).Name)
            {
                return new TaskRepo(HostingEnvironment.MapPath(@"~/Files_Data/tasks.txt"));
            }

            return null;
        }
    }
}