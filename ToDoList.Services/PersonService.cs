using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public class PersonService : IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly ToDoListDbContext _context;

        public PersonService(ILogger<PersonService> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Person GetUserByEmail(string email)
        {
            var user = _context.Persons
                .Include(p => p.AsProjectOwner)
                .Include(p => p.AsScrumMaster)
                .Include(p=> p.Tasks)
                .Include(p =>p.InTeams)
                .FirstOrDefault(p => p.Email == email);
            return user;
        }

        public Person DeleteUserByEmail(string email)
        {
            var person = _context.Persons.FirstOrDefaultAsync(p => p.Email == email).Result;
            if (person == null)
            {
                _logger.LogError($"Person with email {email} not found");
                return null;
            }

            try
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
                return person;
            }
            catch (Exception err)
            {
                _logger.LogError($"Error occoured while trying to remove person with email {email}: {err.Message}");
                return null;
            }
        }

        public Person GetUserByEmailForDashboard(string email)
        {
            var user = _context.Persons
                .Include(p => p.AsProjectOwner).ThenInclude(pm => pm.ScrumMaster)
                .Include(p => p.AsScrumMaster).ThenInclude(p => p.ProjectOwner)
                .Include(p => p.AsScrumMaster).ThenInclude(pm => pm.Team).ThenInclude(pmt => pmt.Person)
                .Include(p => p.InTeams).ThenInclude(pm => pm.Project).ThenInclude(pmt => pmt.ScrumMaster)
                .Include(p => p.InTeams).ThenInclude(pm => pm.Project).ThenInclude(pmt => pmt.Tasks).ThenInclude(pmtq => pmtq.AssignedTo)
                .Include(p => p.InTeams).ThenInclude(pm => pm.Project).ThenInclude(pmt => pmt.Team).ThenInclude(pmtq => pmtq.Person)
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Email == email);

            return user;
        }

        public Person EditUser(Person user)
        {
            var personInDb = _context.Persons.SingleOrDefault(p => p.Email == user.Email);
            if (personInDb == null)
            {
                try
                {
                    _context.Persons.Add(user);
                    _context.SaveChanges();
                }
                catch (Exception err)
                {
                    _logger.LogError($"Error on person adding to DB occoured. Client: {user.Email} Error: {err.Message}");
                    return null;
                }

                return user;
            }
            else
            {
                try
                {
                    personInDb.Email = user.Email;
                    personInDb.Name = user.Name;
                    personInDb.Surname = user.Surname;
                    personInDb.Education = user.Education;
                    personInDb.Address = user.Address;
                    personInDb.Skills = user.Skills;
                    personInDb.Notes = user.Notes;

                    _context.SaveChanges();
                    return personInDb;
                }
                catch (Exception err)
                {
                    _logger.LogError($"Error on person profile editing occoured. Client: {user.Email} Error: {err.Message}");
                    return null;
                }
            }
        }

        public IList<Person> GetAllUsers()
        {
            return _context.Persons
                .Include(p => p.Tasks)
                .Include(p => p.AsProjectOwner)
                .Include(p => p.AsScrumMaster)
                .Include(p => p.AsClient)
                .Include(p => p.InTeams)
                .ToList();
        }
    }
}
