using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerData;
using TaskTrackerModels;

namespace TaskTrackerServices
{
    public class CommentService : ICommentService
    {
        private readonly UserDataContext userDataContext;
        public CommentService(UserDataContext userDataContext)
        {
            this.userDataContext = userDataContext;
        }

        public CommentDto CreateComment(Guid taskId, string login, string text)
        {
            var comment = new Comment();
            comment.Guid = new Guid();
            comment.Author = userDataContext.Users.FirstOrDefault(p => p.Login == login);
            comment.Text = text;
            comment.Date = DateTime.Now;
            comment.Task = userDataContext.Tasks.FirstOrDefault(p=>p.Guid == taskId);
            if (comment.Task == null || comment.Author == null) return null;

            userDataContext.Comments.Add(comment);
            userDataContext.SaveChanges();
            return Converter.Convert(comment);

        }

        

        public List<CommentDto> GetComments(Guid taskId)
        {
          
            throw new NotImplementedException();
        }

        public CommentDto UpdateComment(Guid commentId, string text)
        {
            var comment = userDataContext.Comments.FirstOrDefault(p => p.Guid == commentId);
            if(comment == null) return null;
            if(comment.Text != text)
            {
                comment.Text = text;
            }
            userDataContext.SaveChanges();
            return Converter.Convert(comment);
        }

        public bool DeleteComment(Guid commentId, string login)
        {
            var comment = userDataContext.Comments.FirstOrDefault(p => p.Guid == commentId && p.Author.Login == login);
            if (comment == null) return false;

            userDataContext.Comments.Remove(comment);
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
