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
using ToDoList.Services;
using ToDoList.web.Areas.UserDashboard.Models;

namespace ToDoList.web.Areas.UserDashboard.Controllers
{
    [Area("UserDashboard")]
    [Authorize(Roles = UserRoles.ROLE_USER)]
    public class TasksController : Controller
    {
        private readonly ILogger<TasksController> _logger;
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksController(ILogger<TasksController> logger, IProjectService projectService, ITaskService taskService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _projectService = projectService;
            _taskService = taskService;
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult MarkInProgress(Guid SelectedTaskId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = _taskService.MarkInProgress(SelectedTaskId);
                if (result != null)
                {
                    _logger.LogInformation($"User {currentUser.Email} checked {SelectedTaskId} as in progress succesfully");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult MarkDone(Guid SelectedTaskId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = _taskService.MarkDone(SelectedTaskId);

                if (result != null)
                {
                    _logger.LogInformation($"User {currentUser.Email} checked {SelectedTaskId} as done succesfully");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Create([Bind("ProjectId,Title,Description,Evaluation,Priority,Status,TillDate")] TaskViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var task = new ToDoList.Domains.Task(Guid.NewGuid(), viewModel.Title, viewModel.Description, viewModel.Evaluation, viewModel.Priority, viewModel.Status, DateTime.Now, viewModel.TillDate);
                task.Project = _projectService.GetProjectById(viewModel.ProjectId).Result;

                var result = _taskService.CreateNewTask(task);

                if (result != null)
                {
                    _logger.LogInformation($"ProjectOwner {currentUser.Email} created new task {task.Id} succesfully");
                }
            }
            return RedirectToAction("DetailsAsProjectOwner", "Projects", new { id = viewModel.ProjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid SelectedTaskId, Guid SelectedProjectId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = _taskService.DeleteTaskById(SelectedTaskId);

                if (result != null)
                {
                    _logger.LogInformation($"Client {currentUser.Email} deleted project {SelectedTaskId} succesfully");
                }

                _logger.LogError($"Task deletion failed. Id {SelectedTaskId} client {currentUser.Email}");
            }
            return RedirectToAction("DetailsAsProjectOwner", "Projects", new { id = SelectedProjectId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FromBackLog(Guid SelectedTaskId)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                var result = _taskService.FromBacklog(SelectedTaskId, currentUser.Email);

                if (result != null)
                {
                    _logger.LogInformation($"User {currentUser.Email} added task {SelectedTaskId} from backlog succesfully");
                }

                _logger.LogError($"Task adding from Backlog failed. Id {SelectedTaskId} client {currentUser.Email}");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}