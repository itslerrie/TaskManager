using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SQLDataAccess.Enteties
{
    public class LogWork : BaseEntety
    {
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        public Task Task { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int Time { get; set; }
    }
}
