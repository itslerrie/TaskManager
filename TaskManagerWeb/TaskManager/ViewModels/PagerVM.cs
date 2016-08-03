using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels
{
    public class PagerVM
    {
        public PagerVM()
        {
            CurrentPage = 1;
            PageSize = 10;
        }

        public string Prefix { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int PagesCount { get; set; }
    }
}