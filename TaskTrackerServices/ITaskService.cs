using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerModels;
using TaskTrackerModels.Tasks;

namespace TaskTrackerServices
{
    public interface ITaskService : IDisposable
    {
        TaskShortDto CreateTask(string name, string desc, Guid projectId, string login);

        List<TaskWatchDto> GetTasks(Guid projectId);

        TaskDto GetTask(Guid projectId, Guid taskId);

        bool UpdateTask(Guid taskId, string name, string description);
        bool UpdateTaskStatus(Guid taskId, string newStatus);
        bool DeleteTask(Guid taskId);
    }
}
