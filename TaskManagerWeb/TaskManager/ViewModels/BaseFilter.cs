using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels
{
    public class BaseFilter
    {
        public PagerVM Pager { get; set; }

        public string Prefix { get; set; }
    }
}