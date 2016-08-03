using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.Tools
{
    public class FilterByAttribute : Attribute
    {
        public string DisplayName { get; set; }
    }
}