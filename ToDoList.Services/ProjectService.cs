using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.Domains;
using ToDoList.Domains.Enums;

namespace ToDoList.Services
{
    public class ProjectService : IProjectService
    {

        private readonly ILogger<ProjectService> _logger;
        private readonly ToDoListDbContext _context;

        public ProjectService(ILogger<ProjectService> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<Project> GetClientProjects(string email)
        {
            return _context.Projects
                .Where(p => p.ClientEmail == email)
                .Include(p => p.ProjectOwner)
                .Include(p => p.Tasks)
                .ToList();
        }
        public IList<Project> GetAllProjects()
        {
            return _context.Projects
                .Include(p => p.Client)
                .Include(p => p.ProjectOwner)
                .Include(p => p.ScrumMaster)
                .Include(p => p.Team)
                .ThenInclude(pm => pm.Person)
                .Include(p => p.Tasks)
                .ToList();
        }

        public async Task<Project> CreateNewProject(Project project)
        {
            project.NewId();
            try
            {
                _context.Projects.Add(project);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                _logger.LogError($"Error on project adding to DB occoured. Client: {project.ClientEmail}");
                return null;
            }

            return project;
        }

        public async Task<Project> EditProject(Project project)
        {
            var projectInDb = await _context.Projects.SingleOrDefaultAsync(p => p.Id == project.Id);
            if (projectInDb != null)
            {
                if (projectInDb.Stage != Stage.Completed)
                {
                    try
                    {
                        projectInDb.Title = project.Title;
                        projectInDb.Description = project.Description;
                        projectInDb.ClientCompany = project.ClientCompany;
                        projectInDb.SetStartData(project.StartDate);
                        projectInDb.EndDate = project.EndDate;
                        await _context.SaveChangesAsync();
                        return projectInDb;
                    }
                    catch (Exception)
                    {
                        _logger.LogError($"Error on project editing occoured. Client: {project.ClientEmail}, project {project.Id}");
                        return null;
                    }
                }
                else
                {
                    _logger.LogError($"Project with Id {project.Id} is mark as completed and can not be edited");
                    return null;
                }
            }
            _logger.LogError($"Project with Id {project.Id} not found");
            return null;
        }

        public async Task<Project> GetProjectById(Guid id)
        {
            var project = await _context.Projects
                .Include(p => p.ProjectOwner)
                .Include(p => p.ScrumMaster)
                .Include(p => p.Tasks)
                .Include(p => p.Team)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                _logger.LogError($"Project with Id {id} not found");
                return null;
            }
            return project;
        }

        public async Task<Project> GetProjectByIdAsProjectOwner(Guid id)
        {
            var project = await _context.Projects
                .Include(p => p.ScrumMaster)
                .Include(p => p.Tasks)
                .Include(p => p.Team)
                .ThenInclude(pm => pm.Person)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                _logger.LogError($"Project with Id {id} not found");
                return null;
            }
            return project;
        }

        public async Task<Project> DeleteProjectById(Guid id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                _logger.LogError($"Project with Id {id} not found");
                return null;
            }
            try
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return project;
            }
            catch (Exception err)
            {
                _logger.LogError($"Error occoured while trying to remove project with Id {id}: {err.Message}");
                return null;
            }
        }

        public async Task<Project> SetStateAsCompleted(Guid id)
        {
            var projectInDb = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
            if (projectInDb != null)
            {
                try
                {
                    projectInDb.Stage = Stage.Completed;
                    await _context.SaveChangesAsync();
                    return projectInDb;
                }
                catch (Exception)
                {
                    _logger.LogError($"Error on project setting as Completed occoured. Project {id}");
                    return null;
                }
            }
            _logger.LogError($"Project with Id {id} not found");
            return null;
        }
    }
}
