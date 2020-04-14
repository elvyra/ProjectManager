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
using ToDoList.web.Areas.ClientDashboard.Models;

namespace ToDoList.web.Areas.ClientDashboard.Controllers
{
    [Area("ClientDashboard")]
    [Authorize(Roles = UserRoles.ROLE_CLIENT)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProjectService _projectService;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IProjectService projectService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _projectService = projectService;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            var projectsList = _projectService.GetClientProjects(currentUser.Email);
            var viewModel = CreateDashboardViewModel(projectsList);

            _logger.LogInformation($"Client {currentUser.Email} logged in");

            return View(viewModel);
        }


        /// <summary>
        /// Function for creating Client Dashboard View Model
        /// </summary>
        /// <param name="projectsList">Projects list from Database</param>
        /// <returns>created MainDashboardViewModel</returns>

        private MainDashboardViewModel CreateDashboardViewModel(IList<Project> projectsList)
        {

            var viewModel = new MainDashboardViewModel();

            var viewModelProjects = new List<MyProjectViewModel>();

            foreach (var project in projectsList)
            {
                var viewModelProject = new MyProjectViewModel {
                    Id = project.Id,
                    Title = project.Title,
                    Description = project.Description,
                    ClientCompany = project.ClientCompany,     
                    ClientEmail = project.ClientEmail,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Stage = project.Stage,
                    IsPublic = project.IsPublic
                };

                if (project.ProjectOwner != null)
                {
                    viewModelProject.ProjectOwnerName = project.ProjectOwner.Name;
                    viewModelProject.ProjectOwnerSurname = project.ProjectOwner.Surname;
                    viewModelProject.ProjectOwnerEmail = project.ProjectOwner.Email;
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
                    viewModelProject.Completeness =  (int)percent;
                }

                if (project.EndDate != null)
                {
                    var dateNow = DateTime.Now;
                    TimeSpan timeTillDeadline = project.EndDate - dateNow;
                    viewModelProject.TillDeadline = (int)timeTillDeadline.TotalDays;
                }

                viewModelProjects.Add(viewModelProject);
            }

            viewModel.WorkingOnProjectsList = viewModelProjects.Where(p => p.Stage == Stage.InProgress).ToList();
            viewModel.DoneProjectsList = viewModelProjects.Where(p => p.Stage == Stage.Completed).ToList();
            viewModel.NewProjectsList = viewModelProjects.Where(p => p.Stage == Stage.New).ToList();
            viewModel.UnconfirmedProjectsList = viewModelProjects.Where(p => p.Stage == Stage.Unconfirmed).ToList();

            viewModel.WorkingOnProjectsCount = viewModel.WorkingOnProjectsList.Count();
            viewModel.DoneProjectsCount = viewModel.DoneProjectsList.Count();
            viewModel.NewProjectsCount = viewModel.NewProjectsList.Count();
            viewModel.UnconfirmedProjectsCount = viewModel.UnconfirmedProjectsList.Count();

            return viewModel;
        }
    }
}