using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains.Entities;
using ToDoList.web.Models;

namespace ToDoList.web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
       
    
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (_roleManager.RoleExistsAsync(UserRoles.ROLE_CLIENT).Result)
                    {
                        var resultRole = _userManager.AddToRoleAsync(user, UserRoles.ROLE_CLIENT).Result;
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home", new { area = "ClientDashboard" });
                }

                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return RedirectToAction("Index", "Home");
        }
        

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(model.DemoUser))
                {
                    var result = await _signInManager.PasswordSignInAsync($"{model.DemoUser}@localhost", "P@ssw0rd", model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home", new { area = $"{model.DemoUser}Dashboard" });
                        }
                    }
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(model.Email), UserRoles.ROLE_ADMINISTRATOR))
                            {
                                return RedirectToAction("Index", "Home", new { area = "AdminDashboard" });
                            } 
                            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(model.Email), UserRoles.ROLE_USER))
                            {
                                return RedirectToAction("Index", "Home", new { area = "UserDashboard" });
                            } 
                            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(model.Email), UserRoles.ROLE_CLIENT))
                            {
                                return RedirectToAction("Index", "Home", new { area = "ClientDashboard" });
                            }
                        }
                    }
                }

                ModelState.AddModelError("", "Email and password combination is invalid.");
            }
            return View(model);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RouteToDashboard()
        {
            var currentUser = _userManager.GetUserAsync(HttpContext.User).Result;

            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(currentUser.Email), UserRoles.ROLE_ADMINISTRATOR))
            {
                return RedirectToAction("Index", "Home", new { area = "AdminDashboard" });
            }
            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(currentUser.Email), UserRoles.ROLE_USER))
            {
                return RedirectToAction("Index", "Home", new { area = "UserDashboard" });
            }
            if (await _userManager.IsInRoleAsync(await _userManager.FindByNameAsync(currentUser.Email), UserRoles.ROLE_CLIENT))
            {
                return RedirectToAction("Index", "Home", new { area = "ClientDashboard" });
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        
        
        [AllowAnonymous]
        public async Task<IActionResult> isEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already registered.");
            }
        }
        
        
        [AllowAnonymous]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewData["ReturnUrl"] = ReturnUrl;
            return RedirectToAction("Error", "Error", new { statusCode  = 403 });
        }        
    }
}
