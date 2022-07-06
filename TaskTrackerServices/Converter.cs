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
    public static class Converter
    {
        public static UserShortDto ConvertShort(User user)
        {
            UserShortDto dto = new UserShortDto();
            dto.Id = user.Guid;
            dto.Login = user.Login;
            dto.Password = user.Password;

            return dto;
        }



        public static ProjectDto Convert(Project project)
        {
            var dto = new ProjectDto();
            
            dto.Id = project.Guid;
            dto.Name = project.Name;
            dto.Description = project.Description;
            dto.IsPersonal = project.IsPersonal;
            foreach(var user in project.Users){
                dto.Users.Add(ConvertShort(user));
                if(user.Login == project.Creator)
                {
                    dto.Author = ConvertShort(user);
                }
            }
            foreach(var task in project.Tasks)
            {
                dto.Tasks.Add(ConvertShort(task));
            }
           
            
            return dto;
        }

        public static ProjectShortDto ConvertShort(Project project, string login)
        {
            if(project == null)
            {
                return null;
            }
            var dto = new ProjectShortDto();

            dto.Id = project.Guid;
            dto.Name = project.Name;
            dto.Description = project.Description;
            dto.IsPersonal = project.IsPersonal;
            dto.Author = project.Creator;
            dto.UsersCount = project.Users.Count;

            if (project.Creator == login)
            {
                dto.IsUserIn = true;
            }
            else
            {
                dto.IsUserIn = false;
                foreach (var user in project.Users)
                {
                    if (user.Login == login)
                    {
                        dto.IsUserIn = true;
                    }
                }
            }


            return dto;
        }

        public static ProjectWatchDto ConvertWatch(Project project, string login)
        {
            if (project == null)
            {
                return null;
            }
            var dto = new ProjectWatchDto();

            dto.Id = project.Guid;
            dto.Name = project.Name;
            dto.Description = project.Description;
            dto.Author = project.Creator;
            foreach(var user in project.Users)
            {
                dto.users.Add(user.Login);
            }
            
            return dto;
        }

        public static ProjectMainDto ConvertMain(Project project, string login)
        {
            if (project == null)
            {
                return null;
            }
            var dto = new ProjectMainDto();

            dto.Id = project.Guid;
            dto.Name = project.Name;
            dto.Description = project.Description;
            dto.Author = project.Creator;
            foreach (var user in project.Users)
            {
                dto.users.Add(user.Login);
            }
            dto.allTasks = project.Tasks.Count;

          
            dto.activeTasks = 0;

           
            foreach(var task in project.Tasks)
            {

               if(task.Status.Status != "Done")
                {
                    dto.activeTasks++;
                }

            }
         

            return dto;
        }


        public static TaskShortDto ConvertShort(TaskTrackerData.Task task)
        {
            var dto = new TaskShortDto();
            dto.Id = task.Guid;
            dto.Name = task.Name;
            dto.Description= task.Description;
            dto.Performer = ConvertShort(task.Performer);
            dto.BeginDate = task.BeginDate;
            dto.EndDate = task.EndDate;
            dto.Status = task.Status.Status;
            foreach (var comment in task.Comments)
            {
                dto.Comments.Add(ConvertShort(comment));
            }
                return dto;
        }

        public static CommentShortDto ConvertShort(Comment comment)
        {
            var dto = new CommentShortDto();
            dto.Id= comment.Guid;
            dto.Text = comment.Text;
            dto.Author = comment.Author.Login;

            dto.Date = comment.Date;
            return dto;
        }

        public static CommentDto Convert(Comment comment)
        {
            var dto = new CommentDto();
            dto.Id = comment.Guid;
            dto.Text = comment.Text;
            dto.Author = comment.Author.Login;
            dto.Date = comment.Date;
            return dto;
        }



        public static TaskWatchDto ConvertWatch(TaskTrackerData.Task task)
        {
            var dto = new TaskWatchDto();

            dto.Id=task.Guid;
            dto.Name = task.Name;
            dto.Performer = task.Performer.Login;
            dto.BeginDate = task.BeginDate;
            dto.EndDate = task.EndDate;
            dto.Status = task.Status.Status;
            dto.CommentsCount = task.Comments.Count;
            return dto;
        }

        public static TaskDto Convert(TaskTrackerData.Task task)
        {
            var dto = new TaskDto();
            dto.Id = task.Guid;
            dto.Name=task.Name;
            dto.Description = task.Description;
            dto.ProjectId = task.Project.Guid;
            dto.Performer = task.Performer.Login;
            dto.BeginDate=task.BeginDate;
            dto.EndDate=task.EndDate;
            dto.Status=task.Status.Status;

            foreach(var comment in task.Comments)
            {
                dto.Comments.Add(ConvertShort(comment));
            }

            return dto;
        }
    }
}
