using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.web.Areas.AdminDashboard.Models
{
    public class UsersListViewModel
    {
        public IList<UserViewModel> IdentityUsers { get; set; } = new List<UserViewModel>();
        public IList<UserViewModel> UnregisteredProfiles { get; set; } = new List<UserViewModel>();

        public string SelectedUserEmail { get; set; }
        public string SelectedProfileEmail { get; set; }
    }
}
