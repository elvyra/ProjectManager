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
using ToDoList.web.Areas.AdminDashboard.Models;

namespace ToDoList.web.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [Authorize(Roles = UserRoles.ROLE_ADMINISTRATOR)]

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IPersonService _personService;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(ILogger<AccountController> logger, IPersonService personService, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _personService = personService;
            _userManager = userManager;
        }

        // GET: Account
        public ActionResult Index()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;
            var user = _personService.GetUserByEmail(currentUser.Email);

            var viewModel = new UserViewModel();
            viewModel.Email = currentUser.Email;

            if (user != null)
            {
                viewModel.Name = user.Name;
                viewModel.Surname = user.Surname;
                viewModel.Education = user.Education;
                viewModel.Address = user.Address;
                viewModel.Skills = user.Skills;
                viewModel.Notes = user.Notes;
            }

            return View(viewModel);
        }

        // GET: Account/Edit/5
        public ActionResult Edit()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

            var user = _personService.GetUserByEmail(currentUser.Email);

            if (user != null)
            {
                var viewModel = new UserViewModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                    Education = user.Education,
                    Address = user.Address,
                    Skills = user.Skills,
                    Notes = user.Notes,
                };
                return View(viewModel);
            }
            else
            {
                var viewModel = new UserViewModel
                {
                    Email = currentUser.Email,
                };
                return View(viewModel);
            }
        }


        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Email, Name, Surname, Education, Address, Skills, Notes")] UserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

                Person user = new Person
                {
                    Email = viewModel.Email,
                    Name = viewModel.Name,
                    Surname = viewModel.Surname,
                    Education = viewModel.Education,
                    Address = viewModel.Address,
                    Skills = viewModel.Skills,
                    Notes = viewModel.Notes
                };

                var result = _personService.EditUser(user);

                if (result != null)
                {
                    _logger.LogInformation($"User {currentUser.Email} edited profile");
                    return RedirectToAction("Index", "Account");
                }
            }
            return View(viewModel);
        }
    }
}