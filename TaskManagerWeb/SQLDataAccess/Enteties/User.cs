using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDataAccess.Enteties
{
    public class User : BaseEntety
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
