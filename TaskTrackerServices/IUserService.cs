using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerModels;

namespace TaskTrackerServices
{
    public interface IUserService : IDisposable
    {
        UserShortDto CreateUser(string userLogin, string userPassword);
        UserShortDto UpdatePassword(string userLogin, string userPassword, string newUserPassword);

        bool CheckUser(string userLogin, string userPassword);
        bool IsLoginAvailable(string userLogin);

        bool isAllowedToWatchProject(string login, Guid id);

        bool isTaskOnProject(Guid task, Guid project);
        bool isCommentOnTask(Guid comment, Guid task);
    }   
}
