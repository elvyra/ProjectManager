using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Domains;
using ToDoList.Domains.Entities;
using ToDoList.Domains.Enums;
using ToDoList.Services;
using ToDoList.web.Areas.UserDashboard.Models;

namespace ToDoList.web.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [Authorize(Roles = UserRoles.ROLE_USER)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService _projectService;
        private readonly IPersonService _personService;
        private readonly ITaskService _taskService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService, IPersonService personService, ITaskService taskService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _projectService = projectService;
            _personService = personService;
            _taskService = taskService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            // var projectsList = _projectService.GetUserProjects(currentUser.Email);
            var user = _personService.GetUserByEmailForDashboard(currentUser.Email);
            var viewModel = CreateDashboardViewModel(user);

            _logger.LogInformation($"Client {currentUser.Email} logged in");

            return View(viewModel);
        }



        /// <summary>
        /// Function for creating User Dashboard View Model
        /// </summary>
        /// <param name="projectsList">Projects list from Database</param>
        /// <returns>created MainDashboardViewModel</returns>

        private MainDashboardViewModel CreateDashboardViewModel(Person user)
        {

            var viewModel = new MainDashboardViewModel();
            if (user != null)
            {

                IList<MyProjectViewModel> doneProjects = new List<MyProjectViewModel>();

                viewModel.IsProjectOwnerList = ViewModelListFromProjectModelList(user.AsProjectOwner.Where(p => p.Stage != Stage.Completed).ToList());
                doneProjects = ViewModelListFromProjectModelList(user.AsProjectOwner.Where(p => p.Stage == Stage.Completed).ToList());

                viewModel.IsScrumMasterList = ViewModelListFromProjectModelList(user.AsScrumMaster.Where(p => p.Stage != Stage.Completed).ToList());
                var listTempScrumMaster = ViewModelListFromProjectModelList(user.AsScrumMaster.Where(p => p.Stage == Stage.Completed).ToList());
                foreach (var item in listTempScrumMaster)
                {
                    doneProjects.Add(item);
                }
                
                IList<Project> InTeamsProjectList = new List<Project>();
                foreach (var item in user.InTeams)
                {
                    InTeamsProjectList.Add(item.Project);
                }

                viewModel.IsInTeamList = ViewModelListFromProjectModelList(InTeamsProjectList.Where(p => p.Stage != Stage.Completed).ToList());
                var listTempInTeam = ViewModelListFromProjectModelList(InTeamsProjectList.Where(p => p.Stage == Stage.Completed).ToList());
                foreach (var item in listTempInTeam)
                {
                    doneProjects.Add(item);
                }

                viewModel.Backlog = new List<ToDoList.Domains.Task>();
                foreach (var project in user.InTeams)
                {
                    var backlogList = project.Project.Tasks.Where(t => t.AssignedTo == null);
                    foreach (var item in backlogList)
                    {
                        viewModel.Backlog.Add(item);
                    }
                }


                viewModel.CompletedList = doneProjects;

                viewModel.AssignedTasksList = user.Tasks.Where(t => t.Status != Status.Done).ToList();

                viewModel.DoneTasksList = user.Tasks.Where(t => t.Status == Status.Done).ToList();

                viewModel.IsProjectOwnerCount = viewModel.IsProjectOwnerList.Count();
                viewModel.IsScrumMasterCount = viewModel.IsScrumMasterList.Count();
                viewModel.IsInTeamCount = viewModel.IsInTeamList.Count();
                viewModel.CompletedCount = viewModel.CompletedList.Count();
                viewModel.AssignedTasksCount = viewModel.AssignedTasksList.Count();
                viewModel.DoneTasksCount = viewModel.DoneTasksList.Count();
                viewModel.BacklogCount = viewModel.Backlog.Count();
            }

            return viewModel;
        }

        private IList<MyProjectViewModel> ViewModelListFromProjectModelList(IList<Project> projectsList)
        {
            var viewModelProjects = new List<MyProjectViewModel>();

            foreach (var project in projectsList)
            {
                var viewModelProject = new MyProjectViewModel
                {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    ClientCompany = project.ClientCompany,
                    ClientEmail = project.ClientEmail,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Stage = project.Stage,
                    IsPublic = project.IsPublic,
                };

                if (project.Tasks.Count() > 0)
                {
                    var done = project.Tasks.Where(t => t.Status == Status.Done).Count();
                    var total = project.Tasks.Count();
                    var percent = (double)done / total * 100;
                    viewModelProject.Completeness = (int)percent;
                }

                if (project.ProjectOwner != null)
                {
                    viewModelProject.ProjectOwner = new PersonMainDashboardViewModel(project.ProjectOwner.Name, project.ProjectOwner.Surname, project.ProjectOwner.Email);
                }
                else
                {
                    viewModelProject.ProjectOwner = new PersonMainDashboardViewModel();
                }

                if (project.ScrumMaster != null)
                {
                    viewModelProject.ScrumMaster = new PersonMainDashboardViewModel(project.ScrumMaster.Name, project.ScrumMaster.Surname, project.ScrumMaster.Email);
                }
                else
                {
                    viewModelProject.ScrumMaster = new PersonMainDashboardViewModel();
                }

                if (project.StartDate != null && project.EndDate != null)
                {
                    TimeSpan durationTime = project.EndDate - project.StartDate;
                    viewModelProject.Duration = (int)durationTime.TotalDays;
                }

                if (project.Tasks.Count() > 0)
                {
                    var done = project.Tasks.Where(t => t.Status == Status.Done).Count();
                    var total = project.Tasks.Count();
                    var percent = (double)done / total * 100;
                    viewModelProject.Completeness = (int)percent;
                }

                if (project.EndDate != null)
                {
                    var dateNow = DateTime.Now;
                    TimeSpan timeTillDeadline = project.EndDate - dateNow;
                    viewModelProject.TillDeadline = (int)timeTillDeadline.TotalDays;
                }

                if (project.Team != null && project.Team.Count > 0)
                {
                    foreach (var teamMember in project.Team)
                    {
                        viewModelProject.Team.Add(new PersonMainDashboardViewModel(teamMember.Person.Name, teamMember.Person.Surname, teamMember.Person.Email));
                    }
                }

                viewModelProjects.Add(viewModelProject);
            }

            return viewModelProjects;
        }
    }
}