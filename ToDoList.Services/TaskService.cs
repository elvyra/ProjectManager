using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Data;
using ToDoList.Domains;
using ToDoList.Domains.Enums;

namespace ToDoList.Services
{
    public class TaskService : ITaskService
    {
        private readonly ILogger<TaskService> _logger;
        private readonly ToDoListDbContext _context;

        public TaskService(ILogger<TaskService> logger, ToDoListDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public Task MarkDone(Guid id)
        {
            var taskInDb =  _context.Tasks.SingleOrDefault(t => t.Id == id);
            if (taskInDb != null)
            {
                try
                {
                    taskInDb.Status = Status.Done;
                    _context.SaveChanges();
                    return taskInDb;
                }
                catch (Exception)
                {
                    _logger.LogError($"Error on task editing occoured. Task {id}");
                    return null;
                }
            }
            _logger.LogError($"Task with Id {id} not found");
            return null;
        }

        public Task MarkInProgress(Guid id)
        {
            var taskInDb = _context.Tasks.SingleOrDefault(t => t.Id == id);
            if (taskInDb != null)
            {
                try
                {
                    taskInDb.Status = Status.InProgress;
                    _context.SaveChanges();
                    return taskInDb;
                }
                catch (Exception)
                {
                    _logger.LogError($"Error on task editing occoured. Task {id}");
                    return null;
                }
            }
            _logger.LogError($"Task with Id {id} not found");
            return null;
        }

        public Task FromBacklog(Guid id, string email)
        {
            var taskInDb = _context.Tasks.SingleOrDefault(t => t.Id == id);
            if (taskInDb != null)
            {
                try
                {
                    taskInDb.AssignedTo = _context.Persons.FirstOrDefault(p => p.Email == email);
                    _context.SaveChanges();
                    return taskInDb;
                }
                catch (Exception)
                {
                    _logger.LogError($"Error on task adding AssignedTo person. Task {id}");
                    return null;
                }
            }
            _logger.LogError($"Task with Id {id} not found");
            return null;
        }

        public Task CreateNewTask(Task task)
        {
            try
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                _logger.LogError($"Error on task adding to DB occoured.");
                return null;
            }

            return task;
        }

        public  Task DeleteTaskById(Guid id)
        {
            var task =  _context.Tasks.FirstOrDefault(p => p.Id == id);
            if (task == null)
            {
                _logger.LogError($"Task with Id {id} not found");
                return null;
            }
            try
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return task;
            }
            catch (Exception err)
            {
                _logger.LogError($"Error occoured while trying to remove task with Id {id}: {err.Message}");
                return null;
            }
        }
    }
}
