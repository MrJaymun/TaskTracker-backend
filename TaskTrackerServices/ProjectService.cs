using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerData;
using TaskTrackerModels;

namespace TaskTrackerServices
{
    public class ProjectService : IProjectService
    {
        private readonly UserDataContext userDataContext;
        public ProjectService(UserDataContext userDataContext)
        {
            this.userDataContext = userDataContext;
        }

        public ProjectDto CreateProject(string login, string name, string description, bool isPerosnal)
        {
            var newProject = new Project();
            newProject.Guid = Guid.NewGuid();
            newProject.Name = name;
            newProject.Description = description;
            newProject.IsPersonal = isPerosnal;
            newProject.Creator = login;
            newProject.Users.Add(userDataContext.Users.FirstOrDefault(p => p.Login == login));
            userDataContext.Projects.Add(newProject);
            userDataContext.SaveChanges();
            return Converter.Convert(newProject);
        }

       

        public List<ProjectShortDto> GetMyProjects(string login)
        {
            var array = new List<ProjectShortDto>();
            var result = userDataContext.Projects.Include(c => c.Users).Where(p => p.Creator == login || p.IsPersonal == false);
            foreach (var item in result)
            {
               
                array.Add(Converter.ConvertShort(item, login));
            }

            return array;
        }


        public ProjectWatchDto WatchProject(Guid id, string login)
        {
            return Converter.ConvertWatch(userDataContext.Projects.Include(c => c.Users).FirstOrDefault(p => p.Guid == id), login);
        }

        public ProjectMainDto GoToProject(Guid id, string login)
        {
            return Converter.ConvertMain(userDataContext.Projects.Include(c => c.Users).Include(t => t.Tasks).ThenInclude(s => s.Status).FirstOrDefault(p => p.Guid == id), login);
        }

        public ProjectDto UpdateProject(Guid id, string login, string newName, string newDescription)
        {
            var project = userDataContext.Projects.FirstOrDefault(p => p.Guid == id);
          
            if (project == null) { return null; }
            if (project.Creator != login)
            {
                return null;
            }
            if (project.Name != newName)
            {
                project.Name = newName;
            }
            if (project.Description != newDescription)
            {
                project.Description = newDescription;
            }
            userDataContext.SaveChanges();
            return Converter.Convert(project);
        }

        public ProjectDto JoinProject(Guid id, string login)
        {
            var project = userDataContext.Projects.Include(c => c.Users).FirstOrDefault(p => p.Guid == id);
            var user = userDataContext.Users.FirstOrDefault(p => p.Login == login);
            if(project == null || user == null || project.Users.Contains(user))
            {
                return null;
            }
            project.Users.Add(user);
            userDataContext.SaveChanges();
            return Converter.Convert(project);
        }

        public bool DeleteProject(Guid id, string login)
        {
            var project = userDataContext.Projects.FirstOrDefault(p => p.Guid == id && p.Creator == login);
            if (project == null) return false;
            userDataContext.Projects.Remove(project);
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
