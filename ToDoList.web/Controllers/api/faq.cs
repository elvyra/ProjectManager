using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domains = ToDoList.Domains;
using ToDoList.Services;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.web.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQ : ControllerBase
    {
        private readonly IPublicDataService _publicDataService;

        public FAQ(IPublicDataService publicDataService)
        {
            _publicDataService = publicDataService;
        }

        // GET api/values
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IList<Domains.FAQ>> Get()
        {
            var list = _publicDataService.FAQList();

            if (list == null)
            {
                return NotFound();
            }

            return Ok(list);
        }        
    }
}
