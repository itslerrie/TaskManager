using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SQLDataAccess.Enteties
{
    public class Comment : BaseEntety
    {
        [ForeignKey("task")]
        public int taskId { get; set; }
        public Task task { get; set; }
        public int commentCreatorId { get; set; }
        public string commentTittle { get; set; }
        public string commentContent { get; set; }
        public DateTime Date { get; set; }
    }
}
