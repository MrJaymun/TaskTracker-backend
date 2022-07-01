using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerModels;

namespace TaskTrackerServices
{
    public interface ICommentService : IDisposable
    {
        List<CommentDto> GetComments(Guid taskId);

        CommentDto CreateComment(Guid taskId, string login, string text);

        CommentDto UpdateComment(Guid commentId, string text);

        bool DeleteComment(Guid commentId, string login);
    }
}
