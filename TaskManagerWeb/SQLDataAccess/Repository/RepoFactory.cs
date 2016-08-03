using SQLDataAccess.Enteties;
using SQLDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace TaskManager
{
    public static class RepoFactory
    {
        public static object CreateRepository(Type entityType)
        {
            if (entityType.Name == typeof(User).Name)
            {
                return new UserRepo();
            }
            else if (entityType.Name == typeof(Task).Name)
            {
                return new TaskRepo();
            }

            return null;
        }
    }
}