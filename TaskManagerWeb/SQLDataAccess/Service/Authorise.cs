using SQLDataAccess.Enteties;
using SQLDataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLDataAccess.Service
{
    public class Authorise
    {
        public User LoggedUser { get; private set; }

        public void Authenticate(string username, string password)
        {
            UserRepo userRepo = new UserRepo();

            List<User> users = userRepo.GetAll(u => u.Username == username && u.Password == password).ToList();

            LoggedUser = users.Count > 0 ? users[0] : null;
        }
    }
}
