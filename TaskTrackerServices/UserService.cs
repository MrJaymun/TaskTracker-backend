using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerData;
using TaskTrackerModels;
using TaskTrackerServices.Additional;

namespace TaskTrackerServices
{
    public class UserService : IUserService
    {
        private readonly UserDataContext userDataContext;
        public UserService(UserDataContext userDataContext)
        {
            this.userDataContext = userDataContext;
        }

        public bool CheckUser(string userLogin, string userPassword)
        {
            var user = userDataContext.Users.FirstOrDefault(p => p.Login == userLogin);

            if (user == null) return false;
            if (!PasswordWorker.VerifyHashedPassword(user.Password, userPassword)) return false;
            return true;
        }

        public bool IsLoginAvailable(string userLogin)
        {
            var user = userDataContext.Users.FirstOrDefault(p => p.Login == userLogin);
            return user == null;
        }

        public UserShortDto CreateUser(string userLogin, string userPassword)
        {
            if (!IsLoginAvailable(userLogin))
            {
                return null;
            }
            var newUser = new User();
            newUser.Guid = Guid.NewGuid();
            newUser.Login = userLogin;
            newUser.Password = PasswordWorker.HashPassword(userPassword);
            userDataContext.Users.Add(newUser);
            userDataContext.SaveChanges();

            return Converter.ConvertShort(newUser);
        }

        public UserShortDto UpdatePassword(string userLogin, string userPassword, string newUserPassword)
        {
            var user = userDataContext.Users.FirstOrDefault(p => p.Login == userLogin);
            if (!PasswordWorker.VerifyHashedPassword(user.Password, userPassword)) return null;
            user.Password = PasswordWorker.HashPassword(newUserPassword);
            userDataContext.SaveChanges();
            return Converter.ConvertShort(user);

        }

        public bool isAllowedToWatchProject(string login, Guid id)
        {
            var project = userDataContext.Projects.Include(u => u.Users).FirstOrDefault(p => p.Guid == id);
            if (project == null) return false;
            foreach (var user in project.Users)
            {
                if (user.Login == login) return true;
            }
            return false;
        }

        public bool isTaskOnProject(Guid task, Guid project)
        {
            var result = userDataContext.Tasks.FirstOrDefault(p => p.Project.Guid == project && p.Guid == task);
            return result != null;
        }

        public bool isCommentOnTask(Guid comment, Guid task)
        {
            var result = userDataContext.Comments.Include(t => t.Task).FirstOrDefault(p => p.Task.Guid == task && p.Guid == comment);
            return result != null;
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
