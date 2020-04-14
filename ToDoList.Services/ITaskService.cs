using System;
using ToDoList.Domains;

namespace ToDoList.Services
{
    public interface ITaskService
    {
        Task CreateNewTask(Task task);
        Task MarkInProgress(Guid id);
        Task MarkDone(Guid id);
        Task FromBacklog(Guid id, string email);
        Task DeleteTaskById(Guid id);
    }
}