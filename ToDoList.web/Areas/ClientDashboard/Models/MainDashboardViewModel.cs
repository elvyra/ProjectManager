using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains;

namespace ToDoList.web.Areas.ClientDashboard.Models
{
    public class MainDashboardViewModel
    {
        public int WorkingOnProjectsCount{ get; set; }
        public int DoneProjectsCount{ get; set; }
        public int NewProjectsCount{ get; set; }
        public int UnconfirmedProjectsCount{ get; set; }

        public IList<MyProjectViewModel> WorkingOnProjectsList { get; set; }
        public IList<MyProjectViewModel> DoneProjectsList { get; set; }
        public IList<MyProjectViewModel> NewProjectsList { get; set; }
        public IList<MyProjectViewModel> UnconfirmedProjectsList { get; set; }

        public Guid SelectedProjectId { get; set; }

    }
}
