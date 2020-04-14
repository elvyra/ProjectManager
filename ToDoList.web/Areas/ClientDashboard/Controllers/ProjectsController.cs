using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                    _logger.LogInformation($"Client {currentUser.Email} previewed project {project.Id}");
                    return View(viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ClientCompany,StartDate,EndDate")] ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                Project project = new Project(
                    viewModel.Title,
                    !string.IsNullOrEmpty(viewModel.Description) ? viewModel.Description : "",
                    false,
                    !string.IsNullOrEmpty(viewModel.ClientCompany) ? viewModel.ClientCompany : "",
                    currentUser.Email,
                    viewModel.StartDate,
                    viewModel.EndDate);

                var result = await _projectService.CreateNewProject(project);

                if (result != null)
                {
                    _logger.LogInformation($"Client {currentUser.Email} created new project {project.Id} succesfully");
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(viewModel);
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

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid SelectedProjectId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = await _projectService.DeleteProjectById(SelectedProjectId);

                if (result != null)
                {
                    _logger.LogInformation($"Client {currentUser.Email} deleted project {SelectedProjectId} succesfully");
                    return RedirectToAction("Index", "Home");
                }

                _logger.LogError($"Project deletion failed. Id {SelectedProjectId} client {currentUser.Email}");
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
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Stage = project.Stage,
                TotalTasksCount = project.Tasks.Count(),
                DoneTasksCount = project.Tasks.Where(t => t.Status == Status.Done).Count(),
                TeamMembersCount = project.Team.Count()
            };
        }
    }
}