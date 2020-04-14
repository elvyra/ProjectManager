using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domains.Entities;
using ToDoList.Services;
using ToDoList.web.Areas.AdminDashboard.Models;

namespace ToDoList.web.Areas.AdminDashboard.Controllers
{
    [Area("AdminDashboard")]
    [Authorize(Roles = UserRoles.ROLE_ADMINISTRATOR)]
    public class UsersController : Controller
    {
        private readonly IPersonService _personService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IPersonService personService)
        {
            _personService = personService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult List()
        {
            var usersList = _userManager.Users.ToList();
            var usersProfiles = _personService.GetAllUsers();

            IList<UserViewModel> identityUsers = new List<UserViewModel>();
            IList<UserViewModel> unregisteredProfiles = new List<UserViewModel>();

            if (usersList != null && usersList.Count() > 0)
            {
                foreach (var user in usersList)
                {
                    var userDetails = usersProfiles.FirstOrDefault(u => u.Email == user.Email);

                    var userViewModel = new UserViewModel { Email = user.Email };

                    userViewModel.Role = _userManager.GetRolesAsync(user).Result.First();

                    if (userDetails != null)
                    {
                        userViewModel.Name = !string.IsNullOrEmpty(userDetails.Name) ? userDetails.Name : "";
                        userViewModel.Surname = !string.IsNullOrEmpty(userDetails.Surname) ? userDetails.Surname : "";
                        userViewModel.Education = !string.IsNullOrEmpty(userDetails.Education) ? userDetails.Education : "";
                        userViewModel.Address = !string.IsNullOrEmpty(userDetails.Address) ? userDetails.Address : "";
                        userViewModel.Skills = !string.IsNullOrEmpty(userDetails.Skills) ? userDetails.Skills : "";
                        userViewModel.Notes = !string.IsNullOrEmpty(userDetails.Notes) ? userDetails.Notes : "";
                        userViewModel.AsProjectOwnerCount = userDetails.AsProjectOwner.Count();
                        userViewModel.AsScrumMasterCount = userDetails.AsScrumMaster.Count();
                        userViewModel.AsTeamMemberCount = userDetails.InTeams.Count();
                        userViewModel.AsClientCount = userDetails.AsClient.Count();
                    }
                    identityUsers.Add(userViewModel);
                }
            }

            if (usersProfiles != null && usersProfiles.Count() > 0)
            {
                foreach (var user in usersProfiles)
                {
                    if (usersList.FirstOrDefault(u => u.Email == user.Email) == null)
                    {
                        var userViewModel = new UserViewModel { Email = user.Email };

                        userViewModel.Name = !string.IsNullOrEmpty(user.Name) ? user.Name : "";
                        userViewModel.Surname = !string.IsNullOrEmpty(user.Surname) ? user.Surname : "";
                        userViewModel.Education = !string.IsNullOrEmpty(user.Education) ? user.Education : "";
                        userViewModel.Address = !string.IsNullOrEmpty(user.Address) ? user.Address : "";
                        userViewModel.Skills = !string.IsNullOrEmpty(user.Skills) ? user.Skills : "";
                        userViewModel.Notes = !string.IsNullOrEmpty(user.Notes) ? user.Notes : "";
                        userViewModel.AsProjectOwnerCount = user.AsProjectOwner.Count();
                        userViewModel.AsScrumMasterCount = user.AsScrumMaster.Count();
                        userViewModel.AsTeamMemberCount = user.InTeams.Count();
                        userViewModel.AsClientCount = user.AsClient.Count();

                        unregisteredProfiles.Add(userViewModel);
                    }
                }
            }

            var viewModel = new UsersListViewModel();
            viewModel.IdentityUsers = identityUsers;
            viewModel.UnregisteredProfiles = unregisteredProfiles;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create([Bind("Email, Password, Name, Surname, Role")] CreateUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = viewModel.Email, Email = viewModel.Email };
                var result = await _userManager.CreateAsync(user, viewModel.Password);
                if (result.Succeeded)
                {
                    if (_roleManager.RoleExistsAsync(viewModel.Role).Result)
                    {
                        await _userManager.AddToRoleAsync(user, viewModel.Role);
                    }

                    return RedirectToAction("List");
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteProfile(string SelectedProfileEmail)
        {
            if (ModelState.IsValid)
            {
                var result = _personService.DeleteUserByEmail(SelectedProfileEmail);              
            }

            return RedirectToAction("List");
        }


        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeleteUser(string SelectedUserEmail)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(SelectedUserEmail).Result;

                if (user != null)
                {
                    var rolesForUser = _userManager.GetRolesAsync(user).Result;
                    foreach (var item in rolesForUser)
                    {
                        var resultRole = _userManager.RemoveFromRoleAsync(user, item).Result;
                    }
                    var result = _userManager.DeleteAsync(user).Result;
                }
            }
            return RedirectToAction("List");
        }
    }
}

