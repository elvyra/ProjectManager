using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.web.Models;
using ToDoList.web.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.web.Controllers
{
    [AllowAnonymous]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            var error = new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier, 404, "Route not found");
            _logger.LogError($"Unknown Error occured: Error status: {error.ErrorStatus}, Request Id: {error.RequestId}, Error Code: {error.ErrorCode}, Error Text: {error.ErrorText}");
            return View("Error", error);
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            var error = new ErrorViewModel(Activity.Current?.Id ?? HttpContext.TraceIdentifier, statusCode, "Something went wrong, Check the data or try later.", ErrorStatus.info);
            switch (statusCode)
            {
                case 401:
                    error.ErrorText = "Unauthorized access denied";
                    error.ErrorStatus = ErrorStatus.warning;
                    break;
                case 403:
                    error.ErrorText = "Forbidden (you do not have required permissions)";
                    error.ErrorStatus = ErrorStatus.warning;
                    break;
                case 404:
                    error.ErrorText = "The page or resource was not found";
                    error.ErrorStatus = ErrorStatus.warning;
                    break;
                case int code when (code >= 400 && code < 500):
                    error.ErrorText = "Something went wrong. Please try again";
                    error.ErrorStatus = ErrorStatus.warning;
                    break;
                case 500:
                    error.ErrorText = "Internal server error";
                    error.ErrorStatus = ErrorStatus.danger;
                    break;
                case int code when (code >= 500 && code < 600):
                    error.ErrorText = "Server Error occured. Please, try later";
                    error.ErrorStatus = ErrorStatus.danger;
                    break;
            }
            _logger.LogError($"Error occured: Error status: {error.ErrorStatus}, Request Id: {error.RequestId}, Error Code: {error.ErrorCode}, Error Text: {error.ErrorText}");
            return View("Error", error);
        }
    }
}