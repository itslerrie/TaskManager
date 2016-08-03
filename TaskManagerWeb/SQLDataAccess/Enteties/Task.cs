using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SQLDataAccess.Enteties
{
    public class Task : BaseEntety
    {
        public int assignetId { get; set; }
        public string tittle { get; set; }
        public string content { get; set; }
        public int estimate { get; set; }
        [ForeignKey("creator")]
        public int creatorId { get; set; }
        public User creator { get; set; }
        public DateTime createdON { get; set; }
        public DateTime editedON { get; set; }
        public int editedBY { get; set; }
        public bool finnished { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<LogWork> LogWork { get; set; }
    }
}
