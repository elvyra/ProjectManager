using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains;

namespace ToDoList.web.Areas.AdminDashboard.Models
{
    public class MainDashboardViewModel
    {
        public IList<Project> AllProjects { get; set; }

        public Guid SelectedProjectId { get; set; }
    }
}
