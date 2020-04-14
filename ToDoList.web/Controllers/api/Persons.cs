using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Domains;
using ToDoList.Services;

namespace ToDoList.web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Persons : ControllerBase
    {
        private readonly IPublicDataService _publicDataService;

        public Persons(IPublicDataService publicDataService)
        {
            _publicDataService = publicDataService;
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IList<Person>> Get()
        {
            var list = _publicDataService.PublicPersonsList();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }
    }
}
