using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.CommentRequest
{
    public class NewCommentRequest
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public string Text { get; set; }
      
    }
}
