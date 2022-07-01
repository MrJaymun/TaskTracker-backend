using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerModels;


namespace TaskTrackerServices
{
    public interface IProjectService : IDisposable
    {
        List<ProjectShortDto> GetMyProjects(string login);

      
        ProjectWatchDto WatchProject(Guid id, string login);
        ProjectMainDto GoToProject(Guid id, string login);
        ProjectDto CreateProject(string login, string name, string description, bool isPerosnal);
        ProjectDto UpdateProject(Guid id, string login, string newName, string newDescription);
        ProjectDto JoinProject(Guid id, string login);

        bool DeleteProject(Guid id, string login);


    }
}
