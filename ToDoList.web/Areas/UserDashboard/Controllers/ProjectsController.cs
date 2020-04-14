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
    public class ProjectsController : Controller
    {
        private readonly ILogger<ProjectsController> _logger;
        private readonly IProjectService _projectService;
        private readonly UserManager<IdentityUser> _userManager;

        public ProjectsController(ILogger<ProjectsController> logger, IProjectService projectService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _projectService = projectService;
            _userManager = userManager;
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var project = await _projectService.GetProjectById(id);

                if (project != null)
                {
                    var viewModel = CreateDetailsProjectViewModel(project);
                    _logger.LogInformation($"User {currentUser.Email} previewed project {project.Id}");
                    return View(viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> DetailsAsProjectOwner(Guid id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var project = await _projectService.GetProjectByIdAsProjectOwner(id);

                if (project != null)
                {
                    var viewModel = CreateDetailsProjectAsProjectOwnerViewModel(project);
                    _logger.LogInformation($"ProjectOwner {currentUser.Email} previewed project {project.Id}");
                    return View(viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        // GET: Projects/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var project = await _projectService.GetProjectById(id);

                if (project != null)
                {
                    var viewModel = new ProjectViewModel
                    {
                        Id = project.Id,
                        Title = project.Title,
                        Description = project.Description,
                        ClientCompany = project.ClientCompany,
                        ClientEmail = project.ClientEmail,
                        StartDate = project.StartDate,
                        EndDate = project.EndDate,

                    };
                    return View(viewModel);
                }

            }
            return RedirectToAction("Details", "Project", id);
        }

        // POST: Projects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id, Title,Description,ClientCompany,StartDate,EndDate")] ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                Project project = new Project(
                    viewModel.Id,
                    viewModel.Title,
                    !string.IsNullOrEmpty(viewModel.Description) ? viewModel.Description : "",
                    !string.IsNullOrEmpty(viewModel.ClientCompany) ? viewModel.ClientCompany : "",
                    viewModel.StartDate,
                    viewModel.EndDate);

                var result = await _projectService.EditProject(project);

                if (result != null)
                {
                    _logger.LogInformation($"Client {currentUser.Email} edited project {project.Id} succesfully");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
        }

        public IActionResult MarkAsCompleted(Guid SelectedProjectId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = _projectService.SetStateAsCompleted(SelectedProjectId).Result;

                if (result != null)
                {
                    _logger.LogInformation($"User {currentUser.Email} marked project {SelectedProjectId} as completed succesfully");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Function for transforming Project Model to Details Project View Model
        /// </summary>
        /// <param name="project">project to transform</param>
        /// <returns>created DetailsProjectViewModel</returns>
        private DetailsProjectViewModel CreateDetailsProjectViewModel(Project project)
        {
            return new DetailsProjectViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ClientCompany = project.ClientCompany,
                ProjectOwnerName = project.ProjectOwner != null ? project.ProjectOwner.Name : "",
                ProjectOwnerSurname = project.ProjectOwner != null ? project.ProjectOwner.Surname : "",
                ProjectOwnerEmail = project.ProjectOwner != null ? project.ProjectOwner.Email : "",
                ScrumMasterName = project.ScrumMaster != null ? project.ScrumMaster.Name : "",
                ScrumMasterSurname = project.ScrumMaster != null ? project.ScrumMaster.Surname : "",
                ScrumMasterEmail = project.ScrumMaster != null ? project.ScrumMaster.Email : "",
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Stage = project.Stage,
                TotalTasksCount = project.Tasks.Count(),
                DoneTasksCount = project.Tasks.Where(t => t.Status == Status.Done).Count(),
                TeamMembersCount = project.Team.Count()
            };
        }

        /// <summary>
        /// Function for transforming Project Model to Details Project As Project Owner View Model
        /// </summary>
        /// <param name="project">project to transform</param>
        /// <returns>created DetailsProjectAsProjectOwnerViewModel</returns>
        private DetailsProjectAsProjectOwnerViewModel CreateDetailsProjectAsProjectOwnerViewModel(Project project)
        {
            return new DetailsProjectAsProjectOwnerViewModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                ClientCompany = project.ClientCompany,
                ScrumMaster = new PersonMainDashboardViewModel
                {
                    Name = project.ScrumMaster != null ? project.ScrumMaster.Name : "",
                    Surname = project.ScrumMaster != null ? project.ScrumMaster.Surname : "",
                    Email = project.ScrumMaster != null ? project.ScrumMaster.Email : ""
                },
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Stage = project.Stage,
                TotalTasksCount = project.Tasks.Count(),
                DoneTasksCount = project.Tasks.Where(t => t.Status == Status.Done).Count(),
                CriticalTasksCount = project.Tasks.Where(t => t.Priority == Priority.Critical).Count(),

                TasksList = project.Tasks,
                 
                TeamMembersCount = project.Team.Count()
            };
        }


    }
}
