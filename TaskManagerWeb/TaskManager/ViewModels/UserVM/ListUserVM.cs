using SQLDataAccess.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels.UserVM
{
    public class ListUserVM : BaseVMList<User, UserFilterVM>
    {
    }
}