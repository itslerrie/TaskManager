using SQLDataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SQLDataAccess
{
    public class TaskManagerWebDbContext<T> : DbContext
          where T : class
    {
        public TaskManagerWebDbContext()
            : base("name=TaskManagerWeb")
        {
        }

        public DbSet<T> Items { get; set; }
    }


}
