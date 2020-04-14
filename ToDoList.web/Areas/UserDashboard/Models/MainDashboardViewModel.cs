using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Domains;

namespace ToDoList.web.Areas.UserDashboard.Models
{
    public class MainDashboardViewModel
    {
        public int IsProjectOwnerCount { get; set; }
        public int IsScrumMasterCount { get; set; }
        public int IsInTeamCount { get; set; }
        public int CompletedCount { get; set; }
        public int AssignedTasksCount { get; set; }
        public int DoneTasksCount { get; set; }
        public int BacklogCount { get; set; }

        public IList<MyProjectViewModel> IsProjectOwnerList { get; set; }
        public IList<MyProjectViewModel> IsScrumMasterList { get; set; }
        public IList<MyProjectViewModel> IsInTeamList { get; set; }
        public IList<MyProjectViewModel> CompletedList { get; set; }
        public IList<Task> AssignedTasksList { get; set; }
        public IList<Task> DoneTasksList { get; set; }
        public IList<Task> Backlog{ get; set; }

        public Guid SelectedProjectId { get; set; }
        public Guid SelectedTaskId { get; set; }
    }
}
