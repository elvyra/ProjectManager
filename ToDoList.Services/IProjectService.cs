using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public interface IProjectService
    {
        IList<Project> GetClientProjects(string email);
        IList<Project> GetAllProjects();

        Task<Project> CreateNewProject(Project project);

        Task<Project> EditProject(Project project);

        Task<Project> GetProjectById(Guid id);

        Task<Project> GetProjectByIdAsProjectOwner(Guid id);        

        Task<Project> DeleteProjectById(Guid id);

        Task<Project> SetStateAsCompleted(Guid id);
    }
}
