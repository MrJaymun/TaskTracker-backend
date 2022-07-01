using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerData;
using TaskTrackerModels;
using TaskTrackerModels.Tasks;

namespace TaskTrackerServices
{
    public class TaskService : ITaskService
    {
        private readonly UserDataContext userDataContext;
        public TaskService(UserDataContext userDataContext)
        {
            this.userDataContext = userDataContext;
        }

        public TaskShortDto CreateTask(string name, string desc, Guid projectId, string login)
        {
            TaskTrackerData.Task task = new TaskTrackerData.Task();

            var performer = userDataContext.Users.FirstOrDefault(p => p.Login == login);
            if(performer == null) return null;
            var project = userDataContext.Projects.FirstOrDefault(p => p.Guid == projectId);
            if(project == null) return null;
            
            task.Guid = Guid.NewGuid();
            task.Name = name;
            task.Description = desc;
            task.Performer = performer;
            task.Project = project;
            task.Status = userDataContext.TaskStatuses.FirstOrDefault(p => p.Status == "Planned");
            task.BeginDate = DateTime.MinValue;
            task.EndDate = DateTime.MinValue;

            userDataContext.Tasks.Add(task);

            userDataContext.SaveChanges();
            return Converter.ConvertShort(task);
        }

        public List<TaskWatchDto> GetTasks(Guid projectId)
        {
           var result = new List<TaskWatchDto>();
           var data = userDataContext.Tasks.Include(p => p.Status).Include(t => t.Comments).Include(u => u.Performer).Where(i => i.Project.Guid == projectId).OrderBy(d => d.BeginDate).ToArray();
            if (data.Length == 0) return null;
            foreach(var task in data)
            {
                result.Add(Converter.ConvertWatch(task));
            }
            return result;
            
        }

        public TaskDto GetTask(Guid projectId, Guid taskId)
        {
           var task = userDataContext.Tasks.Include(c => c.Comments).Include(s => s.Status).Include(u => u.Performer).FirstOrDefault(p => p.Project.Guid == projectId && p.Guid == taskId);
            if(task == null) return null;
            return Converter.Convert(task);
        }

        public bool UpdateTask(Guid taskId, string name, string description)
        {
            var task = userDataContext.Tasks.FirstOrDefault(p => p.Guid == taskId);
            if (task == null) return false;
            if(name != task.Name)
            {
                task.Name = name;
            }
            if(description != task.Description)
            {
                task.Description = description;
            }
            userDataContext.SaveChanges();
            return true;
        }

        public bool UpdateTaskStatus(Guid taskId, string newStatus)
        {
            var task = userDataContext.Tasks.Include(s => s.Status).FirstOrDefault(p => p.Guid == taskId);

            var status = userDataContext.TaskStatuses.FirstOrDefault(p => p.Status == newStatus);
            if(status == null || task == null) return false;
            task.Status = status;

            switch (status.Status)
            {
                case "Planned":
                    if(task.BeginDate != DateTime.MinValue)
                    {
                        task.BeginDate = DateTime.MinValue;
                    }
                    if (task.EndDate!= DateTime.MinValue)
                    {
                        task.EndDate = DateTime.MinValue;
                    }
                    break;
                case "In progress":
                    if (task.BeginDate == DateTime.MinValue)
                    {
                        task.BeginDate = DateTime.Now;
                    }
                    if (task.EndDate != DateTime.MinValue)
                    {
                        task.EndDate = DateTime.MinValue;
                    }
                    break;
                default:
                    if (task.BeginDate == DateTime.MinValue)
                    {
                        task.BeginDate = DateTime.Now;
                    }
                    if (task.EndDate == DateTime.MinValue)
                    {
                        task.EndDate = DateTime.Now;
                    }
                    break;
            }
           userDataContext.SaveChanges();
            return true;
        }

        public bool DeleteTask(Guid taskId)
        {
            var task = userDataContext.Tasks.FirstOrDefault(p => p.Guid == taskId);
            if(task == null)
            {
                return false;
            }
            userDataContext.Tasks.Remove(task);
            userDataContext.SaveChanges();
            return true;
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    userDataContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        
    }
}
