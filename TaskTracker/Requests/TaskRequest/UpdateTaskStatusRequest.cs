using System.ComponentModel.DataAnnotations;

namespace TaskTracker.Requests.TaskRequest
{
    public class UpdateTaskStatusRequest
    {
        [Required]
        public string UserLogin { get; set; }
        [Required]
        public string UserPassword { get; set; }
      
        [Required]
        public string Status { get; set; }
      
        [Required]
        public Guid ProjectId { get; set; }

        [Required]
        public Guid TaskId { get; set; }


    }
}
