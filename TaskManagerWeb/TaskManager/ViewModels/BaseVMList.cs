using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskManager.ViewModels;

namespace TaskManager.ViewModels
{
    public class BaseVMList<T, F>
        where T:BaseEntety
        where F:FilterVM<T>
    {
        public List<T> Items { get; set; }
        public PagerVM Pager { get; set; }
        public F Filter { get; set; }
    }
}