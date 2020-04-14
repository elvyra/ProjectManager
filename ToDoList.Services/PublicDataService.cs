using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Data;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public class PublicDataService : IPublicDataService
    {
        private readonly ILogger<PublicDataService> _logger;
        private readonly ToDoListDbContext _context;

        public PublicDataService(ILogger<PublicDataService> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Project> PublicProjectsList()
        {
            try
            {
                return _context.Projects.Where(p => p.IsPublic).ToList();
            }
            catch (Exception error)
            {
                _logger.LogError($"Public projects list load from database failed. Error: ${error.Message}");
                return null;
            }
        }

        public IList<Person> PublicPersonsList()
        {
            try
            {
                return _context.Persons.Where(p => p.IsPublic).ToList();
            }
            catch (Exception error)
            {
                _logger.LogError($"Public persons list load from database failed. Error: ${error.Message}");
                return null;
            }
        }
        
        public IList<FAQ> FAQList()
        {
            try
            {
                return _context.FAQ.ToList();
            }
            catch (Exception error)
            {
                _logger.LogError($"FAQ list load from database failed. Error: ${error.Message}");
                return null;
            }
        }
    }
}
