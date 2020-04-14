using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.Domains;
using ToDoList.Services;
using ToDoList.web.Models;
using ToDoList.web.Models.Enums;

namespace ToDoList.web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDemoDataSeeder _dataSeeder;
        private readonly IPublicDataService _publicDataService;

        public HomeController(ILogger<HomeController> logger, IDemoDataSeeder dataSeeder, IPublicDataService publicDataService)
        {
            _logger = logger;
            _dataSeeder = dataSeeder;
            _publicDataService = publicDataService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedDemoData()
        {
            _dataSeeder.RemoveAllData();
            _dataSeeder.SeedDemoData();
            return RedirectToAction("Index");
        } 

         public IActionResult ClearDatabase()
        {
            _dataSeeder.RemoveAllData();
            return RedirectToAction("Index");
        } 
    }
}
