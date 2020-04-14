using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domains.Entities;
using ToDoList.Services;
using ToDoList.web.Areas.AdminDashboard.Models;

namespace ToDoList.web.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [Authorize(Roles = UserRoles.ROLE_ADMINISTRATOR)]
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;

        public HomeController(IProjectService projectService, ITaskService taskService)
        {
            _projectService = projectService;
            _taskService = taskService;
        }

        public IActionResult Index(string sortOrder)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.StageSortParm = sortOrder == "Stage" ? "stage_desc" : "Stage";
            ViewBag.StartSortParm = sortOrder == "Start" ? "start_desc" : "Start";
            ViewBag.EndSortParm = sortOrder == "End" ? "end_desc" : "End";

            var projectsList = _projectService.GetAllProjects();

            switch (sortOrder)
            {
                case "title_desc":
                    projectsList = projectsList.OrderByDescending(p => p.Title).ToList();
                    break;
                case "Start":
                    projectsList = projectsList.OrderBy(p => p.StartDate).ToList();
                    break;
                case "start_desc":
                    projectsList = projectsList.OrderByDescending(p => p.StartDate).ToList();
                    break;
                case "End":
                    projectsList = projectsList.OrderBy(p => p.EndDate).ToList();
                    break;
                case "end_desc":
                    projectsList = projectsList.OrderByDescending(p => p.EndDate).ToList();
                    break;
                case "Stage":
                    projectsList = projectsList.OrderBy(p => p.Stage).ToList();
                    break;
                case "stage_desc":
                    projectsList = projectsList.OrderByDescending(p => p.Stage).ToList();
                    break;
                default:
                    projectsList = projectsList.OrderBy(p => p.Title).ToList();
                    break;
            }

            var viewModel = new MainDashboardViewModel();
            viewModel.AllProjects = projectsList;

            return View(viewModel);
            //  return RedirectToAction("List", "Users");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteProject(Guid SelectedProjectId)
        {
            if (ModelState.IsValid)
            {
                var project = _projectService.GetProjectById(SelectedProjectId).Result;

                if (project != null)
                {
                    while (project.Tasks.Count > 0)
                    {
                        _taskService.DeleteTaskById(project.Tasks.First().Id);
                    }
                }
                var result = _projectService.DeleteProjectById(SelectedProjectId).Result;
            }

            return RedirectToAction("Index");
        }





    }
}